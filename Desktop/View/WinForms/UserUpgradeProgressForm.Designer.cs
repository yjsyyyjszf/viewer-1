
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

namespace Macro.Desktop.View.WinForms
{
	partial class UserUpgradeProgressForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserUpgradeProgressForm));
			this._progressBar = new System.Windows.Forms.ProgressBar();
			this._message = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// _progressBar
			// 
			resources.ApplyResources(this._progressBar, "_progressBar");
			this._progressBar.Name = "_progressBar";
			this._progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// _message
			// 
			resources.ApplyResources(this._message, "_message");
			this._message.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._message.Name = "_message";
			this._message.ReadOnly = true;
			this._message.TabStop = false;
			// 
			// UserUpgradeProgressForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add(this._progressBar);
			this.Controls.Add(this._message);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UserUpgradeProgressForm";
			this.ShowInTaskbar = false;
			//this.Style = Crownwood.DotNetMagic.Common.VisualStyle.Office2007Black;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar _progressBar;
		private System.Windows.Forms.TextBox _message;
	}
}