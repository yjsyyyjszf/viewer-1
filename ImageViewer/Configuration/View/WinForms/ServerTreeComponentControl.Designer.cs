
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

using System;
namespace Macro.ImageViewer.Configuration.View.WinForms
{
	partial class ServerTreeComponentControl
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

				if (_component.ShowLocalServerNode)
				{
                    _aeTreeView.MouseEnter -= OnMouseEnter;
				}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerTreeComponentControl));
            this._contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._stateImageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._aeTreeView = new System.Windows.Forms.TreeView();
            this._serverTools = new System.Windows.Forms.ToolStrip();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contextMenu
            // 
            this._contextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._contextMenu.Name = "_contextMenu";
            resources.ApplyResources(this._contextMenu, "_contextMenu");
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "MyComputer.png");
            this._imageList.Images.SetKeyName(1, "Server.png");
            this._imageList.Images.SetKeyName(2, "ServerGroup.png");
            // 
            // _stateImageList
            // 
            this._stateImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_stateImageList.ImageStream")));
            this._stateImageList.TransparentColor = System.Drawing.Color.Transparent;
            this._stateImageList.Images.SetKeyName(0, "Unchecked.bmp");
            this._stateImageList.Images.SetKeyName(1, "PartiallyChecked.bmp");
            this._stateImageList.Images.SetKeyName(2, "Checked.bmp");
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox1.Controls.Add(this._aeTreeView);
            this.groupBox1.Controls.Add(this._serverTools);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // _aeTreeView
            // 
            this._aeTreeView.AllowDrop = true;
            this._aeTreeView.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this._aeTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._aeTreeView.ContextMenuStrip = this._contextMenu;
            resources.ApplyResources(this._aeTreeView, "_aeTreeView");
            this._aeTreeView.HideSelection = false;
            this._aeTreeView.ImageList = this._imageList;
            this._aeTreeView.Name = "_aeTreeView";
            this._aeTreeView.StateImageList = this._stateImageList;
            // 
            // _serverTools
            // 
            this._serverTools.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this._serverTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._serverTools.ImageScalingSize = new System.Drawing.Size(24, 24);
            resources.ApplyResources(this._serverTools, "_serverTools");
            this._serverTools.Name = "_serverTools";
            // 
            // ServerTreeComponentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.groupBox1);
            this.Name = "ServerTreeComponentControl";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.ContextMenuStrip _contextMenu;
		private System.Windows.Forms.ImageList _stateImageList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView _aeTreeView;
        private System.Windows.Forms.ToolStrip _serverTools;
    }
}
