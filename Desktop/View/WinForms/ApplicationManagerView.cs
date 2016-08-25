
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


using System;
using Macro.Common;

namespace Macro.Desktop.View.WinForms
{
    [ExtensionOf(typeof(ApplicationManagerViewExtensionPoint))]
    public class ApplicationManagerView : WinFormsView, IApplicationManagerView
    {
        private ApplicationManagerForm form;
        private ApplicationManager manager;

        public ApplicationManagerView()
        {
            System.Windows.Forms.Application.ThreadException += (sender, e) => ExceptionHandler.ReportUnhandled(e.Exception);
        }


        #region IApplicationManagerView 成员

        public void CreateWindowView(ApplicationManager window)
        {
            manager = window;
            form = new ApplicationManagerForm();
            form.QuDing.Click += Close;
            form.Load += formLoad;
        }

        private void formLoad(object sender, EventArgs args)
        {
            form.SetMiYao(manager.Id);
        }

        public void Show()
        {
            if (form != null)
            {
                System.Windows.Forms.Application.Run(form);
            }
        }

        private void Close(object sender, EventArgs args)
        {
            if (form != null)
            {
                System.Windows.Forms.MessageBox.Show("请从新启动应用程序");
                if (form.GetShouQuan != null && form.GetShouQuan != "")
                {
                    manager.SetmachineId(form.GetShouQuan);
                    form.Close();
                }
            }
        }

        #endregion

        public override object GuiElement
        {
            // not used
            get { throw new NotSupportedException(); }
        }
    }
}
