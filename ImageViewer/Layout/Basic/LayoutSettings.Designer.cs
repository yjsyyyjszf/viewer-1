
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

//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Macro.ImageViewer.Layout.Basic {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class LayoutSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static LayoutSettings defaultInstance = ((LayoutSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new LayoutSettings())));
        
        public static LayoutSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        /// <summary>
        /// Per-modality image box and tile configuration(s) used to layout the viewer when it is first opened, based on the loaded study.
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Per-modality image box and tile configuration(s) used to layout the viewer when i" +
            "t is first opened, based on the loaded study.")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Xml.XmlDocument LayoutSettingsXml {
            get {
                return ((global::System.Xml.XmlDocument)(this["LayoutSettingsXml"]));
            }
            set {
                this["LayoutSettingsXml"] = value;
            }
        }
    }
}
