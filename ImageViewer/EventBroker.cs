
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
using Macro.Common.Utilities;
using Macro.ImageViewer.Graphics;
using Macro.ImageViewer.InputManagement;
using Macro.ImageViewer.StudyManagement;

namespace Macro.ImageViewer
{
    /// <summary>
    /// A central place from where image viewer events are raised.
    /// </summary>
    public class EventBroker
    {

        #region Private fields

        private event EventHandler<ImageDrawingEventArgs> _imageDrawingEvent;
        private event EventHandler<ImageBoxDrawingEventArgs> _imageBoxDrawingEvent;
        private event EventHandler<ImageBoxSelectedEventArgs> _imageBoxSelectedEvent;
        private event EventHandler<DisplaySetSelectedEventArgs> _displaySetSelectedEvent;
        private event EventHandler<TileSelectedEventArgs> _tileSelectedEvent;
        private event EventHandler<PresentationImageSelectedEventArgs> _presentationImageSelectedEvent;

        private event EventHandler<GraphicSelectionChangedEventArgs> _graphicSelectionChangedEvent;
        private event EventHandler<GraphicFocusChangedEventArgs> _graphicFocusChangedEvent;

        private event EventHandler<StudyLoadedEventArgs> _studyLoadedEvent;
        private event EventHandler<StudyLoadFailedEventArgs> _studyLoadFailedEvent;

        private event EventHandler<ItemEventArgs<Sop>> _imageLoadedEvent;

        private event EventHandler<MouseCaptureChangedEventArgs> _mouseCaptureChanged;
        private event EventHandler<MouseWheelCaptureChangedEventArgs> _mouseWheelCaptureChanged;

        private event EventHandler<DisplaySetChangingEventArgs> _displaySetChanging;
        private event EventHandler<DisplaySetChangedEventArgs> _displaySetChanged;

        private event EventHandler<CloneCreatedEventArgs> _cloneCreated;
        private event EventHandler _layoutCompletedEvent;

        private event EventHandler _cutEvent;
        private event EventHandler _copyEvent;
        private event EventHandler _pasteEvent;
        private event EventHandler _selectAll;
        private event EventHandler _selectRever;
        private event EventHandler _deleteCurrentPage;
        private event EventHandler _createEmptyPage;
        private event EventHandler _firstPage;
        private event EventHandler _lastPage;
        private event EventHandler _upPage;
        private event EventHandler _downPage;

        private event EventHandler _selectTool;

        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="EventBroker"/>.
        /// </summary>
        public EventBroker()
        {

        }

        public event EventHandler SelectToolEvent
        {
            add { _selectTool += value; }
            remove { _selectTool -= value; }
        }

        public EventHandler DelegateSelectTool
        {
            get { return _selectTool; }
        }

        public event EventHandler FirstPageEvent
        {
            add { _firstPage += value; }
            remove { _firstPage -= value; }
        }

        public EventHandler DelegateFirstPage
        {
            get { return _firstPage; }
        }

        public event EventHandler LastPageEvent
        {
            add { _lastPage += value; }
            remove { _lastPage -= value; }
        }

        public EventHandler DelegateLastPage
        {
            get { return _lastPage; }
        }

        public event EventHandler UpPageEvent
        {
            add { _upPage += value; }
            remove { _upPage -= value; }
        }

        public EventHandler DelegateUpPage
        {
            get { return _upPage; }
        }

        public event EventHandler DownPageEvent
        {
            add { _downPage += value; }
            remove { _downPage -= value; }
        }

        public EventHandler DelegateDownPage
        {
            get { return _downPage; }
        }

        public event EventHandler CreateEmptyPage
        {
            add { _createEmptyPage += value; }
            remove { _createEmptyPage -= value; }
        }

        public EventHandler DelegateCreateEmptyPage
        {
            get { return _createEmptyPage; }
        }

        public event EventHandler CurrentPageEvent
        {
            add { _deleteCurrentPage += value; }
            remove { _deleteCurrentPage -= value; }
        }

        public EventHandler DelegateDeleteCurrentPage
        {
            get { return _deleteCurrentPage; }
        }

        public event EventHandler CutEvent
        {
            add { _cutEvent += value; }
            remove { _cutEvent -= value; }
        }

        public EventHandler DelegateCutEvent
        {
            get { return _cutEvent; }
        }

        public event EventHandler CopyEvent
        {
            add { _copyEvent += value; }
            remove { _copyEvent -= value; }
        }

        public EventHandler DelegateCopyEvent
        {
            get { return _copyEvent; }
        }

        public event EventHandler PasteEvent
        {
            add { _pasteEvent += value; }
            remove { _pasteEvent -= value; }
        }

        public EventHandler DelegatePasteEvent
        {
            get { return _pasteEvent; }
        }

        public event EventHandler SelectAll
        {
            add { _selectAll += value; }
            remove { _selectAll -= value; }
        }

        public EventHandler DelegateSelectAll
        {
            get { return _selectAll; }
        }

        public event EventHandler SelectRever
        {
            add { _selectRever += value; }
            remove { _selectRever -= value; }
        }

        public EventHandler DelegateSelectRever
        {
            get { return _selectRever; }
        }

        /// <summary>
        /// Occurs when a <see cref="PresentationImage"/> is about to be drawn.
        /// </summary>
        public event EventHandler<ImageDrawingEventArgs> ImageDrawing
        {
            add { _imageDrawingEvent += value; }
            remove { _imageDrawingEvent -= value; }
        }

        public void OnImageDrawing(ImageDrawingEventArgs args)
        {
            EventsHelper.Fire(_imageDrawingEvent, this, args);
        }

        /// <summary>
        /// Occurs when a <see cref="IImageBox"/> is about to be drawn.
        /// </summary>
        public event EventHandler<ImageBoxDrawingEventArgs> ImageBoxDrawing
        {
            add { _imageBoxDrawingEvent += value; }
            remove { _imageBoxDrawingEvent -= value; }
        }

        public void OnImageBoxDrawing(ImageBoxDrawingEventArgs args)
        {
            EventsHelper.Fire(_imageBoxDrawingEvent, this, args);
        }

        /// <summary>
        /// Occurs when an <see cref="IImageBox"/> is selected.
        /// </summary>
        public event EventHandler<ImageBoxSelectedEventArgs> ImageBoxSelected
        {
            add { _imageBoxSelectedEvent += value; }
            remove { _imageBoxSelectedEvent -= value; }
        }

        public void OnImageBoxSelected(ImageBoxSelectedEventArgs args)
        {
            EventsHelper.Fire(_imageBoxSelectedEvent, this, args);
        }

        /// <summary>
        /// Occurs when an <see cref="IDisplaySet"/> is selected.
        /// </summary>
        public event EventHandler<DisplaySetSelectedEventArgs> DisplaySetSelected
        {
            add { _displaySetSelectedEvent += value; }
            remove { _displaySetSelectedEvent -= value; }
        }

        public void OnDisplaySetSelected(DisplaySetSelectedEventArgs args)
        {
            EventsHelper.Fire(_displaySetSelectedEvent, this, args);
        }

        /// <summary>
        /// Occurs when an <see cref="ITile"/> is selected.
        /// </summary>
        public event EventHandler<TileSelectedEventArgs> TileSelected
        {
            add { _tileSelectedEvent += value; }
            remove { _tileSelectedEvent -= value; }
        }

        public void OnTileSelected(TileSelectedEventArgs args)
        {
            EventsHelper.Fire(_tileSelectedEvent, this, args);
        }

        /// <summary>
        /// Occurs when an <see cref="IPresentationImage"/> is selected.
        /// </summary>
        public event EventHandler<PresentationImageSelectedEventArgs> PresentationImageSelected
        {
            add { _presentationImageSelectedEvent += value; }
            remove { _presentationImageSelectedEvent -= value; }
        }

        public void OnPresentationImageSelected(PresentationImageSelectedEventArgs args)
        {
            EventsHelper.Fire(_presentationImageSelectedEvent, this, args);
        }

        /// <summary>
        /// Occurs when the selected <see cref="IGraphic"/> in the currently selected
        /// <see cref="PresentationImage"/>'s scene graph has changed.
        /// </summary>
        public event EventHandler<GraphicSelectionChangedEventArgs> GraphicSelectionChanged
        {
            add { _graphicSelectionChangedEvent += value; }
            remove { _graphicSelectionChangedEvent -= value; }
        }

        public void OnGraphicSelectionChanged(GraphicSelectionChangedEventArgs args)
        {
            EventsHelper.Fire(_graphicSelectionChangedEvent, this, args);
        }

        /// <summary>
        /// Occurs when the focused <see cref="IGraphic"/> in the currently selected
        /// <see cref="PresentationImage"/>'s scene graph has changed.
        /// </summary>
        public event EventHandler<GraphicFocusChangedEventArgs> GraphicFocusChanged
        {
            add { _graphicFocusChangedEvent += value; }
            remove { _graphicFocusChangedEvent -= value; }
        }

        public void OnGraphicFocusChanged(GraphicFocusChangedEventArgs args)
        {
            EventsHelper.Fire(_graphicFocusChangedEvent, this, args);
        }

        /// <summary>
        /// Occurs when a DICOM study is loaded.
        /// </summary>
        public event EventHandler<StudyLoadedEventArgs> StudyLoaded
        {
            add { _studyLoadedEvent += value; }
            remove { _studyLoadedEvent -= value; }
        }

        public event EventHandler LayoutCompleted
        {
            add { _layoutCompletedEvent += value; }
            remove { _layoutCompletedEvent -= value; }
        }

        public void OnLayoutCompleted()
        {
            EventsHelper.Fire(_layoutCompletedEvent, this, EventArgs.Empty);
        }

        public void OnStudyLoaded(StudyLoadedEventArgs studyLoadedArgs)
        {
            EventsHelper.Fire(_studyLoadedEvent, this, studyLoadedArgs);
        }

        /// <summary>
        /// Occurs when a DICOM study has failed to load.
        /// </summary>
        public event EventHandler<StudyLoadFailedEventArgs> StudyLoadFailed
        {
            add { _studyLoadFailedEvent += value; }
            remove { _studyLoadFailedEvent -= value; }
        }

        public void OnStudyLoadFailed(StudyLoadFailedEventArgs studyLoadFailedArgs)
        {
            EventsHelper.Fire(_studyLoadFailedEvent, this, studyLoadFailedArgs);
        }

        /// <summary>
        /// Occurs when a DICOM image is loaded.
        /// </summary>
        public event EventHandler<ItemEventArgs<Sop>> ImageLoaded
        {
            add { _imageLoadedEvent += value; }
            remove { _imageLoadedEvent -= value; }
        }

        internal void OnImageLoaded(ItemEventArgs<Sop> sopEventArgs)
        {
            EventsHelper.Fire(_imageLoadedEvent, this, sopEventArgs);
        }

        /// <summary>
        /// Occurs when an object has gained or lost mouse capture.
        /// </summary>
        public event EventHandler<MouseCaptureChangedEventArgs> MouseCaptureChanged
        {
            add { _mouseCaptureChanged += value; }
            remove { _mouseCaptureChanged -= value; }
        }

        public void OnMouseCaptureChanged(MouseCaptureChangedEventArgs args)
        {
            EventsHelper.Fire(_mouseCaptureChanged, this, args);
        }

        /// <summary>
        /// Occurs when an object has gained or lost mouse wheel capture.
        /// </summary>
        public event EventHandler<MouseWheelCaptureChangedEventArgs> MouseWheelCaptureChanged
        {
            add { _mouseWheelCaptureChanged += value; }
            remove { _mouseWheelCaptureChanged -= value; }
        }

        public void OnMouseWheelCaptureChanged(MouseWheelCaptureChangedEventArgs args)
        {
            EventsHelper.Fire(_mouseWheelCaptureChanged, this, args);
        }

        /// <summary>
        /// Occurs when a display set is about to change.
        /// </summary>
        public event EventHandler<DisplaySetChangingEventArgs> DisplaySetChanging
        {
            add { _displaySetChanging += value; }
            remove { _displaySetChanging -= value; }
        }

        public void OnDisplaySetChanging(DisplaySetChangingEventArgs args)
        {
            EventsHelper.Fire(_displaySetChanging, this, args);
        }

        /// <summary>
        /// Occurs when a display set has changed.
        /// </summary>
        public event EventHandler<DisplaySetChangedEventArgs> DisplaySetChanged
        {
            add { _displaySetChanged += value; }
            remove { _displaySetChanged -= value; }
        }

        public void OnDisplaySetChanged(DisplaySetChangedEventArgs args)
        {
            EventsHelper.Fire(_displaySetChanged, this, args);
        }

        /// <summary>
        /// Fires when objects are cloned; only certain objects
        /// publish the fact that they have been cloned.
        /// </summary>
        public event EventHandler<CloneCreatedEventArgs> CloneCreated
        {
            add { _cloneCreated += value; }
            remove { _cloneCreated -= value; }
        }

        public void OnCloneCreated(CloneCreatedEventArgs args)
        {
            EventsHelper.Fire(_cloneCreated, this, args);
        }
    }
}
