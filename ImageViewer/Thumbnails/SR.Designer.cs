
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
//     运行时版本:4.0.30319.1008
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Macro.ImageViewer.Thumbnails {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SR {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SR() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Macro.ImageViewer.Thumbnails.SR", typeof(SR).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 ({0} images)
        ///{1} 的本地化字符串。
        /// </summary>
        internal static string FormatThumbnailName {
            get {
                return ResourceManager.GetString("FormatThumbnailName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 (1 image)
        ///{0} 的本地化字符串。
        /// </summary>
        internal static string FormatThumbnailName1Image {
            get {
                return ResourceManager.GetString("FormatThumbnailName1Image", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Thumbnails 的本地化字符串。
        /// </summary>
        internal static string LabelThumbnails {
            get {
                return ResourceManager.GetString("LabelThumbnails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Thumbnails 的本地化字符串。
        /// </summary>
        internal static string MenuShowThumbnails {
            get {
                return ResourceManager.GetString("MenuShowThumbnails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 视图 的本地化字符串。
        /// </summary>
        internal static string MenuView {
            get {
                return ResourceManager.GetString("MenuView", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {0}
        ///{1} 的本地化字符串。
        /// </summary>
        internal static string MessageFormatStudyNotLoadable {
            get {
                return ResourceManager.GetString("MessageFormatStudyNotLoadable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Loading ... 的本地化字符串。
        /// </summary>
        internal static string MessageLoading {
            get {
                return ResourceManager.GetString("MessageLoading", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 No Images 的本地化字符串。
        /// </summary>
        internal static string MessageNoImages {
            get {
                return ResourceManager.GetString("MessageNoImages", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 缩略图 的本地化字符串。
        /// </summary>
        internal static string TitleThumbnails {
            get {
                return ResourceManager.GetString("TitleThumbnails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Thumbnails (Loading Priors ... ) 的本地化字符串。
        /// </summary>
        internal static string TitleThumbnailsLoadingPriors {
            get {
                return ResourceManager.GetString("TitleThumbnailsLoadingPriors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 缩略图 的本地化字符串。
        /// </summary>
        internal static string ToolbarShowThumbnails {
            get {
                return ResourceManager.GetString("ToolbarShowThumbnails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 显示缩略图 的本地化字符串。
        /// </summary>
        internal static string TooltipShowThumbnails {
            get {
                return ResourceManager.GetString("TooltipShowThumbnails", resourceCulture);
            }
        }
    }
}
