
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
using System.Drawing;
using Macro.Common.Utilities;
using Macro.Dicom.Iod.Modules;
using Macro.ImageViewer.Graphics;

namespace Macro.ImageViewer
{
    public interface IPrintViewImageViewer : IImageViewer
    {

        /// <summary>
        ///事件代理 
        /// </summary>
        EventBroker EventBroker { get; }

        /// <summary>
        /// 删除PresensationImage
        /// </summary>
        /// <param name="tile">当前选中的Tile</param>
        /// <param name="isMove">是否前移</param>
        void Delete(ITile tile, bool isMove);

        /// <summary>
        /// 分格或者子分格
        /// </summary>
        /// <param name="tile">当前选中的Tile</param>
        /// <param name="isSubGrid">是否子分格</param>
        void SubGrid(ImageDisplayFormat DisplayFormat);

        /// <summary>
        /// 布局
        /// </summary>
        void Layout();

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="tile"></param>
        void Select(ITile tile);

        /// <summary>
        /// 根节点
        /// </summary>
        PrintViewImageBox RootImageBox { get; }

        /// <summary>
        /// 绘制图像
        /// </summary>
        void Draw();

        /// <summary>
        /// 工作区大小
        /// </summary>
        Rectangle WorkRectangle { get; }
        /// <summary>
        /// 设置布局信息
        /// </summary>
        void SetTileGrid(ImageDisplayFormat DisplayFormat);
        /// <summary>
        /// 根据配置文件设置布局
        /// </summary>
        /// <param name="fileName"></param>
        void SetGrid(string fileName);

        /// <summary>
        /// 合并单元格
        /// </summary>
        void MergerGrid();
        /// <summary>
        /// 保存排版布局
        /// </summary>
        void SaveGrid(string fileName);
        /// <summary>
        /// 事件命名空间
        /// </summary>
        string ActionsNamespace { get; }

        /// <summary>
        /// 要打印的所有图像，包括不同病人的图像
        /// </summary>
        PresentationImageCollection DisplayPresentationImages { get; }

        /// <summary>
        /// 打印的胶片数
        /// </summary>
        int FilmCount { get; }

        /// <summary>
        /// 图象的数目
        /// </summary>
        int ImageCount { get; }

        /// <summary>
        /// DICOM打印方法
        /// </summary>
        void Accept(bool isAllPage,bool printedDeleteImage);

        /// <summary>
        /// 资源获得
        /// </summary>
        IResourceResolver ResourceResolver { get; }

        /// <summary>
        /// 打印机组建
        /// </summary>
        IDicomPrintPreviewComponent DicomPrintComponent { get; }

        /// <summary>
        /// 保存所有打印图像
        /// </summary>
        IDisplaySet DisplaySet { get; }

        /// <summary>
        /// 清理所有图像
        /// </summary>
        void ClearAllImages();

        /// <summary>
        /// 选择的所有图像
        /// </summary>
        List<PresentationImage> SelectPresentationImages { get; }

        /// <summary>
        /// 控制预览胶片的大小和方向
        /// </summary>
        void ImageBoxSizeChanged(FilmSize filmSize, FilmOrientation filmOrientation);

        /// <summary>
        /// 体位图中包含的体位线
        /// </summary>
        Dictionary<IPresentationImage, List<IGraphic>> ReferenceLines { get; }

    }
}
