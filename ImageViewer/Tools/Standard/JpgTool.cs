
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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Macro.Common;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.ImageViewer.BaseTools;
using Macro.ImageViewer.Graphics;
using Macro.ImageViewer.ImageExport;

namespace Macro.ImageViewer.Tools.Standard
{
    [ButtonAction("activate", "global-toolbars/ToolbarStandard/ToolbarJpg", "DcmToBmp")]
    [Tooltip("activate", "TooltipJpg")]
    [IconSet("activate", "Icons.SmallJPG.png", "Icons.MiddleJPG.png", "Icons.MaxJPG.png")]
    [EnabledStateObserver("activate", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(ImageViewerToolExtensionPoint))]
    public class JpgTool : ImageViewerTool
    {

        private void DcmToBmp()
        {
            if (this.Context.Viewer != null)
            {
                var arg = new SelectFolderDialogCreationArgs("c:\\");
                arg.AllowCreateNewFolder = true;
                arg.Prompt = "选择bmp图像存储目录";
                var result = this.Context.DesktopWindow.ShowSelectFolderDialogBox(arg);
                if (result.Action != DialogBoxAction.Ok)
                    return;
                try
                {
                    int i = 0;
                    foreach (var presentationImage in this.SelectedPresentationImage.ParentDisplaySet.PresentationImages)
                    {
                        var parm = new ExportImageParams();
                        parm.ExportOption = ExportOption.CompleteImage;
                        parm.SizeMode = SizeMode.ScaleToFit;
                        parm.DisplayRectangle = this.SelectedPresentationImage.ClientRectangle;
                        parm.Dpi = 96;
                        parm.Scale = 1;
                        parm.OutputSize = new Size(SelectedPresentationImage.ClientRectangle.Width, SelectedPresentationImage.ClientRectangle.Height);
                        var bmp = ImageExporter.DrawToBitmap(presentationImage, parm);
                        string filename = System.IO.Path.Combine(result.FileName, string.Format("{0}.bmp", i++));
                        bmp.Save(filename);
                    }
                    this.Context.DesktopWindow.ShowMessageBox("Bmp图像导出完成",MessageBoxActions.Ok);
                }
                catch (Exception e)
                {
                    ExceptionHandler.Report(e, this.Context.DesktopWindow);
                }

            }
        }
    }
}
