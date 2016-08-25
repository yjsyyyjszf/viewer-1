
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
    partial class DesktopForm
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
            this._toolbar = new System.Windows.Forms.ToolStrip();
            this._toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._tabbedGroups = new Crownwood.DotNetMagic.Controls.TabbedGroups();
            this._mainMenu = new Macro.Desktop.View.WinForms.CustomContrlsMenuStrip();
            this._toolStripContainer.ContentPanel.SuspendLayout();
            this._toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tabbedGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // _toolbar
            // 
            this._toolbar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this._toolbar.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this._toolbar.Location = new System.Drawing.Point(0, 24);
            this._toolbar.Name = "_toolbar";
            this._toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._toolbar.Size = new System.Drawing.Size(737, 25);
            this._toolbar.TabIndex = 1;
            this._toolbar.Text = "toolStrip1";
            // 
            // _toolStripContainer
            // 
            // 
            // _toolStripContainer.ContentPanel
            // 
            this._toolStripContainer.ContentPanel.Controls.Add(this._tabbedGroups);
            this._toolStripContainer.ContentPanel.Size = new System.Drawing.Size(737, 471);
            this._toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toolStripContainer.Location = new System.Drawing.Point(0, 49);
            this._toolStripContainer.Name = "_toolStripContainer";
            this._toolStripContainer.Size = new System.Drawing.Size(737, 496);
            this._toolStripContainer.TabIndex = 2;
            this._toolStripContainer.Text = "toolStripContainer1";
            // 
            // _tabbedGroups
            // 
            this._tabbedGroups.AllowDrop = true;
            this._tabbedGroups.AtLeastOneLeaf = true;
            this._tabbedGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabbedGroups.Location = new System.Drawing.Point(0, 0);
            this._tabbedGroups.Name = "_tabbedGroups";
            this._tabbedGroups.ProminentLeaf = null;
            this._tabbedGroups.ResizeBarColor = System.Drawing.SystemColors.Control;
            this._tabbedGroups.Size = new System.Drawing.Size(737, 471);
            this._tabbedGroups.TabIndex = 0;
            // 
            // _mainMenu
            // 
            this._mainMenu.Location = new System.Drawing.Point(0, 0);
            this._mainMenu.Name = "_mainMenu";
            this._mainMenu.Size = new System.Drawing.Size(737, 24);
            this._mainMenu.TabIndex = 3;
            this._mainMenu.Text = "customContrlsMenuStrip1";
            this._mainMenu.ThemeColor = System.Drawing.Color.Gray;
            // 
            // DesktopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 545);
            this.Controls.Add(this._toolStripContainer);
            this.Controls.Add(this._toolbar);
            this.Controls.Add(this._mainMenu);
            this.Name = "DesktopForm";
            this.Text = "DesktopForm";
            this._toolStripContainer.ContentPanel.ResumeLayout(false);
            this._toolStripContainer.ResumeLayout(false);
            this._toolStripContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tabbedGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolbar;
        private System.Windows.Forms.ToolStripContainer _toolStripContainer;
        private Crownwood.DotNetMagic.Controls.TabbedGroups _tabbedGroups;
        private Crownwood.DotNetMagic.Docking.DockingManager _dockingManager;
        private CustomContrlsMenuStrip _mainMenu;

    }
}