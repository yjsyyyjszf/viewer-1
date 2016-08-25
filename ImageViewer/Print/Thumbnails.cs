
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


using System.Collections.Generic;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Trees;
using Macro.ImageViewer.StudyManagement;

namespace Macro.ImageViewer
{


    public class Thumbnails
    {
        #region For Item Binding

        private class ThumbnailTreeItemBinding : DisplaySetTreeItemBingding
        {
            private readonly string _primaryStudyInstanceUid;
            private readonly IPrintViewImageViewer _owner;

            public ThumbnailTreeItemBinding(IPrintViewImageViewer owner, string primaryStudyInstanceUid)
            {
                _owner = owner;
                _primaryStudyInstanceUid = primaryStudyInstanceUid;
            }

            public override IconSet GetIconSet(object item)
            {
                var imageSetItem = item as ImageSetTreeItem;
                if (imageSetItem != null)
                {
                    if (imageSetItem.ImageSet.Uid == _primaryStudyInstanceUid)
                    {
                        return new IconSet("PrimaryImageSet.png");
                    }
                }

                return base.GetIconSet(item);
            }


            public override IResourceResolver GetResourceResolver(object item)
            {
                return _owner.ResourceResolver;
            }
        }

        #endregion

        private static readonly List<Macro.ImageViewer.IImageViewer> _viewerTrees = new List<Macro.ImageViewer.IImageViewer>();

        private DisplaySetTree _displaySetTree = null;
        private IPrintViewImageViewer _dicomPrintPreviewComponent;

        public Thumbnails(IDesktopWindow desktopWindow, IPrintViewImageViewer dicomPrintPreviewComponent)
        {
            _dicomPrintPreviewComponent = dicomPrintPreviewComponent;
            Initialize(desktopWindow);

        }

        private void Initialize(IDesktopWindow desktopWindow)
        {
            Macro.ImageViewer.IImageViewer imageViewer = CastToImageViewer(desktopWindow.ActiveWorkspace);

            if (!_viewerTrees.Contains(imageViewer))
            {
                var imageSets1 = imageViewer.LogicalWorkspace.ImageSets;
                //ObservableList<IImageSet> imageSets = new ObservableList<IImageSet>();
                //foreach (var imageSet in imageSets1)
                //{
                //    ImageSet tempImageSet = new ImageSet();
                //    foreach (var displaySet in imageSet.DisplaySets)
                //    {
                //        tempImageSet.DisplaySets.Add(displaySet.Clone());
                //    }
                //    imageSets.Add(tempImageSet);
                //}
                string primaryStudyInstanceUid = GetPrimaryStudyInstanceUid(imageViewer.StudyTree);
                if (_displaySetTree == null)
                {
                    _displaySetTree = new DisplaySetTree(imageSets1, new ThumbnailTreeItemBinding(_dicomPrintPreviewComponent, primaryStudyInstanceUid));
                }
                else
                {
                    _displaySetTree.AddTreeItem(imageSets1);
                }

                _viewerTrees.Add(imageViewer);
            }
        }

        public ITree Tree
        {
            get { return _displaySetTree.TreeRoot; }
        }

        public ISelection Selection
        {
            get { return _displaySetTree.Selection; }
            set { _displaySetTree.Selection = value; }
        }

        public void AddTreeItem(IDesktopWindow desktopWindow)
        {
            Initialize(desktopWindow);
        }

        private static string GetPrimaryStudyInstanceUid(StudyTree studyTree)
        {
            foreach (Patient patient in studyTree.Patients)
            {
                foreach (Study study in patient.Studies)
                {
                    return study.StudyInstanceUid;
                }
            }

            return null;
        }

        private static Macro.ImageViewer.IImageViewer CastToImageViewer(Workspace workspace)
        {
            Macro.ImageViewer.IImageViewer viewer = null;
            if (workspace != null)
                viewer = ImageViewerComponent.GetAsImageViewer(workspace);

            return viewer;
        }

        public IEnumerable<IDisplaySet> DisplaySet
        {
            get
            {
                foreach (var imageSet in _displaySetTree.ImageSets)
                {
                    foreach (var displaySet in imageSet.DisplaySets)
                    {
                        yield return displaySet.Clone();
                    }
                }
            }
        }

        public void Dispose()
        {
            _viewerTrees.Clear();
            if (_displaySetTree != null)
            {
                _displaySetTree.Dispose();
            }

        }
    }
}
