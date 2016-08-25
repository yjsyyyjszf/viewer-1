
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

namespace Macro.ImageViewer.Explorer.Dicom.SeriesDetails {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Macro.ImageViewer.Explorer.Dicom.SeriesDetails.SR", typeof(SR).Assembly);
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
        ///   查找类似 Open Activity Monitor 的本地化字符串。
        /// </summary>
        internal static string LinkOpenActivityMonitor {
            get {
                return ResourceManager.GetString("LinkOpenActivityMonitor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Delete Series 的本地化字符串。
        /// </summary>
        internal static string MenuDeleteSeries {
            get {
                return ResourceManager.GetString("MenuDeleteSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Retrieve Series 的本地化字符串。
        /// </summary>
        internal static string MenuRetrieveSeries {
            get {
                return ResourceManager.GetString("MenuRetrieveSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Send Series 的本地化字符串。
        /// </summary>
        internal static string MenuSendSeries {
            get {
                return ResourceManager.GetString("MenuSendSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 View Series Details 的本地化字符串。
        /// </summary>
        internal static string MenuShowSeriesDetails {
            get {
                return ResourceManager.GetString("MenuShowSeriesDetails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Series scheduled for deletion cannot be sent. 的本地化字符串。
        /// </summary>
        internal static string MessageCannotSendSeriesScheduledForDeletion {
            get {
                return ResourceManager.GetString("MessageCannotSendSeriesScheduledForDeletion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Deleting the remaining series will cause the entire study to be deleted. Are you sure you want to delete the entire study? 的本地化字符串。
        /// </summary>
        internal static string MessageConfirmDeleteEntireStudy {
            get {
                return ResourceManager.GetString("MessageConfirmDeleteEntireStudy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Are you sure you want to delete the selected series? 的本地化字符串。
        /// </summary>
        internal static string MessageConfirmDeleteSeries {
            get {
                return ResourceManager.GetString("MessageConfirmDeleteSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 You have selected multiple servers to send to.  Are you sure you want to continue? 的本地化字符串。
        /// </summary>
        internal static string MessageConfirmSendToMultipleServers {
            get {
                return ResourceManager.GetString("MessageConfirmSendToMultipleServers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Failed to initiate deletion of the series. 的本地化字符串。
        /// </summary>
        internal static string MessageFailedToDeleteSeries {
            get {
                return ResourceManager.GetString("MessageFailedToDeleteSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Failed to initiate retrieving of the selected items. 的本地化字符串。
        /// </summary>
        internal static string MessageFailedToRetrieveSeries {
            get {
                return ResourceManager.GetString("MessageFailedToRetrieveSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Failed to initiate sending of the selected items. 的本地化字符串。
        /// </summary>
        internal static string MessageFailedToSendSeries {
            get {
                return ResourceManager.GetString("MessageFailedToSendSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Are you sure you want to delete the {0} selected series? 的本地化字符串。
        /// </summary>
        internal static string MessageFormatConfirmDeleteSeries {
            get {
                return ResourceManager.GetString("MessageFormatConfirmDeleteSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The deletion of {0} series in the study for {1} [{2},A#:{3}] has been scheduled. 的本地化字符串。
        /// </summary>
        internal static string MessageFormatDeleteSeriesScheduled {
            get {
                return ResourceManager.GetString("MessageFormatDeleteSeriesScheduled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The retrieve of {0} series for {2} [{3},A#:{4}] from {1} has been scheduled. 的本地化字符串。
        /// </summary>
        internal static string MessageFormatRetrieveSeriesScheduled {
            get {
                return ResourceManager.GetString("MessageFormatRetrieveSeriesScheduled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The send of {0} series to {1} for {2} [{3},A#:{4}] has been scheduled. 的本地化字符串。
        /// </summary>
        internal static string MessageFormatSendSeriesScheduled {
            get {
                return ResourceManager.GetString("MessageFormatSendSeriesScheduled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Deleting series may result in partial study data, which could lead to patient misdiagnosis. 的本地化字符串。
        /// </summary>
        internal static string MessagePartialStudyWarning {
            get {
                return ResourceManager.GetString("MessagePartialStudyWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Failed to retrieve the selected series because the DICOM Server service is not running.
        ///Please ensure the Macro Service is running or contact your system administrator for assistance. 的本地化字符串。
        /// </summary>
        internal static string MessageRetrieveDicomServerServiceNotRunning {
            get {
                return ResourceManager.GetString("MessageRetrieveDicomServerServiceNotRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Please select a destination. 的本地化字符串。
        /// </summary>
        internal static string MessageSelectDestination {
            get {
                return ResourceManager.GetString("MessageSelectDestination", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Please select series that are not already scheduled for deletion. 的本地化字符串。
        /// </summary>
        internal static string MessageSelectSeriesNotAlreadyScheduledForDeletion {
            get {
                return ResourceManager.GetString("MessageSelectSeriesNotAlreadyScheduledForDeletion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Failed to send the selected studies because the DICOM Server service is not running.
        ///Please ensure the Macro Service is running or contact your system administrator for assistance. 的本地化字符串。
        /// </summary>
        internal static string MessageSendDicomServerServiceNotRunning {
            get {
                return ResourceManager.GetString("MessageSendDicomServerServiceNotRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The selected study is currently in use.  Please close all relevant workspaces before deleting. 的本地化字符串。
        /// </summary>
        internal static string MessageStudyInUse {
            get {
                return ResourceManager.GetString("MessageStudyInUse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Delete Scheduled 的本地化字符串。
        /// </summary>
        internal static string TitleDeleteScheduled {
            get {
                return ResourceManager.GetString("TitleDeleteScheduled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Modality 的本地化字符串。
        /// </summary>
        internal static string TitleModality {
            get {
                return ResourceManager.GetString("TitleModality", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Instances 的本地化字符串。
        /// </summary>
        internal static string TitleNumberOfSeriesRelatedInstances {
            get {
                return ResourceManager.GetString("TitleNumberOfSeriesRelatedInstances", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Send Series 的本地化字符串。
        /// </summary>
        internal static string TitleSendSeries {
            get {
                return ResourceManager.GetString("TitleSendSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Description 的本地化字符串。
        /// </summary>
        internal static string TitleSeriesDescription {
            get {
                return ResourceManager.GetString("TitleSeriesDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Series Details 的本地化字符串。
        /// </summary>
        internal static string TitleSeriesDetails {
            get {
                return ResourceManager.GetString("TitleSeriesDetails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Number 的本地化字符串。
        /// </summary>
        internal static string TitleSeriesNumber {
            get {
                return ResourceManager.GetString("TitleSeriesNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Delete Series 的本地化字符串。
        /// </summary>
        internal static string ToolbarDeleteSeries {
            get {
                return ResourceManager.GetString("ToolbarDeleteSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Retrieve Series 的本地化字符串。
        /// </summary>
        internal static string ToolbarRetrieveSeries {
            get {
                return ResourceManager.GetString("ToolbarRetrieveSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Send Series 的本地化字符串。
        /// </summary>
        internal static string ToolbarSendSeries {
            get {
                return ResourceManager.GetString("ToolbarSendSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Delete the selected series from local storage 的本地化字符串。
        /// </summary>
        internal static string TooltipDeleteSeries {
            get {
                return ResourceManager.GetString("TooltipDeleteSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Retrieve the selected series 的本地化字符串。
        /// </summary>
        internal static string TooltipRetrieveSeries {
            get {
                return ResourceManager.GetString("TooltipRetrieveSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Send the selected series to another DICOM device 的本地化字符串。
        /// </summary>
        internal static string TooltipSendSeries {
            get {
                return ResourceManager.GetString("TooltipSendSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 View Series Details 的本地化字符串。
        /// </summary>
        internal static string TooltipSeriesDetails {
            get {
                return ResourceManager.GetString("TooltipSeriesDetails", resourceCulture);
            }
        }
    }
}
