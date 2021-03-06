
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

namespace Macro.ImageViewer.Tools.Standard.PresetVoiLuts {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class PresetVoiLutSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static PresetVoiLutSettings defaultInstance = ((PresetVoiLutSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new PresetVoiLutSettings())));
        
        public static PresetVoiLutSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        /// <summary>
        /// XML document containing per-modality user defined LUT (e.g. window/level) presets.  These presets appear in the context menu and Window/Level tool drop-down.
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("XML document containing per-modality user defined LUT (e.g. window/level) presets" +
            ".  These presets appear in the context menu and Window/Level tool drop-down.")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DefaultPresetVoiLutConfiguration.xml")]
        public string SettingsXml {
            get {
                return ((string)(this["SettingsXml"]));
            }
            set {
                this["SettingsXml"] = value;
            }
        }
    }
}
