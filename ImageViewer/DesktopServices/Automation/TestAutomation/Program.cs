
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Macro.ImageViewer.Common.Automation;

namespace TestAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filePath = @"D:\Picture\CT39535\20130715";
                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress endpoint = new EndpointAddress("http://127.0.0.1:51124/Macro/ImageViewer/Automation?wsdl");
                ViewerAutomationServiceClient client = new ViewerAutomationServiceClient(binding, endpoint);
                OpenFilesRequest openFiles = new OpenFilesRequest();
                openFiles.Files = Directory.GetFiles(filePath).ToList();
                client.OpenFiles(openFiles);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("PACS浏览器打开图像出错，错误原因{0}", e.Message));

            }

            Console.ReadKey();
        }

    }
}
