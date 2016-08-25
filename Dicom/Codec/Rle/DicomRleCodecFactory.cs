
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
using System.Xml;
using Macro.Common;

namespace Macro.Dicom.Codec.Rle
{
    /// <summary>
    /// Default codec factory for the DICOM RLE Transfer Syntax.
    /// </summary>
	[ExtensionOf(typeof(DicomCodecFactoryExtensionPoint))]
    public class DicomRleCodecFactory : IDicomCodecFactory
    {
        private readonly string _name = TransferSyntax.RleLossless.Name;
        private readonly TransferSyntax _transferSyntax = TransferSyntax.RleLossless;

        public string Name
        {
            get { return _name; }
        }

        public bool Enabled
        {
            get { return true; }
        }

        public TransferSyntax CodecTransferSyntax
        {
            get { return _transferSyntax; }
        }

        virtual public DicomCodecParameters GetCodecParameters(DicomAttributeCollection dataSet)
        {
			DicomRleCodecParameters codecParms = new DicomRleCodecParameters { ConvertPaletteToRGB = true };

			return codecParms;
		}
		public DicomCodecParameters GetCodecParameters(XmlDocument parms)
		{
			DicomRleCodecParameters codecParms = new DicomRleCodecParameters();

			XmlElement element = parms.DocumentElement;

			if (element != null && element.Attributes["convertFromPalette"]!=null)
			{
				String boolString = element.Attributes["convertFromPalette"].Value;
				bool convert;
				if (false == bool.TryParse(boolString, out convert))
					throw new ApplicationException("Invalid convertFromPalette value specified for RLE: " + boolString);
				codecParms.ConvertPaletteToRGB = convert;
			}
			else
				codecParms.ConvertPaletteToRGB = true;

			return codecParms;
		}
        public IDicomCodec GetDicomCodec()
        {
            return new DicomRleCodec();
        }
    }
}
