
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
using System.IO;
using Macro.Common;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.Desktop.Tools;
using Macro.ImageViewer.Configuration;
using Macro.ImageViewer.StudyManagement;

namespace Macro.ImageViewer.Tools.Standard
{
    [MenuAction("Open", "global-toolbars/MenuTools/MenuOpenFile", "Open")]
    [Tooltip("Open", "TooltipOpenFile")]
    [IconSet("Open", "Icons.DicomFileImportToolSmall.png", "Icons.DicomFileImportToolMedium.png", "Icons.DicomFileImportToolLarge.png")]
    [GroupHint("Open", "Application.Options")]


    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public class OpenFileTool : Tool<IDesktopToolContext>
    {

        public void Open()
        {
            SelectFolderDialogCreationArgs args = new SelectFolderDialogCreationArgs();
            args.AllowCreateNewFolder = false;
            args.Path = @"C:\";
            FileDialogResult result = this.Context.DesktopWindow.ShowSelectFolderDialogBox(args);
            if (result.FileNames.Length > 0)
            {
                List<string> files = BuildFileList(result.FileNames);
                new OpenFilesHelper(files) { WindowBehaviour = ViewerLaunchSettings.WindowBehaviour }.OpenFiles();

            }
        }

        private static List<string> BuildFileList(IEnumerable<string> files)
        {
            List<string> fileList = new List<string>();

            foreach (string path in files)
            {
                if (File.Exists(path))
                    fileList.Add(path);
                else if (Directory.Exists(path))
                    fileList.AddRange(Directory.GetFiles(path, "*.*", SearchOption.AllDirectories));
            }

            return fileList;
        }
    }
}
