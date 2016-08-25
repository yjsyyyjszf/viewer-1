
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
using Macro.ImageViewer.ImageExport;

namespace Macro.ImageViewer
{
    public static class PrintPresentationImagesHost
    {
        /// <summary>
        /// 所有要打印的图像都保存在这个<see cref="DisplaySet"/>中
        /// </summary>

        public static void AddPresentationImage(IPresentationImage presentationImage)
        {
            if (presentationImage == null)
            {
                return;
            }
            var clonPi = ImageExporter.ClonePresentationImage(presentationImage);
            if (clonPi != null)
            {
                PresentationImage image = clonPi as PresentationImage;
                if (_imageViewerComponent != null)
                {
                    _imageViewerComponent.DisplaySet.PresentationImages.Add(image);
                    image.Selected = false;
                }
            }
        }

        private static PrintImageViewerComponent _imageViewerComponent;

        public static PrintImageViewerComponent ImageViewerComponent
        {
            get { return _imageViewerComponent; }
            set { _imageViewerComponent = value; }
        }



    }
}
