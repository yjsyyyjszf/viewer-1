
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

#region License

// Copyright (c) 2006-2008, Macro Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of Macro Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System.Windows.Forms;

namespace Macro.ImageViewer.Explorer.Dicom.View.WinForms
{
    partial class SearchPanelComponentControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchPanelComponentControl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._searchingLabel = new System.Windows.Forms.Label();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._referringPhysiciansName = new Macro.Desktop.View.WinForms.TextField();
            this._patientID = new Macro.Desktop.View.WinForms.TextField();
            this._patientsName = new Macro.Desktop.View.WinForms.TextField();
            this._accessionNumber = new Macro.Desktop.View.WinForms.TextField();
            this._studyDateFrom = new Macro.Desktop.View.WinForms.DateTimeField();
            this._studyDateTo = new Macro.Desktop.View.WinForms.DateTimeField();
            this._studyDescription = new Macro.Desktop.View.WinForms.TextField();
            this._modalityPicker = new Macro.ImageViewer.Explorer.Dicom.View.WinForms.ModalityPicker();
            this._searchButton = new System.Windows.Forms.Button();
            this._searchTodayButton = new System.Windows.Forms.Button();
            this._searchLastWeekButton = new System.Windows.Forms.Button();
            this._clearButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._searchingLabel);
            this.groupBox1.Controls.Add(this._progressBar);
            this.groupBox1.Controls.Add(this._referringPhysiciansName);
            this.groupBox1.Controls.Add(this._patientID);
            this.groupBox1.Controls.Add(this._patientsName);
            this.groupBox1.Controls.Add(this._accessionNumber);
            this.groupBox1.Controls.Add(this._studyDateFrom);
            this.groupBox1.Controls.Add(this._studyDateTo);
            this.groupBox1.Controls.Add(this._studyDescription);
            this.groupBox1.Controls.Add(this._modalityPicker);
            this.groupBox1.Controls.Add(this._searchButton);
            this.groupBox1.Controls.Add(this._searchTodayButton);
            this.groupBox1.Controls.Add(this._searchLastWeekButton);
            this.groupBox1.Controls.Add(this._clearButton);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.FlatStyle=FlatStyle.Standard;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
           
            // 
            // _patientID
            // 
            resources.ApplyResources(this._patientID, "_patientID");
            this._patientID.Name = "_patientID";
            this._patientID.Value = null;
            // 
            // _accessionNumber
            // 
            resources.ApplyResources(this._accessionNumber, "_accessionNumber");
            this._accessionNumber.Name = "_accessionNumber";
            this._accessionNumber.Value = null;
            // 
            // _patientsName
            // 
            resources.ApplyResources(this._patientsName, "_patientsName");
            this._patientsName.Name = "_patientsName";
            this._patientsName.Value = null;
            // 
            // _studyDateFrom
            // 
            resources.ApplyResources(this._studyDateFrom, "_studyDateFrom");
            this._studyDateFrom.Maximum = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this._studyDateFrom.Minimum = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this._studyDateFrom.Name = "_studyDateFrom";
            this._studyDateFrom.Nullable = true;
            this._studyDateFrom.Value = null;
            // 
            // _studyDateTo
            // 
            resources.ApplyResources(this._studyDateTo, "_studyDateTo");
            this._studyDateTo.Maximum = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this._studyDateTo.Minimum = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this._studyDateTo.Name = "_studyDateTo";
            this._studyDateTo.Nullable = true;
            this._studyDateTo.Value = null;
            // 
            // _studyDescription
            // 
            resources.ApplyResources(this._studyDescription, "_studyDescription");
            this._studyDescription.Name = "_studyDescription";
            this._studyDescription.Value = null;
            // 
            // _searchButton
            // 
            this._searchButton.Image = global::Macro.ImageViewer.Explorer.Dicom.View.WinForms.Properties.Resources.Search;
            resources.ApplyResources(this._searchButton, "_searchButton");
            this._searchButton.Name = "_searchButton";
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this.OnSearchButtonClicked);
            // 
            // _searchLastWeekButton
            // 
            this._searchLastWeekButton.Image = global::Macro.ImageViewer.Explorer.Dicom.View.WinForms.Properties.Resources.Last7Days;
            resources.ApplyResources(this._searchLastWeekButton, "_searchLastWeekButton");
            this._searchLastWeekButton.Name = "_searchLastWeekButton";
            this._searchLastWeekButton.UseVisualStyleBackColor = true;
            this._searchLastWeekButton.Click += new System.EventHandler(this.OnSearchLastWeekButtonClick);
            // 
            // _clearButton
            // 
            this._clearButton.Image = global::Macro.ImageViewer.Explorer.Dicom.View.WinForms.Properties.Resources.Clear;
            resources.ApplyResources(this._clearButton, "_clearButton");
            this._clearButton.Name = "_clearButton";
            this._clearButton.UseVisualStyleBackColor = true;
            this._clearButton.Click += new System.EventHandler(this.OnClearButonClicked);
            // 
            // _searchTodayButton
            // 
            this._searchTodayButton.Image = global::Macro.ImageViewer.Explorer.Dicom.View.WinForms.Properties.Resources.Today;
            resources.ApplyResources(this._searchTodayButton, "_searchTodayButton");
            this._searchTodayButton.Name = "_searchTodayButton";
            this._searchTodayButton.UseVisualStyleBackColor = true;
            this._searchTodayButton.Click += new System.EventHandler(this.OnSearchTodayButtonClicked);
          
            // 
            // _referringPhysiciansName
            // 
            resources.ApplyResources(this._referringPhysiciansName, "_referringPhysiciansName");
            this._referringPhysiciansName.Name = "_referringPhysiciansName";
            this._referringPhysiciansName.Value = null;
            // 
            // _modalityPicker
            // 
            resources.ApplyResources(this._modalityPicker, "_modalityPicker");
            this._modalityPicker.Name = "_modalityPicker";
            // 
            // _progressBar
            // 
            resources.ApplyResources(this._progressBar, "_progressBar");
            this._progressBar.Name = "_progressBar";
            // 
            // _searchingLabel
            // 
            resources.ApplyResources(this._searchingLabel, "_searchingLabel");
            this._searchingLabel.Name = "_searchingLabel";
            // 
            // SearchPanelComponentControl
            // 
            this.AcceptButton = this._searchButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Controls.Add(this.groupBox1);
            this.groupBox1.Dock=DockStyle.Fill;
            this.ForeColor = System.Drawing.Color.White;
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Name = "SearchPanelComponentControl";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
		private Macro.Desktop.View.WinForms.DateTimeField _studyDateTo;
		private System.Windows.Forms.Button _searchButton;
		private System.Windows.Forms.Button _searchLastWeekButton;
		private System.Windows.Forms.Button _clearButton;
		private System.Windows.Forms.Button _searchTodayButton;
		private Macro.Desktop.View.WinForms.DateTimeField _studyDateFrom;
		private Macro.Desktop.View.WinForms.TextField _patientID;
		private Macro.Desktop.View.WinForms.TextField _patientsName;
		private Macro.Desktop.View.WinForms.TextField _studyDescription;
		private Macro.Desktop.View.WinForms.TextField _accessionNumber;
		private ModalityPicker _modalityPicker;
		private Macro.Desktop.View.WinForms.TextField _referringPhysiciansName;
		private System.Windows.Forms.ProgressBar _progressBar;
		private System.Windows.Forms.Label _searchingLabel;
    }
}
