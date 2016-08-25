
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
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop.Tools;

namespace Macro.ImageViewer
{
    public class PrintViewerSetupHelper : ViewerSetupHelper
    {
        protected override Desktop.Tools.ITool[] GetTools()
        {
            if (Tools != null)
                return Tools;

            try
            {
                object[] extensions = new PrintImageViewerToolExtensionPoint().CreateExtensions();
                return CollectionUtils.Map(extensions, (object tool) => (ITool)tool).ToArray();
            }
            catch (NotSupportedException)
            {
                Platform.Log(LogLevel.Debug, "No viewer tool extensions found.");
                return new ITool[0];
            }
        }
    }
}
