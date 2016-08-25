
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
using System.Drawing;
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.ImageViewer.Common;
using Macro.ImageViewer.Rendering;

namespace Macro.ImageViewer
{
    [ExtensionPoint]
    public sealed class PrintPreviewTileViewExtensionPoint : ExtensionPoint<IView>
    { }


    [AssociateView(typeof(PrintPreviewTileViewExtensionPoint))]
    public class PrintViewTile : IPrintViewTile
    {
        #region Private Fields

        private IImageViewer _imageViewer;
        private PrintViewImageBox _parentImageBox;
        private PresentationImage _presentationImage;
        private RectangleF _normalizedRectangle;
        private Rectangle _clientRectangle;
        private bool _selected = false;
        private bool _enabled = true;
        private InformationBox _informationBox;
        private EditBox _editBox;
        private static int _borderWidth = 1;
        private static int _insetWidth = 5;
        private static Color _selectedColor = Color.Yellow;
        private static Color _unselectedColor = Color.Gray;
        private EventHandler<TileContextMenuRequestEventArgs> _contextMenuRequested;

        private event EventHandler _rendererChangedEvent;
        private event EventHandler _drawingEvent;
        private event EventHandler<ItemEventArgs<ITile>> _selectionChangedEvent;
        private event EventHandler<PresentationImageChangedEventArgs> _presentationImageChangedEvent;
        private event EventHandler _editBoxChanged;
        private event EventHandler<InformationBoxChangedEventArgs> _informationBoxChanged;

        private IPrintViewTile _parentPrintViewTile = null;
        private bool _isSubTile = false;
        private TileCollection _subTileCollection = null;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public PrintViewTile()
        {
            ExtensionData = new ExtensionData();
        }

        #region Public properties

        /// <summary>
        /// Gets the associated <see cref="IImageViewer"/>.
        /// </summary>
        /// <value>The associated <see cref="IImageViewer"/> or <b>null</b> if the 
        /// <see cref="Tile"/> is not part of the 
        /// physical workspace yet.</value>
        public IImageViewer ImageViewer
        {
            get { return _imageViewer; }
            set
            {
                _imageViewer = value;

                if (_presentationImage != null && _imageViewer != null)
                    _presentationImage.ImageViewer = _imageViewer;
            }
        }

        /// <summary>
        /// Gets the parent <see cref="IImageBox"/>
        /// </summary>
        /// <value>The parent <see cref="IImageBox"/> or <b>null</b> if the 
        /// <see cref="Tile"/> has not
        /// been added to the <see cref="IImageBox"/> yet.</value>
        public IImageBox ParentImageBox
        {
            get { return _parentImageBox; }
            set
            {
                Platform.CheckForNullReference(value, "Tile.ParentImageBox");
                _parentImageBox = value as PrintViewImageBox;
            }
        }

        /// <summary>
        /// Gets the <see cref="IPresentationImage"/> associated with this
        /// <see cref="Tile"/>.
        /// </summary>
        public IPresentationImage PresentationImage
        {
            get { return _presentationImage; }
            set
            {
                if (_presentationImage != value)
                {
                    PresentationImageChangedEventArgs eventArgs =
                        new PresentationImageChangedEventArgs(_presentationImage, value);

                    IRenderer oldRenderer = null;

                    if (_presentationImage != null)
                    {
                        oldRenderer = _presentationImage.ImageRenderer;
                    }

                    // Disassociate the old presentation image with this tile
                    if (_presentationImage != null)
                    {
                        _presentationImage.Tile = null;
                        //_presentationImage.Selected = false;
                    }

                    // Assign the new presentation image.  Can be null.
                    _presentationImage = value as PresentationImage;

                    // Assuming the new value is not null, associate
                    // this Tile with the new image
                    if (_presentationImage != null)
                    {
                        _presentationImage.Tile = this;
                        _presentationImage.Selected = this.Selected;

                        if (_presentationImage.ImageViewer == null)
                            _presentationImage.ImageViewer = this.ImageViewer;

                        IRenderer newRenderer = _presentationImage.ImageRenderer;

                        if (oldRenderer != null)
                        {
                            if (newRenderer.GetType() != oldRenderer.GetType())
                                EventsHelper.Fire(_rendererChangedEvent, this, EventArgs.Empty);
                        }
                        else
                        {
                            EventsHelper.Fire(_rendererChangedEvent, this, EventArgs.Empty);
                        }
                    }

                    EventsHelper.Fire(_presentationImageChangedEvent, this, eventArgs);
                }
            }
        }

        /// <summary>
        /// Gets the presentation image index.
        /// </summary>
        public int PresentationImageIndex
        {
            get
            {
                Platform.CheckMemberIsSet(_presentationImage, "PresentationImage", "ExceptionNoPresentationImageAssociatedWithTile");
                Platform.CheckMemberIsSet(this.ParentImageBox.DisplaySet, "ParentImageBox.DisplaySet", "ExceptionNoDisplaySetAssociatedWithImageBoxTile");

                IDisplaySet displaySet = this.ParentImageBox.DisplaySet;

                return displaySet.PresentationImages.IndexOf(this.PresentationImage);
            }
        }

        /// <summary>
        /// Gets or sets whether the tile is currently enabled.
        /// </summary>
        public bool Enabled
        {
            get { return _parentImageBox.Enabled ? _enabled : false; }
            set { _enabled = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Tile"/> is
        /// selected.
        /// </summary>
        /// <remarks>
        /// <see cref="Tile"/> selection is mutually exclusive.  That is,
        /// only one <see cref="Tile"/> is ever selected at a given time.  
        /// </remarks>
        public bool Selected
        {
            get
            {
                if (PresentationImage != null)
                {
                    return PresentationImage.Selected;
                }
                return _selected;
            }
            set
            {
                //if (_selected != value)
                //{
                _selected = value;
                EventsHelper.Fire(_selectionChangedEvent, this, new ItemEventArgs<ITile>(this));
                //}
            }
        }

        /// <summary>
        /// Gets or sets the colour of the border when the tile
        /// is selected.
        /// </summary>
        public static Color SelectedColor
        {
            get { return _selectedColor; }
            set { _selectedColor = value; }
        }

        /// <summary>
        /// Gets or sets the colour of the border when the tile
        /// is not selected.
        /// </summary>
        public static Color UnselectedColor
        {
            get { return _unselectedColor; }
            set { _unselectedColor = value; }
        }

        /// <summary>
        /// Gets the current border colour.
        /// </summary>
        public Color BorderColor
        {
            get
            {
                if (this.Selected)
                    return _selectedColor;
                else
                    return _unselectedColor;
            }
        }

        /// <summary>
        /// Gets or sets the width of the border in pixels.
        /// </summary>
        public static int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        /// <summary>
        /// Gets or sets the inset width of the border in pixels.
        /// </summary>
        public static int InsetWidth
        {
            get { return _insetWidth; }
            set { _insetWidth = value; }
        }

        /// <summary>
        /// Gets this <see cref="Tile"/>'s normalized rectangle.
        /// </summary>
        /// <remarks>
        /// Normalized coordinates specify the top-left corner,
        /// width and height of the <see cref="Tile"/> as a 
        /// fraction of the image box.  For example, if the
        /// <see cref="NormalizedRectangle"/> is (left=0.25, top=0.0, width=0.5, height=0.5) 
        /// and the image box has dimensions of (width=1000, height=800), the 
        /// <see cref="Tile"/> rectangle would be (left=250, top=0, width=500, height=400)
        /// </remarks>
        public RectangleF NormalizedRectangle
        {
            get { return _normalizedRectangle; }
            internal set { _normalizedRectangle = value; }
        }

        /// <summary>
        /// Gets this <see cref="Tile"/>'s client rectangle.
        /// </summary>
        public Rectangle ClientRectangle
        {
            get { return _clientRectangle; }
        }

        /// <summary>
        /// Gets or sets this <see cref="Tile"/>'s <see cref="Macro.ImageViewer.InformationBox">InformationBox</see>.
        /// </summary>
        public InformationBox InformationBox
        {
            get { return _informationBox; }
            set
            {
                if (_informationBox == value)
                    return;

                _informationBox = value;
                EventsHelper.Fire(_informationBoxChanged, this, new InformationBoxChangedEventArgs(_informationBox));
            }
        }

        /// <summary>
        /// Gets or sets this <see cref="Tile"/>'s <see cref="Macro.ImageViewer.EditBox">EditBox</see>.
        /// </summary>
        public EditBox EditBox
        {
            get { return _editBox; }
            set
            {
                if (_editBox == value)
                    return;

                _editBox = value;
                EventsHelper.Fire(_editBoxChanged, this, new EventArgs());
            }
        }

        public Common.ExtensionData ExtensionData { get; private set; }

        #endregion

        #region Public events

        /// <summary>
        /// Occurs when the <see cref="IRenderer"/> of a new 
        /// <see cref="PresentationImage"/> is different from that of 
        /// the previous <see cref="PresentationImage"/> that occupied this
        /// <see cref="Tile"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When a different <see cref="IPresentationImage"/> occupies this
        /// <see cref="Tile"/>, it's possible that the image renderer maybe different
        /// as well and the view needs to know this.
        /// </para>
        /// <para>
        /// For internal Framework use only.
        /// </para>
        /// </remarks>
        public event EventHandler RendererChanged
        {
            add { _rendererChangedEvent += value; }
            remove { _rendererChangedEvent -= value; }
        }

        /// <summary>
        /// Occurs when the <see cref="Tile"/> is about to be drawn.
        /// </summary>
        public event EventHandler Drawing
        {
            add { _drawingEvent += value; }
            remove { _drawingEvent -= value; }
        }

        /// <summary>
        /// Occurs when the <see cref="Selected"/> property has changed.
        /// </summary>
        public event EventHandler<ItemEventArgs<ITile>> SelectionChanged
        {
            add { _selectionChangedEvent += value; }
            remove { _selectionChangedEvent -= value; }
        }

        /// <summary>
        /// Occurs when the <see cref="PresentationImage"/> property has changed.
        /// </summary>
        public event EventHandler<PresentationImageChangedEventArgs> PresentationImageChanged
        {
            add { _presentationImageChangedEvent += value; }
            remove { _presentationImageChangedEvent -= value; }
        }

        /// <summary>
        /// Occurs when the <see cref="InformationBox"/> property has changed.
        /// </summary>
        public event EventHandler<InformationBoxChangedEventArgs> InformationBoxChanged
        {
            add { _informationBoxChanged += value; }
            remove { _informationBoxChanged -= value; }
        }

        /// <summary>
        /// Occurs when the <see cref="EditBox"/> property has changed.
        /// </summary>
        public event EventHandler EditBoxChanged
        {
            add { _editBoxChanged += value; }
            remove { _editBoxChanged -= value; }
        }

        public event EventHandler<TileContextMenuRequestEventArgs> ContextMenuRequested
        {
            add { _contextMenuRequested += value; }
            remove { _contextMenuRequested -= value; }
        }

        #endregion

        #region Disposal

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by this <see cref="Tile"/>.
        /// </summary>
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

        #endregion

        /// <summary>
        /// Implementation of the <see cref="IDisposable"/> pattern
        /// </summary>
        /// <param name="disposing">True if this object is being disposed, false if it is being finalized</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ExtensionData != null)
                {
                    ExtensionData.Dispose();
                    ExtensionData = null;
                }
            }
        }

        #endregion

        #region Public methods

        public void ShowContextMenu(Point? tileLocation, ActionModelNode actionModel)
        {
            EventsHelper.Fire(_contextMenuRequested, this, new TileContextMenuRequestEventArgs(tileLocation, actionModel));
        }

        /// <summary>
        /// Selects the <see cref="Tile"/>.
        /// </summary>
        /// <remarks>
        /// Selecting a <see cref="Tile"/> also selects the containing <see cref="ImageBox"/>
        /// and deselects any other currently seleccted <see cref="Tile"/> 
        /// and <see cref="ImageBox"/>.
        /// </remarks>
        public void Select(XKeys key = XKeys.None, XMouseButtons button = XMouseButtons.Left)
        {
            //if (!this.Selected)
            //{
            Platform.CheckMemberIsSet(this.ParentImageBox, "Tile.ParentImageBox");
            Platform.CheckMemberIsSet(this.ImageViewer, "Tile.ImageViewer");

            if (button == XMouseButtons.Right)
            {
                if (_presentationImage == null)
                {
                    return;
                }
                if (!_parentImageBox.SelectPresentationImages.Contains(_presentationImage))
                {
                    foreach (var presentationImage in _parentImageBox.SelectPresentationImages)
                    {
                        presentationImage.Selected = false;
                    }
                    _parentImageBox.SelectPresentationImages.Clear();
                    _presentationImage.Selected = true;
                    _parentImageBox.SelectPresentationImages.Add(_presentationImage);
                }

                this.Selected = true;
                _parentImageBox.SelectedTile = this;
                this.ImageViewer.EventBroker.OnTileSelected(new TileSelectedEventArgs(this));
                return;
            }

            if (key == XKeys.None && _parentImageBox.SelectedTile != null)
            {
                var tile = _parentImageBox.SelectedTile as PrintViewTile;
                tile.Selected = false;
                if (_parentImageBox.SelectTiles.Contains(tile))
                {
                    _parentImageBox.SelectTiles.Remove(tile);
                }
            }

            if (key == XKeys.None)
            {
                foreach (var presentationImage in _parentImageBox.SelectPresentationImages)
                {
                    presentationImage.Selected = false;
                }
                _parentImageBox.SelectPresentationImages.Clear();
                foreach (var tile in _parentImageBox.SelectTiles)
                {
                    tile.Selected = false;
                }
                _parentImageBox.SelectTiles.Clear();

                if (_presentationImage != null)
                {
                    _presentationImage.Selected = true;
                    _parentImageBox.SelectPresentationImages.Add(_presentationImage);
                }
                this.Selected = true;
                _parentImageBox.SelectTiles.Add(this);
                _parentImageBox.SelectedTile = this;
            }
            else if (key == XKeys.Control)
            {
                if (_presentationImage != null)
                {
                    if (_parentImageBox.SelectPresentationImages.Contains(_presentationImage))
                    {
                        _presentationImage.Selected = false;
                        _parentImageBox.SelectPresentationImages.Remove(_presentationImage);
                        this.Selected = false;
                        if (_parentImageBox.SelectTiles.Contains(this))
                        {
                            _parentImageBox.SelectTiles.Remove(this);
                        }

                        if (_parentImageBox.SelectPresentationImages.Count == 0)
                        {
                            var title = _parentImageBox.Tiles[0] as PrintViewTile;
                            if (title._presentationImage != null)
                            {
                                title._presentationImage.Selected = true;
                                _parentImageBox.SelectPresentationImages.Add(title._presentationImage);
                            }
                            title.Selected = true;
                            _parentImageBox.SelectedTile = title;
                            if (!_parentImageBox.SelectTiles.Contains(title))
                            {
                                _parentImageBox.SelectTiles.Add(title);
                            }


                        }
                        else
                        {
                            foreach (var image in _parentImageBox.SelectPresentationImages)
                            {
                                if (image.Tile != null && _parentImageBox.SelectedTile != image.Tile)
                                {

                                    var title = image.Tile as PrintViewTile;
                                    title.Selected = true;
                                    _parentImageBox.SelectedTile = title;
                                    if (!_parentImageBox.SelectTiles.Contains(title))
                                    {
                                        _parentImageBox.SelectTiles.Add(title);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        _presentationImage.Selected = true;
                        _parentImageBox.SelectPresentationImages.Add(_presentationImage);
                        this.Selected = true;
                        _parentImageBox.SelectedTile = this;
                        if (!_parentImageBox.SelectTiles.Contains(this))
                        {
                            _parentImageBox.SelectTiles.Add(this);
                        }
                    }
                }
                else
                {
                    if (_parentImageBox.SelectTiles.Contains(this))
                    {
                        this.Selected = false;
                        _parentImageBox.SelectTiles.Remove(this);
                        if (_parentImageBox.SelectTiles.Count > 0)
                        {
                            _parentImageBox.SelectedTile = _parentImageBox.SelectTiles[0];
                        }
                        else
                        {
                            var title = _parentImageBox.Tiles[0] as PrintViewTile;
                            title.Selected = true;
                            _parentImageBox.SelectedTile = title;
                            if (!_parentImageBox.SelectTiles.Contains(title))
                            {
                                _parentImageBox.SelectTiles.Add(title);
                            }
                        }
                    }
                    else
                    {
                        this.Selected = true;
                        if (!_parentImageBox.SelectTiles.Contains(this))
                        {
                            _parentImageBox.SelectTiles.Add(this);
                        }
                        _parentImageBox.SelectedTile = this;
                    }
                }
            }
            else if (key == XKeys.Shift)
            {
                if (_presentationImage != null)
                {
                    if (_parentImageBox.SelectPresentationImages.Count == 0)
                    {
                        _presentationImage.Selected = true;
                        _parentImageBox.SelectPresentationImages.Add(_presentationImage);
                    }
                    else
                    {
                        int start =
                            _parentImageBox.DisplaySet.PresentationImages.IndexOf(
                                _parentImageBox.SelectPresentationImages[
                                    _parentImageBox.SelectPresentationImages.Count - 1]);
                        int end = _parentImageBox.DisplaySet.PresentationImages.IndexOf(_presentationImage);

                        if (start > end)
                        {
                            int temp = start;
                            start = end;
                            end = temp;
                        }

                        for (int i = start; i <= end; i++)
                        {
                            PresentationImage pimage = _parentImageBox.DisplaySet.PresentationImages[i] as PresentationImage;
                            if (!_parentImageBox.SelectPresentationImages.Contains(pimage))
                            {
                                pimage.Selected = true;
                                _parentImageBox.SelectPresentationImages.Add(pimage);
                            }
                            if (pimage.Tile != null)
                            {
                                var mytitle = pimage.Tile as PrintViewTile;
                                mytitle.Selected = true;
                                if (!_parentImageBox.SelectTiles.Contains(mytitle))
                                {
                                    _parentImageBox.SelectTiles.Add(mytitle);
                                }
                            }
                        }


                    }
                    Selected = true;
                    _parentImageBox.SelectedTile = this;
                    if (!_parentImageBox.SelectTiles.Contains(this))
                    {
                        _parentImageBox.SelectTiles.Add(this);
                    }
                }
                else
                {

                    if (_parentImageBox.SelectTiles.Count == 0)
                    {
                        this.Selected = true;
                        _parentImageBox.SelectTiles.Add(this);
                        _parentImageBox.SelectedTile = this;
                    }
                    else
                    {
                        int start =
                            _parentImageBox.Tiles.IndexOf(_parentImageBox.SelectTiles[_parentImageBox.SelectTiles.Count - 1]);
                        int end = _parentImageBox.Tiles.IndexOf(this);
                        if (start > end)
                        {
                            int temp = start;
                            start = end;
                            end = temp;
                        }

                        for (int i = start; i <= end; i++)
                        {
                            var tile = _parentImageBox.Tiles[i] as PrintViewTile;
                            tile.Selected = true;
                            if (!_parentImageBox.SelectTiles.Contains(tile))
                            {
                                _parentImageBox.SelectTiles.Add(tile);
                            }
                        }

                        this.Selected = true;
                        _parentImageBox.SelectedTile = this;
                    }
                }
            }



            //if (_presentationImage != null)
            //{
            //    if (key == XKeys.None)
            //    {
            //        foreach (var presentationImage in _parentImageBox.SelectPresentationImages)
            //        {
            //            presentationImage.Selected = false;
            //        }
            //        _parentImageBox.SelectPresentationImages.Clear();
            //        _presentationImage.Selected = true;
            //        _parentImageBox.SelectPresentationImages.Add(_presentationImage);
            //        this.Selected = true;
            //        _parentImageBox.SelectedTile = this;
            //    }
            //    else if (key == XKeys.Control)
            //    {
            //        if (_parentImageBox.SelectPresentationImages.Contains(_presentationImage))
            //        {
            //            _presentationImage.Selected = false;
            //            _parentImageBox.SelectPresentationImages.Remove(_presentationImage);
            //            this.Selected = false;
            //            if (_parentImageBox.SelectPresentationImages.Count == 0)
            //            {
            //                _parentImageBox.SelectDefaultTile();
            //            }
            //            else
            //            {
            //                foreach (var image in _parentImageBox.SelectPresentationImages)
            //                {
            //                    if (image.Tile != null && _parentImageBox.SelectedTile != image.Tile)
            //                    {
            //                        _parentImageBox.SelectedTile = image.Tile;
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            _presentationImage.Selected = true;
            //            _parentImageBox.SelectPresentationImages.Add(_presentationImage);
            //            this.Selected = true;
            //            _parentImageBox.SelectedTile = this;
            //        }
            //    }
            //    else if (key == XKeys.Shift)
            //    {
            //        if (_parentImageBox.SelectPresentationImages.Count == 0)
            //        {
            //            _presentationImage.Selected = true;
            //            _parentImageBox.SelectPresentationImages.Add(_presentationImage);
            //        }
            //        else
            //        {
            //            int start =
            //                _parentImageBox.DisplaySet.PresentationImages.IndexOf(
            //                    _parentImageBox.SelectPresentationImages[
            //                        _parentImageBox.SelectPresentationImages.Count - 1]);
            //            int end = _parentImageBox.DisplaySet.PresentationImages.IndexOf(_presentationImage);

            //            if (start > end)
            //            {
            //                int temp = start;
            //                start = end;
            //                end = temp;
            //            }

            //            for (int i = start; i <= end; i++)
            //            {
            //                PresentationImage pimage = _parentImageBox.DisplaySet.PresentationImages[i] as PresentationImage;
            //                if (!_parentImageBox.SelectPresentationImages.Contains(pimage))
            //                {
            //                    pimage.Selected = true;
            //                    _parentImageBox.SelectPresentationImages.Add(pimage);
            //                }
            //            }
            //        }

            //        this.Selected = true;
            //        _parentImageBox.SelectedTile = this;
            //    }

            //}
            //else
            //{
            //    if (key == XKeys.None)
            //    {
            //        foreach (var presentationImage in _parentImageBox.SelectPresentationImages)
            //        {
            //            presentationImage.Selected = false;
            //        }
            //        _parentImageBox.SelectPresentationImages.Clear();
            //    }

            //}


            this.ImageViewer.EventBroker.OnTileSelected(new TileSelectedEventArgs(this));
            //}
        }

        /// <summary>
        /// Draws the <see cref="PresentationImage"/> in this <see cref="Tile"/>.
        /// </summary>
        public void Draw()
        {
            EventsHelper.Fire(_drawingEvent, this, EventArgs.Empty);
        }

        /// <summary>
        /// Draws the <see cref="PresentationImage"/> in this <see cref="Tile"/>.
        /// </summary>
        /// <param name="drawArgs"></param>
        /// <remarks>This is called by the GUI control associated with this
        /// component.  Never call this method directly.  Instead, use
        /// <see cref="Draw()"/>.</remarks>
        /// <exception cref="RenderingException">Thrown if any <see cref="Exception"/> is encountered while rendering the image.</exception>
        public void Draw(DrawArgs drawArgs)
        {
            // No PresentationImage associated with this Tile, so nothing to draw
            if (_presentationImage == null)
                return;

            _clientRectangle = drawArgs.RenderingSurface.ClientRectangle;
            _presentationImage.Draw(drawArgs);
        }

        #endregion

        #region Internal/private methods

        public void Deselect()
        {
            if (this.Selected)
            {
                this.Selected = false;

                if (_presentationImage != null)
                    _presentationImage.Selected = false;
            }
        }

        #endregion

        #region IPrintViewTile 成员

        public IPrintViewTile ParentPrintViewTile
        {
            get { return _parentPrintViewTile; }
            set { _parentPrintViewTile = value; }
        }

        public bool isSubTile
        {
            get { return _isSubTile; }
            set { _isSubTile = value; }
        }

        public TileCollection SubTiles
        {
            get
            {

                if (_isSubTile)
                {
                    return null;
                }

                if (_subTileCollection == null)
                {
                    _subTileCollection = new TileCollection();
                }

                return _subTileCollection;
            }
        }

        #endregion
    }
}
