
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

using System.Windows.Forms;

namespace Macro.ImageViewer.Tools.Standard.View.WinForms
{
	partial class ImagePropertiesApplicationComponentControl
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
			if (disposing)
			{
				if (components != null)
					components.Dispose();

				if (_component != null)
					_component.PropertyChanged -= Update;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagePropertiesApplicationComponentControl));
			this._properties = new Macro.ImageViewer.Tools.Standard.View.WinForms.CustomPropertyGrid();
			this.SuspendLayout();
			// 
			// _properties
			// 
			this._properties.BackColor = System.Drawing.Color.Gray;
			this._properties.CategoryForeColor = System.Drawing.Color.WhiteSmoke;
			this._properties.CommandsForeColor = System.Drawing.Color.WhiteSmoke;
			this._properties.CommandsVisibleIfAvailable = false;
			resources.ApplyResources(this._properties, "_properties");
			this._properties.LineColor = System.Drawing.Color.Gray;
			this._properties.Name = "_properties";
			this._properties.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this._properties.ToolbarVisible = false;
			this._properties.ViewBackColor = System.Drawing.Color.Black;
			this._properties.ViewForeColor = System.Drawing.Color.WhiteSmoke;
			// 
			// ImagePropertiesApplicationComponentControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._properties);
			this.Name = "ImagePropertiesApplicationComponentControl";
			this.ResumeLayout(false);

		}

		#endregion

		private CustomPropertyGrid _properties;
	}
}
