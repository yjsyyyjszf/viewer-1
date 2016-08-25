
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

namespace Macro.ImageViewer.Tools.Standard.View.WinForms
{
	partial class ImagePropertyDetailControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagePropertyDetailControl));
			this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this._description = new System.Windows.Forms.Label();
			this._richText = new System.Windows.Forms.RichTextBox();
			this._name = new System.Windows.Forms.Label();
			this._tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _tableLayoutPanel
			// 
			resources.ApplyResources(this._tableLayoutPanel, "_tableLayoutPanel");
			this._tableLayoutPanel.Controls.Add(this._description, 0, 2);
			this._tableLayoutPanel.Controls.Add(this._richText, 0, 1);
			this._tableLayoutPanel.Controls.Add(this._name, 0, 0);
			this._tableLayoutPanel.Name = "_tableLayoutPanel";
			// 
			// _description
			// 
			this._description.AutoEllipsis = true;
			this._description.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			resources.ApplyResources(this._description, "_description");
			this._description.Name = "_description";
			// 
			// _richText
			// 
			this._richText.DetectUrls = false;
			resources.ApplyResources(this._richText, "_richText");
			this._richText.Name = "_richText";
			this._richText.ReadOnly = true;
			// 
			// _name
			// 
			resources.ApplyResources(this._name, "_name");
			this._name.AutoEllipsis = true;
			this._name.Name = "_name";
			// 
			// ImagePropertyDetailControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._tableLayoutPanel);
			this.Name = "ImagePropertyDetailControl";
			this._tableLayoutPanel.ResumeLayout(false);
			this._tableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
		private System.Windows.Forms.Label _name;
		private System.Windows.Forms.Label _description;
		private System.Windows.Forms.RichTextBox _richText;



	}
}
