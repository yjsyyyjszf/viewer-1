
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

namespace Macro.ImageViewer
{
    public interface IPrintViewImageBox : IImageBox
    {

        /// <summary>
        /// 父节点
        /// </summary>
        IImageBox ParentImageBox { get; set; }

        /// <summary>
        /// 上一个节点
        /// </summary>
        IImageBox UpNodeImageBox { get; set; }

        /// <summary>
        /// 下一个节点
        /// </summary>
        IImageBox NextNodeImageBox { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        ImageBoxCollection ChildImageBox { get; }

        /// <summary>
        /// 选择的所有图像
        /// </summary>
        List<PresentationImage> SelectPresentationImages { get; }
        /// <summary>
        /// 选择的Tile
        /// </summary>
        List<PrintViewTile> SelectTiles { get; }
      
    }
}
