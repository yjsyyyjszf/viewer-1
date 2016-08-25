
#region License

// Copyright (c) 2013, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This file is part of the ClearCanvas RIS/PACS open source project.
//
// The ClearCanvas RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The ClearCanvas RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the ClearCanvas RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.Desktop.Tools;
using Macro.Dicom.Iod.Modules;
using Macro.ImageViewer.Graphics;
using Macro.ImageViewer.InputManagement;
using Macro.ImageViewer.StudyManagement;
using System.Xml;

namespace Macro.ImageViewer
{
    [ExtensionPoint]
    public sealed class PrintImageViewerExtansionPoint : ExtensionPoint<IView>
    { }

    [AssociateView(typeof(PrintImageViewerExtansionPoint))]
    public partial class PrintImageViewerComponent : ApplicationComponent, IPrintViewImageViewer, IContextMenuProvider
    {
        public const string PrintContextMenuSite = "Printimageviewer-contextmenu";
        public const string PrintKeyboardSite = "Printimageviewer-keyboard";

        #region Private fields
        private PrintViewImageBox _rootImageBox;
        private Rectangle _workRectangle;

        private StudyTree _studyTree;
        private EventBroker _eventBroker;

        private readonly PrintViewerShortcutManager _shortcutManager;
        private readonly IViewerSetupHelper _setupHelper;
        private ToolSet _toolSet;
        private IViewerActionFilter _contextMenuFilter;
        private ILayoutManager _layoutManager;

        private IImageBox _selectImageBox;

        private event EventHandler _closing;
        private event EventHandler _drawingEvent;
        private event EventHandler _layoutCompletedEvent;
        private event EventHandler _screenRectangleChanged;

        internal readonly IResourceResolver _resourceResolver;
        private ISelection _selection;

        private IDicomPrintPreviewComponent _dicomPrintComponent;

        private Dictionary<IPresentationImage, List<IGraphic>> referenceLines = new Dictionary<IPresentationImage, List<IGraphic>>();

        #endregion

        public PrintImageViewerComponent(IDesktopWindow desktopWindow)
        {
            _resourceResolver = new ApplicationThemeResourceResolver(this.GetType().Assembly);
            _shortcutManager = new PrintViewerShortcutManager(this);
            _setupHelper = new PrintViewerSetupHelper();
            _setupHelper.SetImageViewer(this);
            PrintPresentationImagesHost.ImageViewerComponent = this;
        }

        public override void Start()
        {

            _contextMenuFilter = _setupHelper.GetContextMenuFilter() ?? ViewerActionFilter.Null;
            _contextMenuFilter.SetImageViewer(this);

            _toolSet = new ToolSet(CreateTools(), CreateToolContext());

            // since the keyboard action model is otherwise never used, we explicitly invoke it here to apply the persisted action model values to the actions
            ActionModelRoot.CreateModel(ActionsNamespace, PrintKeyboardSite, _toolSet.Actions);

            foreach (ITool tool in _toolSet.Tools)
                _shortcutManager.RegisterImageViewerTool(tool);
            base.Start();
        }

        #region IPrintViewImageViewer 成员

        public void PropertyValueChanged()
        {
            NotifyPropertyChanged("FilmCount");
            NotifyPropertyChanged("ImageCount");
        }

        public PresentationImageCollection DisplayPresentationImages
        {
            get
            {
                if (_rootImageBox != null)
                {
                    if (_rootImageBox.DisplaySet != null)
                    {
                        return _rootImageBox.DisplaySet.PresentationImages;
                    }
                }
                return null;
            }

        }

        public int FilmCount
        {
            get
            {
                int filmCount;
                if (DisplayPresentationImages.Count % RootImageBox.Tiles.Count == 0)
                {
                    filmCount = DisplayPresentationImages.Count / RootImageBox.Tiles.Count;
                }
                else
                {
                    filmCount = DisplayPresentationImages.Count / RootImageBox.Tiles.Count + 1;
                }
                return filmCount;
            }
        }

        public int ImageCount
        {
            get { return DisplayPresentationImages.Count; }
        }

        public IResourceResolver ResourceResolver
        {
            get { return _resourceResolver; }
        }

        public IDisplaySet DisplaySet
        {
            get { return RootImageBox.DisplaySet; }
        }

        public PrintViewImageBox RootImageBox
        {
            get
            {
                return _rootImageBox;
            }
            internal set { _rootImageBox = value; }
        }

        public void Accept(bool isAllPage, bool printedDeleteImage)
        {
            if (DisplaySet == null || DisplaySet.PresentationImages.Count == 0)
            {
                return;
            }
            _dicomPrintComponent.Accept(DisplaySet, RootImageBox.Tiles, isAllPage, printedDeleteImage);
        }

        public void Delete(ITile tile, bool isMove)
        {
            throw new NotImplementedException();
        }

        public void MergerGrid()
        {
            if (RootImageBox.SelectedTile == null)
            {
                return;
            }

            var printViewTile = RootImageBox.SelectedTile as PrintViewTile;

            var memorableCommand = new MemorableUndoableCommand(RootImageBox) { BeginState = RootImageBox.CreateMemento() };
            List<PrintViewTile> selectTiles;
            if (RootImageBox.DisplaySet != null && RootImageBox.DisplaySet.PresentationImages.Count != 0)
            {
                selectTiles = new List<PrintViewTile>();
                foreach (var selectPresentationImage in RootImageBox.SelectPresentationImages)
                {
                    if (selectPresentationImage.Tile == null)
                    {
                        continue;
                    }
                    var tile = selectPresentationImage.Tile as PrintViewTile;
                    if (!selectTiles.Contains(tile))
                    {
                        selectTiles.Add(tile);
                    }
                }

                if (!selectTiles.Contains(printViewTile))
                {
                    selectTiles.Add(printViewTile);
                }
            }
            else
            {
                selectTiles = new List<PrintViewTile>();
                foreach (var selectTile in RootImageBox.SelectTiles)
                {
                    if (!selectTiles.Contains(selectTile))
                    {
                        selectTiles.Add(selectTile);
                    }
                }
            }

            if (selectTiles.Count <= 1)
            {
                return;
            }

            //合并单元格
            //并集合
            var mycell = RectangleF.Union(selectTiles[0].NormalizedRectangle, selectTiles[1].NormalizedRectangle);
            for (int i = 2; i < selectTiles.Count; i++)
            {
                mycell = RectangleF.Union(mycell, selectTiles[i].NormalizedRectangle);
            }

            //移出与合并之后相交的单元格
            var intersecTiles = new List<ITile>();
            foreach (var tile in RootImageBox.Tiles)
            {
                if (mycell.IntersectsWith(tile.NormalizedRectangle))
                {
                    intersecTiles.Add(tile);
                }
            }

            if (intersecTiles.Count == 0)
            {
                return;
            }
            //确定单元格的位置
            int index = RootImageBox.Tiles.IndexOf(intersecTiles[0]);
            if (index == -1)
            {
                return;
            }

            var mergerResult = new PrintViewTile();
            mergerResult.NormalizedRectangle = mycell;
            RootImageBox.Tiles.Insert(index, mergerResult);

            //移出相交的单元格
            foreach (var item in intersecTiles)
            {
                RootImageBox.Tiles.Remove(item);
            }

            EventsHelper.Fire(_layoutCompletedEvent, this, EventArgs.Empty);
            this.RootImageBox.TopLeftPresentationImageIndex = 0;
            this.RootImageBox.Draw();
            ClearSelectTiles();
            this.RootImageBox.SelectDefaultTile();
            memorableCommand.EndState = RootImageBox.CreateMemento();
            var historyCommand = new DrawableUndoableCommand(RootImageBox) { Name = "RootImageBoxMergerGrid" };
            historyCommand.Enqueue(memorableCommand);
            CommandHistory.AddCommand(historyCommand);

            PropertyValueChanged();
        }

        public void SubGrid(ImageDisplayFormat displayFormat)
        {
            if (RootImageBox.SelectedTile == null)
            {
                return;
            }

            var printViewTile = RootImageBox.SelectedTile as PrintViewTile;

            var memorableCommand = new MemorableUndoableCommand(RootImageBox) { BeginState = RootImageBox.CreateMemento() };
            List<PrintViewTile> selectTiles;
            if (RootImageBox.DisplaySet != null && RootImageBox.DisplaySet.PresentationImages.Count != 0)
            {
                selectTiles = new List<PrintViewTile>();
                foreach (var selectPresentationImage in RootImageBox.SelectPresentationImages)
                {
                    if (selectPresentationImage.Tile == null)
                    {
                        continue;
                    }
                    var tile = selectPresentationImage.Tile as PrintViewTile;
                    if (!selectTiles.Contains(tile) && !tile.isSubTile)
                    {
                        selectTiles.Add(tile);
                    }
                }

                if (!selectTiles.Contains(printViewTile) && !printViewTile.isSubTile)
                {
                    selectTiles.Add(printViewTile);
                }
            }
            else
            {
                selectTiles = new List<PrintViewTile>();
                foreach (var selectTile in RootImageBox.SelectTiles)
                {
                    if (!selectTiles.Contains(selectTile) && !selectTile.isSubTile)
                    {
                        selectTiles.Add(selectTile);
                    }
                }
            }

            if (selectTiles.Count == 0)
            {
                return;
            }



            foreach (var tile in selectTiles)
            {
                TileCollection collection = SetTileGridFactory(displayFormat);
                if (collection == null)
                {
                    continue;
                }
                SubGridFactory(displayFormat, ref collection, tile);
                int index = RootImageBox.Tiles.IndexOf(tile);
                RootImageBox.Tiles.Remove(tile);
                for (int i = collection.Count - 1; i >= 0; i--)
                {
                    var item = collection[i] as PrintViewTile;
                    item.isSubTile = true;
                    item.ParentPrintViewTile = tile;
                    RootImageBox.Tiles.Insert(index, item);
                    tile.SubTiles.Add(item);
                }
            }
            EventsHelper.Fire(_layoutCompletedEvent, this, EventArgs.Empty);
            this.RootImageBox.TopLeftPresentationImageIndex = 0;
            this.RootImageBox.Draw();
            ClearSelectTiles();
            this.RootImageBox.SelectDefaultTile();
            memorableCommand.EndState = RootImageBox.CreateMemento();
            var historyCommand = new DrawableUndoableCommand(RootImageBox) { Name = "RootImageBoxSubGrid" };
            historyCommand.Enqueue(memorableCommand);
            CommandHistory.AddCommand(historyCommand);

            PropertyValueChanged();
        }

        private void ClearSelectTiles()
        {
            if (RootImageBox == null)
            {
                return;
            }

            foreach (var printViewTile in RootImageBox.SelectTiles)
            {
                printViewTile.Selected = false;
            }

            RootImageBox.SelectTiles.Clear();
        }

        private void SubGridFactory(ImageDisplayFormat displayFormat, ref TileCollection collection, IPrintViewTile parentTile)
        {
            if (RootImageBox == null || displayFormat == null)
            {
                return;
            }

            foreach (PrintViewTile tile in collection)
            {
                float x = parentTile.NormalizedRectangle.X + parentTile.NormalizedRectangle.Width * tile.NormalizedRectangle.X;
                float y = parentTile.NormalizedRectangle.Y + parentTile.NormalizedRectangle.Height * tile.NormalizedRectangle.Y;
                float w = parentTile.NormalizedRectangle.Width * tile.NormalizedRectangle.Width;
                float h = parentTile.NormalizedRectangle.Height * tile.NormalizedRectangle.Height;
                tile.NormalizedRectangle = new RectangleF(x, y, w, h);
            }
        }


        public void Layout()
        { }

        public void Layout(FilmSize filmSize, FilmOrientation filmOrientation)
        {
            float w = 1;
            float h = 1;
            if (filmSize != null)
            {
                if (filmOrientation == FilmOrientation.Landscape) //horizontal film position
                {
                    h = filmSize.GetWidth(FilmSize.FilmSizeUnit.Inch);
                    w = filmSize.GetHeight(FilmSize.FilmSizeUnit.Inch);
                }
                else
                {
                    w = filmSize.GetWidth(FilmSize.FilmSizeUnit.Inch);
                    h = filmSize.GetHeight(FilmSize.FilmSizeUnit.Inch);
                }
            }

            RootImageBox = new PrintViewImageBox();
            RootImageBox.ImageViewer = this;
            RootImageBox.NormalizedRectangle = new RectangleF(0, 0, w, h);
            SetTileGrid(ImageDisplayFormat.Standard_1x1);
            //RootImageBox.DisplaySet = new DisplaySet();

        }

        public void ImageBoxSizeChanged(FilmSize filmSize, FilmOrientation filmOrientation)
        {
            float w = 1;
            float h = 1;
            if (filmSize != null)
            {
                if (filmOrientation == FilmOrientation.Landscape) //horizontal film position
                {
                    h = filmSize.GetWidth(FilmSize.FilmSizeUnit.Inch);
                    w = filmSize.GetHeight(FilmSize.FilmSizeUnit.Inch);
                }
                else
                {
                    w = filmSize.GetWidth(FilmSize.FilmSizeUnit.Inch);
                    h = filmSize.GetHeight(FilmSize.FilmSizeUnit.Inch);
                }
            }

            RootImageBox.NormalizedRectangle = new RectangleF(0, 0, w, h);
            EventsHelper.Fire(_layoutCompletedEvent, this, EventArgs.Empty);
            this.RootImageBox.Draw();
        }

        public void SetTileGrid(ImageDisplayFormat displayFormat)
        {
            TileCollection collection = SetTileGridFactory(displayFormat);

            if (collection == null)
            {
                return;
            }

            ClearSelectTiles();
            foreach (var item in RootImageBox.Tiles)
            {
                item.Dispose();
            }
            RootImageBox.Tiles.Clear();
            RootImageBox.Tiles.AddRange(collection);
            EventsHelper.Fire(_layoutCompletedEvent, this, EventArgs.Empty);
            this.RootImageBox.TopLeftPresentationImageIndex = 0;
            this.RootImageBox.Draw();
            PropertyValueChanged();
        }

        private TileCollection SetTileGridFactory(ImageDisplayFormat displayFormat)
        {
            TileCollection tiles = null;
            if (RootImageBox == null || displayFormat == null)
            {
                return null;
            }

            int row = displayFormat.Modifiers[1];
            int col = displayFormat.Modifiers[0];
            switch (displayFormat.Format)
            {
                case ImageDisplayFormat.FormatEnum.STANDARD:
                    tiles = SetTileGrid(row, col);
                    break;
                case ImageDisplayFormat.FormatEnum.ROW:

                    tiles = new TileCollection();
                    double tileWidth = 1.0d / 2;
                    double tileHeight = 1.0d / 2;

                    RectangleF rect1 = new RectangleF(0, 0, 1, (float)tileHeight);

                    PrintViewTile tile1 = new PrintViewTile();
                    tile1.NormalizedRectangle = rect1;
                    tiles.Add(tile1);

                    for (int r = 1; r == 1; r++)
                    {
                        for (int column = 0; column < 2; column++)
                        {
                            double x = column * tileWidth;
                            double y = r * tileHeight;
                            RectangleF rect = new RectangleF((float)x, (float)y, (float)tileWidth, (float)tileHeight);

                            PrintViewTile tile = new PrintViewTile();
                            tile.NormalizedRectangle = rect;
                            tiles.Add(tile);
                        }
                    }
                    break;
                case ImageDisplayFormat.FormatEnum.COL:

                    tiles = new TileCollection();

                    double ColtileWidth = 1.0d / 2;
                    double ColtileHeight = 1.0d / 2;

                    RectangleF Colrect = new RectangleF(0, 0, (float)ColtileWidth, 1);

                    PrintViewTile Coltile = new PrintViewTile();
                    Coltile.NormalizedRectangle = Colrect;
                    tiles.Add(Coltile);

                    for (int r = 0; r < 2; r++)
                    {
                        for (int column = 1; column == 1; column++)
                        {
                            double x = column * ColtileWidth;
                            double y = r * ColtileHeight;
                            RectangleF rect = new RectangleF((float)x, (float)y, (float)ColtileWidth, (float)ColtileHeight);

                            PrintViewTile tile = new PrintViewTile();
                            tile.NormalizedRectangle = rect;
                            tiles.Add(tile);
                        }
                    }
                    break;
            }

            return tiles;

        }

        public TileCollection SetTileGrid(int numberOfRows, int numberOfColumns)
        {
            TileCollection tiles = new TileCollection();

            Platform.CheckPositive(numberOfRows, "numberOfRows");
            Platform.CheckPositive(numberOfColumns, "numberOfColumns");

            double tileWidth = 1.0d / numberOfColumns;
            double tileHeight = 1.0d / numberOfRows;

            for (int row = 0; row < numberOfRows; row++)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    double x = column * tileWidth;
                    double y = row * tileHeight;
                    RectangleF rect = new RectangleF((float)x, (float)y, (float)tileWidth, (float)tileHeight);

                    PrintViewTile tile = new PrintViewTile();
                    tile.NormalizedRectangle = rect;
                    tiles.Add(tile);
                }
            }

            return tiles;
        }

        public void Select(ITile tile)
        {
            throw new NotImplementedException();
        }

        public IDicomPrintPreviewComponent DicomPrintComponent
        {
            get
            {
                if (_dicomPrintComponent == null)
                {
                    _dicomPrintComponent = CreateDicomPrintComponent();
                }
                return _dicomPrintComponent;
            }
        }

        private IDicomPrintPreviewComponent CreateDicomPrintComponent()
        {
            IDicomPrintPreviewComponent dicomPrint = null;
            try
            {
                dicomPrint = (IDicomPrintPreviewComponent)new DicomPrintPreviewComponentExtensionPoint().CreateExtension();
                if (dicomPrint != null)
                {
                    dicomPrint.DesktopWindow = DesktopWindow;
                    dicomPrint.PrintImageViewerComponent = this;
                }
            }
            catch (NotSupportedException e)
            {
                Platform.Log(LogLevel.Debug, e);
            }
            return dicomPrint;
        }

        public event EventHandler Closing
        {
            add { _closing += value; }
            remove { _closing -= value; }
        }

        /// <summary>
        /// Occurs when the <see cref="PhysicalWorkspace"/> is drawn.
        /// </summary>
        public event EventHandler Drawing
        {
            add { _drawingEvent += value; }
            remove { _drawingEvent -= value; }
        }

        public void Draw()
        {
            EventsHelper.Fire(_drawingEvent, this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when all changes to image box collection are complete.
        /// </summary>
        /// <remarks>
        /// <see cref="LayoutCompleted"/> is raised by the Framework when
        /// <see cref="SetImageBoxGrid(int, int)"/> has been called.  If you are adding/removing
        /// <see cref="IImageBox"/> objects manually, you should raise this event when
        /// you're done by calling <see cref="OnLayoutCompleted"/>.  This event is
        /// consumed by the view to reduce flicker when layouts are changed.  
        /// In that way, it is similar to the WinForms methods <b>SuspendLayout</b>
        /// and <b>ResumeLayout</b>.
        /// </remarks>
        public event EventHandler LayoutCompleted
        {
            add { _layoutCompletedEvent += value; }
            remove { _layoutCompletedEvent -= value; }
        }

        /// <summary>
        /// Occurs when <see cref="IPhysicalWorkspace.ScreenRectangle"/> changes.
        /// </summary>
        public event EventHandler ScreenRectangleChanged
        {
            add { _screenRectangleChanged += value; }
            remove { _screenRectangleChanged -= value; }
        }

        public System.Drawing.Rectangle WorkRectangle
        {
            get { return _workRectangle; }
            set
            {
                if (_workRectangle.Equals(value))
                    return;

                _workRectangle = value;
                OnScreenRectangleChanged();
            }
        }

        private void OnScreenRectangleChanged()
        {
            EventsHelper.Fire(_screenRectangleChanged, this, EventArgs.Empty);
        }

        public Dictionary<IPresentationImage, List<IGraphic>> ReferenceLines
        { get { return referenceLines; } }

        public void ClearAllImages()
        {
            foreach (var selectPresentationImage in this.DisplaySet.PresentationImages)
            {
                selectPresentationImage.Dispose();
            }
            this.DisplaySet.PresentationImages.Clear();
            foreach (var tile in this.RootImageBox.Tiles)
            {
                PrintViewTile printViewTile = tile as PrintViewTile;
                printViewTile.PresentationImage = null;
            }

            referenceLines.Clear();
            RootImageBox.TotleImageCount = 0;
            Draw();
            PropertyValueChanged();
        }

        #endregion

        #region IImageViewer 成员

        public IDesktopWindow DesktopWindow
        {
            get { return this.Host.DesktopWindow; }
        }

        /// <summary>
        /// Gets actions associated with the <see cref="ImageViewerComponent"/>'s
        /// <see cref="ToolSet"/>.
        /// </summary>
        public override IActionSet ExportedActions
        {
            get
            {
                // we should technically only export the actions that target the global menus
                // or toolbars, but it doesn't matter - the desktop will sort it out
                return _toolSet.Actions;
            }
        }

        /// <summary>
        /// Gets the namespace that qualifies the global action models owned by this <see cref="IImageViewer"/>. This value may not be null.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This namespace only applies to the global action models (i.e. global menu and toolbar sites) when the
        /// <see cref="IImageViewer"/> is launched as a <see cref="IWorkspace"/>. In contrast, the namespace returned
        /// by <see cref="ActionsNamespace"/> applies to local action models such as the viewer context menu.
        /// </para>
        /// <para>
        /// The default implementation defers to <see cref="ActionsNamespace"/>.
        /// </para>
        /// </remarks>
        public override string GlobalActionsNamespace
        {
            get { return this.ActionsNamespace; }
        }

        public virtual string ActionsNamespace
        {
            get { return typeof(PrintImageViewerComponent).FullName; }
        }

        public PrintViewerShortcutManager ShortcutManager
        {
            get { return _shortcutManager; }
        }

        public EventBroker EventBroker
        {
            get
            {
                if (_eventBroker == null)
                {
                    _eventBroker = new EventBroker();
                }
                return _eventBroker;
            }
        }

        /// <summary>
        /// Gets the <see cref="StudyTree"/>.
        /// </summary>
        /// <remarks>
        /// Although each <see cref="ImageViewerComponent"/> (IVC) maintains its own
        /// <see cref="StudyTree"/>, actual <see cref="ImageSop"/> objects are shared
        /// between IVCs for efficient memory usage.
        /// </remarks>
        public StudyTree StudyTree
        {
            get
            {
                if (_studyTree == null)
                    _studyTree = new StudyTree();

                return _studyTree;
            }
        }

        public ILayoutManager LayoutManager
        {
            get { return null; }
        }

        public IPhysicalWorkspace PhysicalWorkspace
        {
            get { return null; }
        }

        public ILogicalWorkspace LogicalWorkspace
        {
            get { return null; }
        }

        public IImageBox SelectedImageBox
        {
            get { return _selectImageBox; }
            internal set { _selectImageBox = value; }
        }

        public ITile SelectedTile
        {
            get
            {
                if (this.SelectedImageBox == null)
                    return null;
                else
                    return this.SelectedImageBox.SelectedTile;
            }
        }

        public IPresentationImage SelectedPresentationImage
        {
            get
            {
                if (this.SelectedTile == null)
                    return null;
                else
                    return this.SelectedTile.PresentationImage;
            }
        }

        /// <summary>
        /// 选择的所有图像
        /// </summary>
        public List<PresentationImage> SelectPresentationImages
        {
            get
            {
                if (this.SelectedImageBox == null)
                    return null;

                IPrintViewImageBox imageBox = this.SelectedImageBox as IPrintViewImageBox;
                return imageBox.SelectPresentationImages;
            }
        }

        public CommandHistory CommandHistory
        {
            get { return this.Host.CommandHistory; }
        }

        public string PatientsLoadedLabel
        {
            get { return "PrintPreview"; }
        }

        public IPriorStudyLoader PriorStudyLoader
        {
            get { return null; }
        }

        public Common.ExtensionData ExtensionData
        {
            get { return null; }
        }

        public void LoadStudy(LoadStudyArgs loadStudyArgs)
        { }

        public void LoadImages(string[] path)
        { }

        public void LoadImages(string[] files, IDesktopWindow desktop, out bool cancelled)
        {
            cancelled = false;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception e)
            {
                // shouldn't throw anything from inside Dispose()
                Platform.Log(LogLevel.Error, e);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_toolSet != null)
                {
                    _toolSet.Dispose();
                    _toolSet = null;
                }

                if (_studyTree != null)
                {
                    _studyTree.Dispose();
                    _studyTree = null;
                }

                if (DicomPrintComponent != null)
                {
                    DicomPrintComponent.Dispose();
                }
                referenceLines.Clear();
                referenceLines = null;

                if (this.DisplaySet != null && this.DisplaySet.PresentationImages != null)
                {
                    foreach (var selectPresentationImage in this.DisplaySet.PresentationImages)
                    {
                        selectPresentationImage.Dispose();
                    }
                    DisplaySet.PresentationImages.Clear();

                }

                if (_rootImageBox != null)
                {
                    _rootImageBox.Dispose();
                }

                PrintPresentationImagesHost.ImageViewerComponent = null;
            }
        }
        #endregion

        #region 菜单
        /// <summary>
        /// Gets the <see cref="ToolSet"/>.
        /// </summary>
        protected ToolSet ToolSet
        {
            get { return _toolSet; }
        }

        /// <summary>
        /// Creates a set of tools for this image viewer to load into its tool set.  Subclasses can override
        /// this to provide their own tools or cull the set of tools this creates.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable CreateTools()
        {
            return _setupHelper.GetTools();
        }

        /// <summary>
        /// Creates an <see cref="IImageViewerToolContext"/> to provide to all the tools owned by this image viewer.
        /// </summary>
        /// <remarks>
        /// Subclasses can override this to provide their own custom implementation of an <see cref="IImageViewerToolContext"/>.
        /// </remarks>
        protected virtual PrintImageViewToolContext CreateToolContext()
        {
            return new PrintImageViewToolContext(this);
        }

        #region Private/internal properties

        /// <summary>
        /// Gets the context menu model for the <see cref="ImageViewerComponent"/>.
        /// </summary>
        /// <param name="mouseInformation"></param>
        /// <returns>An <see cref="ActionModelNode"/></returns>
        /// <remarks>
        /// This method is used by the tile's view class to generate the 
        /// <see cref="ImageViewerComponent"/> context menu when a user right-clicks
        /// on a tile.  It is unlikely that you will ever need to use this method.
        /// </remarks>
        public virtual ActionModelNode GetContextMenuModel(IMouseInformation mouseInformation)
        {
            return this.ContextMenuModel;
        }

        public ActionModelNode ContextMenuModel
        {
            get
            {
                IActionSet actions = _toolSet.Actions.Select(_contextMenuFilter.Evaluate);
                return ActionModelRoot.CreateModel(ActionsNamespace, PrintContextMenuSite, actions);
            }
        }

        private ActionModelNode KeyboardModel
        {
            get
            {
                return ActionModelRoot.CreateModel(ActionsNamespace, PrintKeyboardSite, _toolSet.Actions);
            }
        }

        public ActionModelNode ToolbarModel
        {
            get
            {
                return ActionModelRoot.CreateModel(GlobalActionsNamespace, "global-toolbars", _toolSet.Actions);
            }
        }

        #endregion
        #endregion

        #region 文件设置布局
        public void SetGrid(string fileName)
        {
            if (this.RootImageBox == null || this.RootImageBox.Tiles.Count == 0)
            {
                return;
            }
            var file = System.IO.Path.Combine(Platform.ConfigDirectory, fileName);
            if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(file))
            {
                return;
            }
            var tiles = LoadConfigGridXml(file);
            if (tiles == null)
            {
                return;
            }
            RootImageBox.Tiles.Clear();
            RootImageBox.Tiles.AddRange(tiles);
            EventsHelper.Fire(_layoutCompletedEvent, this, EventArgs.Empty);
            this.RootImageBox.TopLeftPresentationImageIndex = 0;
            this.RootImageBox.Draw();
            ClearSelectTiles();
            PropertyValueChanged();
        }

        public void SaveGrid(string fileName)
        {
            if (this.RootImageBox == null || this.RootImageBox.Tiles.Count == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(fileName))
            {
                return;

            }
            var file = System.IO.Path.Combine(Platform.ConfigDirectory, fileName);
            SaveConfigGridXml(file);
        }

        private TileCollection LoadConfigGridXml(string fileName)
        {
            var xml = new XmlDocument();
            try
            {
                var tileCollection = new TileCollection();
                xml.Load(fileName);
                var root = xml.DocumentElement;
                foreach (XmlNode item in root.ChildNodes)
                {
                    float x = float.Parse(item.Attributes["x"].Value);
                    float y = float.Parse(item.Attributes["y"].Value);
                    float width = float.Parse(item.Attributes["width"].Value);
                    float height = float.Parse(item.Attributes["height"].Value);
                    var tile = new PrintViewTile();
                    tile.NormalizedRectangle = new RectangleF(x, y, width, height);
                    tileCollection.Add(tile);
                }
                return tileCollection;
            }
            catch (Exception e)
            {
                Platform.Log(LogLevel.Error, e);
            }
            return null;
        }

        private void SaveConfigGridXml(string fileName)
        {

            var document = new XmlDocument();
            var root = document.CreateElement("Root");
            document.AppendChild(root);
            int index = 0;
            foreach (var tile in this.RootImageBox.Tiles)
            {
                var itemName = string.Format("Title{0}", index++);
                var item = document.CreateElement(itemName);
                //x
                var x = document.CreateAttribute("x");
                x.Value = tile.NormalizedRectangle.X.ToString();
                item.Attributes.Append(x);
                //y
                var y = document.CreateAttribute("y");
                y.Value = tile.NormalizedRectangle.Y.ToString();
                item.Attributes.Append(y);
                //width
                var width = document.CreateAttribute("width");
                width.Value = tile.NormalizedRectangle.Width.ToString();
                item.Attributes.Append(width);
                //height
                var height = document.CreateAttribute("height");
                height.Value = tile.NormalizedRectangle.Height.ToString();
                item.Attributes.Append(height);
                root.AppendChild(item);

            }
            document.Save(fileName);

        }
        #endregion


    }
}
