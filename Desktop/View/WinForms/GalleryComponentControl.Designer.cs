
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
	partial class GalleryComponentControl
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
				if (_component != null)
					_component.DataSourceChanged -= OnDataSourceChanged;
				
				if (components != null)
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader colName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GalleryComponentControl));
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._listView = new Macro.Desktop.View.WinForms.GalleryComponentListViewControl();
            this._contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // colName
            // 
            resources.ApplyResources(colName, "colName");
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            resources.ApplyResources(this._toolStrip, "_toolStrip");
            this._toolStrip.Name = "_toolStrip";
            // 
            // _listView
            // 
            this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            colName});
            this._listView.ContextMenuStrip = this._contextMenu;
            resources.ApplyResources(this._listView, "_listView");
            this._listView.InsertionBoxIndex = -1;
            this._listView.Name = "_listView";
            this._listView.ShowItemToolTips = true;
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.OnAfterLabelEdit);
            this._listView.ItemActivate += new System.EventHandler(this.OnItemActivate);
            this._listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this._listView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
            // 
            // _contextMenu
            // 
            this._contextMenu.Name = "_contextMenu";
            resources.ApplyResources(this._contextMenu, "_contextMenu");
            // 
            // GalleryComponentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._listView);
            this.Controls.Add(this._toolStrip);
            this.Name = "GalleryComponentControl";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip _toolStrip;
		private Macro.Desktop.View.WinForms.GalleryComponentListViewControl _listView;
		private System.Windows.Forms.ContextMenuStrip _contextMenu;
	}
}