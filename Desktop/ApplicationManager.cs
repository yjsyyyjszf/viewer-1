
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
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Macro.Common;
using Macro.Common.Utilities;

namespace Macro.Desktop
{
    /// <summary>
    /// Defines the interface to a view for the <see cref="Application"/> object.
    /// </summary>
    public interface IApplicationManagerView : IView
    {
        /// <summary>
        /// Creates a view for the specified desktop window.
        /// </summary>
        void CreateWindowView(ApplicationManager window);

        void Show();

    }


    [ExtensionPoint]
    public sealed class ApplicationManagerViewExtensionPoint : ExtensionPoint<IApplicationManagerView>
    {
    }



    [ExtensionOf(typeof(ApplicationLicenseManagerExtensionPoint))]
    [AssociateView(typeof(ApplicationManagerViewExtensionPoint))]
    public class ApplicationManager : ILicenseManager
    {
        #region ILicenseManager 成员

        public bool IsGivedLicense()
        {
            string license = LicenseInformation.LicenseKey;
            string machineId = EnvironmentUtilities.MachineIdentifier;
            if (license == null || license == "" || !Decrypt(license).Equals(machineId))
            {
                ApplicationManagerViewExtensionPoint xp = new ApplicationManagerViewExtensionPoint();
                IApplicationManagerView manager = (IApplicationManagerView)xp.CreateExtension();
                if (manager == null)
                {
                    return false;
                }
                manager.CreateWindowView(this);
                manager.Show();
            }
            else
            {
                return true;
            }

            return false;

        }

        public void SetmachineId(string mid)
        {
            LicenseInformation.LicenseKey = mid;
        }

        public string Id
        {
            get { return Encrypt(EnvironmentUtilities.MachineIdentifier); }
        }

        private static string Decrypt(string @string)
        {
            if (String.IsNullOrEmpty(@string))
                return @string;

            string result;
            try
            {
                byte[] bytes = Convert.FromBase64String(@string);
                using (MemoryStream dataStream = new MemoryStream(bytes))
                {
                    RC2CryptoServiceProvider cryptoService = new RC2CryptoServiceProvider();
                    cryptoService.Key = Encoding.UTF8.GetBytes(@"Macro");
                    cryptoService.IV = Encoding.UTF8.GetBytes(@"IsSoCool");
                    cryptoService.UseSalt = false;
                    using (CryptoStream cryptoStream = new CryptoStream(dataStream, cryptoService.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                            reader.Close();
                        }
                        cryptoStream.Close();
                    }
                    dataStream.Close();
                }
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }

        private static string Encrypt(string @string)
        {
            if (String.IsNullOrEmpty(@string))
                return @string;

            string result;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(@string);
                using (MemoryStream dataStream = new MemoryStream())
                {
                    RC2CryptoServiceProvider cryptoService = new RC2CryptoServiceProvider();
                    cryptoService.Key = Encoding.UTF8.GetBytes(@"FuckHold");
                    cryptoService.IV = Encoding.UTF8.GetBytes(@"SFuckYOU");
                    cryptoService.UseSalt = false;
                    using (CryptoStream cryptoStream = new CryptoStream(dataStream, cryptoService.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.FlushFinalBlock();
                        byte[] inputBytes = dataStream.ToArray();
                        result = Convert.ToBase64String(inputBytes);
                        cryptoStream.Close();
                    }
                    dataStream.Close();
                }
            }
            catch (Exception e)
            {
                result = string.Empty;
            }
            return result;
        }
        #endregion


    }
}
