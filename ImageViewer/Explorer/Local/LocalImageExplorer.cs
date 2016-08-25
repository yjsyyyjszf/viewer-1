
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


using System.Collections.Generic;
using Macro.Common;
using Macro.Common.Authorization;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.Desktop.Explorer;
using Macro.Desktop.Tools;

namespace Macro.ImageViewer.Explorer.Local
{

    //[ExtensionOf(typeof(DesktopToolExtensionPoint))]
    //public class LocalImageExplorer : Tool<IDesktopToolContext>
    //{
    //    LocalImageExplorerComponent _component;
    //    private static IDesktopObject _desktopObject;


    //    public LocalImageExplorer()
    //    {

    //    }
    //    public override IActionSet Actions
    //    {
    //        get
    //        {
    //            if (_component == null)
    //            {
    //                return new ActionSet();
    //            }
    //            else
    //            {
    //                return base.Actions;
    //            }
    //        }
    //    }

    //    public override void Initialize()
    //    {
    //        ShowInternal();

    //        base.Initialize();
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        CloseChildDesktopWindows();

    //        base.Dispose(disposing);
    //    }

    //    public void Show()
    //    {
    //        BlockingOperation.Run(delegate { ShowInternal(); });
    //    }

    //    private void ShowInternal()
    //    {
    //        if (_desktopObject != null)
    //        {
    //            _desktopObject.Activate();
    //            return;
    //        }

    //        WorkspaceCreationArgs args = new WorkspaceCreationArgs();
    //        args.Component = Component;
    //        args.Title = "�ҵĵ���";
    //        args.Name = "Viewer/Explorer/My Computer";
    //        args.UserClosable = false;
    //        _desktopObject = ApplicationComponent.LaunchAsWorkspace(this.Context.DesktopWindow, args);


    //        _desktopObject.Closed += delegate { _desktopObject = null; };
    //    }



    //    private void CloseChildDesktopWindows()
    //    {
    //        List<DesktopWindow> childWindowsToClose = new List<DesktopWindow>();

    //        // We can't just iterate through the collection and close them,
    //        // because closing a window changes the collection.  So instead,
    //        // we create a list of the child windows then iterate through
    //        // that list and close them.
    //        foreach (DesktopWindow window in Application.DesktopWindows)
    //        {
    //            // Child windows are all those other than the one
    //            // this tool is hosted by
    //            if (window != this.Context.DesktopWindow)
    //                childWindowsToClose.Add(window);
    //        }

    //        foreach (DesktopWindow window in childWindowsToClose)
    //            window.Close();
    //    }

    //    public IApplicationComponent Component
    //    {
    //        get
    //        {
    //            if (_component == null)
    //                _component = new LocalImageExplorerComponent();

    //            return _component;
    //        }
    //    }


    //}

    public static class AuthorityTokens
    {
        [AuthorityToken(Description = "Grant access to the 'My Computer' explorer.")]
        public const string MyComputer = "Viewer/Explorer/My Computer";
    }

    [ExtensionOf(typeof(HealthcareArtifactExplorerExtensionPoint))]
    public class LocalImageExplorer : IHealthcareArtifactExplorer
    {
        LocalImageExplorerComponent _component;

        public LocalImageExplorer()
        {

        }

        #region IHealthcareArtifactExplorer Members

        public string Name
        {
            get { return SR.MyComputer; }
        }

        public bool IsAvailable
        {
            get { return PermissionsHelper.IsInRole(AuthorityTokens.MyComputer); }
        }

        public IApplicationComponent Component
        {
            get
            {
                if (_component == null && IsAvailable)
                    _component = new LocalImageExplorerComponent();

                return _component;
            }
        }

        #endregion

    }
}
