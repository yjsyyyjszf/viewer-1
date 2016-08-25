
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
	partial class ComboBoxCellEditorControl
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
			this._comboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// _comboBox
			// 
			this._comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._comboBox.FormattingEnabled = true;
			this._comboBox.Location = new System.Drawing.Point(0, 0);
			this._comboBox.Name = "_comboBox";
			this._comboBox.Size = new System.Drawing.Size(150, 21);
			this._comboBox.TabIndex = 0;
			this._comboBox.SelectionChangeCommitted += new System.EventHandler(this._comboBox_SelectionChangeCommitted);
			this._comboBox.Format += new System.Windows.Forms.ListControlConvertEventHandler(this._comboBox_Format);
			// 
			// ComboBoxCellEditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._comboBox);
			this.Name = "ComboBoxCellEditorControl";
			this.Size = new System.Drawing.Size(150, 25);
			this.Load += new System.EventHandler(this.ComboBoxCellEditorControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox _comboBox;
	}
}
