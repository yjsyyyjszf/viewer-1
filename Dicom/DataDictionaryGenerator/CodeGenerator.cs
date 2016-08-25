
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

// Copyright (c) 2013, Macro Inc.
// All rights reserved.
// http://www.Macro.ca
//
// This file is part of the Macro RIS/PACS open source project.
//
// The Macro RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The Macro RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the Macro RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Macro.Dicom.DataDictionaryGenerator
{
    public class CodeGenerator
    {
    	readonly SortedList<uint, Tag> _tagList = null;
    	readonly SortedList _tSyntaxList = null;
    	readonly SortedList _sopList = null;
    	readonly SortedList _metaSopList = null;
    	readonly XmlDocument _transferSyntaxDoc = null;

        public CodeGenerator(SortedList<uint, Tag> tags, SortedList tSyntax, SortedList sops, SortedList metaSops, XmlDocument transferSyntaxDoc)
        {
            _tagList = tags;
            _tSyntaxList = tSyntax;
            _sopList = sops;
            _metaSopList = metaSops;
            _transferSyntaxDoc = transferSyntaxDoc;
        }

        private void WriterHeader(StreamWriter writer)
        {
            writer.WriteLine("#region License");
            writer.WriteLine("");
            writer.WriteLine("// Copyright (c) 2012, Macro Inc.");
            writer.WriteLine("// All rights reserved.");
            writer.WriteLine("// http://www.Macro.ca");
            writer.WriteLine("//");
            writer.WriteLine("// This software is licensed under the Open Software License v3.0.");
            writer.WriteLine("// For the complete license, see http://www.Macro.ca/OSLv3.0");
            writer.WriteLine("");
            writer.WriteLine("#endregion");
            writer.WriteLine("");
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine("");
            writer.WriteLine("// This file is auto-generated by the Macro.Dicom.DataDictionaryGenerator project.");
            writer.WriteLine("");
            writer.WriteLine("namespace Macro.Dicom");
            writer.WriteLine("{");
        }

        private void WriterFooter(StreamWriter writer)
        {
            writer.WriteLine("}");
        }

        /// <summary>
        /// Create the DicomTags.cs file.
        /// </summary>
        /// <param name="tagFile"></param>
        public void WriteTags(String tagFile)
        {
            var writer = new StreamWriter(tagFile);

            WriterHeader(writer);

            writer.WriteLine("    /// <summary>");
            writer.WriteLine("    /// This structure contains defines for all DICOM tags.");
            writer.WriteLine("    /// </summary>");
            writer.WriteLine("    public struct DicomTags");
            writer.WriteLine("    {");

            IEnumerator<Tag> iter = _tagList.Values.GetEnumerator();

            while (iter.MoveNext())
            {
                Tag tag = iter.Current;

                writer.WriteLine("        /// <summary>");
                writer.WriteLine("        /// <para>" + tag.tag + " " + tag.unEscapedName + "</para>");
                writer.WriteLine("        /// <para> VR: " + tag.vr + " VM:" + tag.vm + "</para>");
                if (tag.retired != null && tag.retired.Equals("RET"))
                    writer.WriteLine("        /// <para>This tag has been retired.</para>");
                writer.WriteLine("        /// </summary>");
                writer.WriteLine("        public const uint " + tag.varName + " = " + tag.nTag + ";");
            }

            writer.WriteLine("    }");
            WriterFooter(writer);

            writer.Close();
        }        

        /// <summary>
        /// Get transfer syntax details from the TransferSyntax.xml file for a specific transfer syntax.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="littleEndian"></param>
        /// <param name="encapsulated"></param>
        /// <param name="explicitVR"></param>
        /// <param name="deflated"></param>
        /// <param name="lossy"></param>
        /// <param name="lossless"></param>
        public void GetTransferSyntaxDetails(String uid, ref String littleEndian, ref String encapsulated,
            ref String explicitVR, ref String deflated, ref string lossy, ref string lossless)
        {
            XmlNode syntaxNode = _transferSyntaxDoc.FirstChild;

            // I know the format, just do a quick traversal to the first TransferSyntax entry
            syntaxNode = syntaxNode.NextSibling;
            syntaxNode = syntaxNode.FirstChild;

            while (syntaxNode != null)
            {
                if (syntaxNode.Name.Equals("TransferSyntax"))
                {
                    String xmlUid = syntaxNode.Attributes["uid"].Value;

                    if (xmlUid.Equals(uid))
                    {
                        littleEndian = syntaxNode.Attributes["littleEndian"].Value;
                        encapsulated = syntaxNode.Attributes["encapsulated"].Value;
                        explicitVR = syntaxNode.Attributes["explicitVR"].Value;
                        deflated = syntaxNode.Attributes["deflated"].Value;
						lossy = syntaxNode.Attributes["lossy"].Value;
						lossless = syntaxNode.Attributes["lossless"].Value;
						return;
                    }
                }
                syntaxNode = syntaxNode.NextSibling;
            }
        }

        /// <summary>
        /// Create the TransferSyntax.cs file.
        /// </summary>
        /// <param name="syntaxFile"></param>
        public void WriteTransferSyntaxes(String syntaxFile)
        {
            var writer = new StreamWriter(syntaxFile);

            WriterHeader(writer);
            writer.WriteLine("    /// <summary>");
            writer.WriteLine("    /// Enumerated value to differentiate between little and big endian.");
            writer.WriteLine("    /// </summary>");
            writer.WriteLine("    public enum Endian");
            writer.WriteLine("    {");
            writer.WriteLine("        Little,");
            writer.WriteLine("        Big");
            writer.WriteLine("    }");
            writer.WriteLine("");

            writer.WriteLine("    /// <summary>");
            writer.WriteLine("    /// This class contains transfer syntax definitions.");
            writer.WriteLine("    /// </summary>");
            writer.WriteLine("    public class TransferSyntax");
            writer.WriteLine("    {");

            IEnumerator iter = _tSyntaxList.GetEnumerator();

            while (iter.MoveNext())
            {
                var tSyntax = (SopClass)((DictionaryEntry)iter.Current).Value;

                writer.WriteLine("        /// <summary>String representing");
                writer.WriteLine("        /// <para>" + tSyntax.Name + "</para>");
                writer.WriteLine("        /// <para>UID: " + tSyntax.Uid + "</para>");
                writer.WriteLine("        /// </summary>");
                writer.WriteLine("        public static readonly String " + tSyntax.VarName + "Uid = \"" + tSyntax.Uid + "\";");
                writer.WriteLine("");

                String littleEndian = "";
                String encapsulated = "";
                String explicitVR = "";
                String deflated = "";
            	String lossless = "";
            	string lossy = "";

                GetTransferSyntaxDetails(tSyntax.Uid, ref littleEndian, ref encapsulated, ref explicitVR, ref deflated, ref lossy, ref lossless);

                writer.WriteLine("        /// <summary>TransferSyntax object representing");
                writer.WriteLine("        /// <para>" + tSyntax.Name + "</para>");
                writer.WriteLine("        /// <para>UID: " + tSyntax.Uid + "</para>");
                writer.WriteLine("        /// </summary>");
                writer.WriteLine("        public static readonly TransferSyntax " + tSyntax.VarName + " =");
                writer.WriteLine("                    new TransferSyntax(\"" + tSyntax.Name + "\",");
                writer.WriteLine("                                 " + tSyntax.VarName + "Uid,");
                writer.WriteLine("                                 " + littleEndian + ", // Little Endian?");
                writer.WriteLine("                                 " + encapsulated + ", // Encapsulated?");
                writer.WriteLine("                                 " + explicitVR + ", // Explicit VR?");
                writer.WriteLine("                                 " + deflated + ", // Deflated?");
				writer.WriteLine("                                 " + lossy + ", // lossy?");
				writer.WriteLine("                                 " + lossless + " // lossless?");
				writer.WriteLine("                                 );");
                writer.WriteLine("");
            }

            writer.WriteLine("        // Internal members");
            writer.WriteLine("        private static readonly Dictionary<String,TransferSyntax> _transferSyntaxes = new Dictionary<String,TransferSyntax>();");
            writer.WriteLine("        private readonly bool _littleEndian;");
            writer.WriteLine("        private readonly bool _encapsulated;");
            writer.WriteLine("        private readonly bool _explicitVr;");
            writer.WriteLine("        private readonly bool _deflate;");
			writer.WriteLine("        private readonly bool _lossless;");
			writer.WriteLine("        private readonly bool _lossy;");
			writer.WriteLine("        private readonly String _name;");
            writer.WriteLine("        private readonly String _uid;");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>");
            writer.WriteLine("        /// Constructor for transfer syntax objects");
            writer.WriteLine("        ///</summary>");
            writer.WriteLine("        public TransferSyntax(String name, String uid, bool bLittleEndian, bool bEncapsulated, bool bExplicitVr, bool bDeflate, bool bLossy, bool bLossless)");
            writer.WriteLine("        {");
            writer.WriteLine("            _uid = uid;");
            writer.WriteLine("            _name = name;");
            writer.WriteLine("            _littleEndian = bLittleEndian;");
            writer.WriteLine("            _encapsulated = bEncapsulated;");
            writer.WriteLine("            _explicitVr = bExplicitVr;");
            writer.WriteLine("            _deflate = bDeflate;");
			writer.WriteLine("            _lossy = bLossy;");
			writer.WriteLine("            _lossless = bLossless;");
			writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>Override to the ToString() method, returns the name of the transfer syntax.</summary>");
            writer.WriteLine("        public override String ToString()");
            writer.WriteLine("        {");
            writer.WriteLine("            return _name;");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>Property representing the UID string of transfer syntax.</summary>");
            writer.WriteLine("        public String UidString");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _uid; }");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>Property representing the DicomUid of the transfer syntax.</summary>");
            writer.WriteLine("        public DicomUid DicomUid");
            writer.WriteLine("        {");
            writer.WriteLine("            get");
            writer.WriteLine("            {");
            writer.WriteLine("                return new DicomUid(_uid, _name, UidType.TransferSyntax);");
            writer.WriteLine("            }");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>Property representing the name of the transfer syntax.</summary>");
            writer.WriteLine("        public String Name");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _name; }");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>Property representing if the transfer syntax is encoded as little endian.</summary>");
            writer.WriteLine("        public bool LittleEndian");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _littleEndian; }");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>Property representing the Endian enumerated value for the transfer syntax.</summary>");
            writer.WriteLine("        public Endian Endian");
            writer.WriteLine("        {");
            writer.WriteLine("            get");
            writer.WriteLine("            {");
            writer.WriteLine("                if (_littleEndian)");
            writer.WriteLine("                    return Endian.Little;");
            writer.WriteLine("");
            writer.WriteLine("                return Endian.Big;");
            writer.WriteLine("            }");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>Property representing if the transfer syntax is encoded as encapsulated.</summary>");
            writer.WriteLine("        public bool Encapsulated");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _encapsulated; }");
            writer.WriteLine("        }");
            writer.WriteLine("");
			writer.WriteLine("        ///<summary>Property representing if the transfer syntax is a lossy compression syntax.</summary>");
			writer.WriteLine("        public bool LossyCompressed");
			writer.WriteLine("        {");
			writer.WriteLine("            get { return _lossy; }");
			writer.WriteLine("        }");
			writer.WriteLine("");
			writer.WriteLine("        ///<summary>Property representing if the transfer syntax is a lossless compression syntax.</summary>");
			writer.WriteLine("        public bool LosslessCompressed");
			writer.WriteLine("        {");
			writer.WriteLine("            get { return _lossless; }");
			writer.WriteLine("        }");
			writer.WriteLine("");
			writer.WriteLine("        ///<summary>Property representing if the transfer syntax is encoded as explicit Value Representation.</summary>");
            writer.WriteLine("        public bool ExplicitVr");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _explicitVr; }");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        ///<summary>Property representing if the transfer syntax is encoded in deflate format.</summary>");
            writer.WriteLine("        public bool Deflate");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _deflate; }");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        /// <summary>");
            writer.WriteLine("        /// Get a TransferSyntax object for a specific transfer syntax UID.");
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        public static TransferSyntax GetTransferSyntax(String uid)");
            writer.WriteLine("        {");
			writer.WriteLine("            TransferSyntax theSyntax;");
			writer.WriteLine("            if (!_transferSyntaxes.TryGetValue(uid, out theSyntax))");
            writer.WriteLine("                return null;");
            writer.WriteLine("");
			writer.WriteLine("            return theSyntax;");
            writer.WriteLine("        }");
            writer.WriteLine("");
            
            writer.WriteLine("        static TransferSyntax()");
            writer.WriteLine("        {");

            iter = _tSyntaxList.GetEnumerator();

            while (iter.MoveNext())
            {
                var tSyntax = (SopClass)((DictionaryEntry)iter.Current).Value;

                writer.WriteLine("            _transferSyntaxes.Add(" + tSyntax.VarName + "Uid,");
                writer.WriteLine("                                  " + tSyntax.VarName + ");");
                writer.WriteLine("");
            }

            writer.WriteLine("        }");

            writer.WriteLine("    }");
            WriterFooter(writer);

            writer.Close();
        }

        /// <summary>
        /// Create the SopClass.cs file.
        /// </summary>
        /// <param name="sopsFile"></param>
        public void WriteSqlInsert(String sopsFile)
        {
            var writer = new StreamWriter(sopsFile);

            WriterHeader(writer);

            IEnumerator iter = _sopList.GetEnumerator();

            while (iter.MoveNext())
            {
                var sopClass = (SopClass)((DictionaryEntry)iter.Current).Value;                
                
                writer.WriteLine("INSERT INTO [ImageServer].[dbo].[SopClass] ([GUID],[SopClassUid],[Description],[NonImage])");
                if (sopClass.Name.ToLower().Contains("image"))
                    writer.WriteLine("VALUES (newid(), '" + sopClass.Uid + "', '" + sopClass.Name + "', 0);");
                else
                    writer.WriteLine("VALUES (newid(), '" + sopClass.Uid + "', '" + sopClass.Name + "', 1);");
                writer.WriteLine("");
                
            }


            writer.Close();
        }

        /// <summary>
        /// Create the SopClass.cs file.
        /// </summary>
        /// <param name="tagsFile"></param>
        public void WriteTagsText(String tagsFile)
        {
            var writer = new StreamWriter(tagsFile);

            writer.WriteLine("# Copyright (c) 2012, Macro Inc.");
            writer.WriteLine("# All rights reserved.");
            writer.WriteLine("# http://www.Macro.ca");
            writer.WriteLine("#");
            writer.WriteLine("# generated from DICOM standard");
            writer.WriteLine("# columns are in the same order as parameters to the DicomTag constructor");
            writer.WriteLine("# eg. tag;name;varName;vr;isMultiVrTag;vmLow;vmHigh;isRetired");
            writer.WriteLine("#");

            IEnumerator<Tag> iter = _tagList.Values.GetEnumerator();

            while (iter.MoveNext())
            {
                Tag tag = iter.Current;
                uint vmLow = 0;
                uint vmHigh = 0;
                var charSeparators = new[] {'�', '-'};

                String[] nodes = tag.vm.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                if (nodes.Length == 1)
                {
                    vmLow = uint.Parse(nodes[0]);
                    vmHigh = vmLow;
                }
                else if (nodes.Length == 2)
                {
                    if (nodes[0].Contains("N") || nodes[0].Contains("n"))
                    {
                        vmLow = 1;
                    }
                    else
                    {
                        vmLow = uint.Parse(nodes[0]);
                    }
                    if (nodes[1].Contains("N") || nodes[1].Contains("n"))
                    {
                        vmHigh = UInt32.MaxValue;
                    }
                    else
                    {
                        vmHigh = uint.Parse(nodes[1]);
                    }
                }
                string vr;
                bool isMultiVrTag;
                if (tag.vr.Contains("or"))
                {
                    if (tag.varName.Equals("PixelData"))
                    {
                        vr = "OW";
                        isMultiVrTag = true;
                    }
                    else
                    {
                            // Just take the first VR listed
                        vr = tag.vr.Substring(0, 2);
                        isMultiVrTag = true;
                    }
                }
                else
                {
                    vr = tag.vr;
                    isMultiVrTag = false;
                }
                writer.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}",
                    tag.nTag, tag.unEscapedName, tag.varName, vr, isMultiVrTag, vmLow, vmHigh, !string.IsNullOrEmpty(tag.retired));
            }

            writer.Close();
        }

        /// <summary>
        /// Create the SopClass.cs file.
        /// </summary>
        /// <param name="sopsFile"></param>
        public void WriteSopClasses(String sopsFile)
        {
            var writer = new StreamWriter(sopsFile);

            WriterHeader(writer);

            writer.WriteLine("    /// <summary>");
            writer.WriteLine("    /// This class contains defines for all DICOM SOP Classes.");
            writer.WriteLine("    /// </summary>");
            writer.WriteLine("    public class SopClass");
            writer.WriteLine("    {");

            IEnumerator iter = _sopList.GetEnumerator();

            while (iter.MoveNext())
            {
                var sopClass = (SopClass)((DictionaryEntry)iter.Current).Value;

                writer.WriteLine("        /// <summary>");
                writer.WriteLine("        /// <para>" + sopClass.Name + "</para>");
                writer.WriteLine("        /// <para>UID: " + sopClass.Uid + "</para>");
                writer.WriteLine("        /// </summary>");
                writer.WriteLine("        public static readonly String " + sopClass.VarName + "Uid = \"" + sopClass.Uid + "\";");
                writer.WriteLine("");
                writer.WriteLine("        /// <summary>SopClass for");
                writer.WriteLine("        /// <para>" + sopClass.Name + "</para>");
                writer.WriteLine("        /// <para>UID: " + sopClass.Uid + "</para>");
                writer.WriteLine("        /// </summary>");
                writer.WriteLine("        public static readonly SopClass " + sopClass.VarName + " =");
                writer.WriteLine("                             new SopClass(\"" + sopClass.Name + "\", ");
                writer.WriteLine("                                          " + sopClass.VarName + "Uid, ");
                writer.WriteLine("                                          false);");
                writer.WriteLine("");

            }

            iter = _metaSopList.GetEnumerator();

            while (iter.MoveNext())
            {
                var sopClass = (SopClass)((DictionaryEntry)iter.Current).Value;

                writer.WriteLine("        /// <summary>String UID for");
                writer.WriteLine("        /// <para>" + sopClass.Name + "</para>");
                writer.WriteLine("        /// <para>UID: " + sopClass.Uid + "</para>");
                writer.WriteLine("        /// </summary>");
                writer.WriteLine("        public static readonly String " + sopClass.VarName + "Uid = \"" + sopClass.Uid + "\";");
                writer.WriteLine("");
                writer.WriteLine("        /// <summary>SopClass for");
                writer.WriteLine("        /// <para>" + sopClass.Name + "</para>");
                writer.WriteLine("        /// <para>UID: " + sopClass.Uid + "</para>");
                writer.WriteLine("        /// </summary>");
                writer.WriteLine("        public static readonly SopClass " + sopClass.VarName + " =");
                writer.WriteLine("                             new SopClass(\"" + sopClass.Name + "\", ");
                writer.WriteLine("                                          " + sopClass.VarName + "Uid, ");
                writer.WriteLine("                                          true);");
            }


            /*
             * Now, write out the constructor and the actual class
             */
            writer.WriteLine("");
            writer.WriteLine("        private readonly String _sopName;");
			writer.WriteLine("        private readonly String _sopUid;");
			writer.WriteLine("        private readonly bool _bIsMeta;");
            writer.WriteLine("");
            writer.WriteLine("        /// <summary> Property that represents the Name of the SOP Class. </summary>");
            writer.WriteLine("        public String Name");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _sopName; }");
            writer.WriteLine("        }");
            writer.WriteLine("        /// <summary> Property that represents the Uid for the SOP Class. </summary>");
            writer.WriteLine("        public String Uid");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _sopUid; }");
            writer.WriteLine("        }");
            writer.WriteLine("        /// <summary> Property that returns a DicomUid that represents the SOP Class. </summary>");
            writer.WriteLine("        public DicomUid DicomUid");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return new DicomUid(_sopUid,_sopName,_bIsMeta ? UidType.MetaSOPClass : UidType.SOPClass); }");
            writer.WriteLine("        }");
            writer.WriteLine("        /// <summary> Property that represents the Uid for the SOP Class. </summary>");
            writer.WriteLine("        public bool Meta");
            writer.WriteLine("        {");
            writer.WriteLine("            get { return _bIsMeta; }");
            writer.WriteLine("        }");
            writer.WriteLine("        /// <summary> Constructor to create SopClass object. </summary>");
            writer.WriteLine("        public SopClass(String name,");
            writer.WriteLine("                           String uid,");
            writer.WriteLine("                           bool isMeta)");
            writer.WriteLine("        {");
            writer.WriteLine("            _sopName = name;");
            writer.WriteLine("            _sopUid = uid;");
            writer.WriteLine("            _bIsMeta = isMeta;");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        private static Dictionary<String,SopClass> _sopList = new Dictionary<String,SopClass>();");
            writer.WriteLine("");
            writer.WriteLine("        /// <summary>Override that displays the name of the SOP Class.</summary>");
            writer.WriteLine("        public override string ToString()");
            writer.WriteLine("        {");
            writer.WriteLine("            return this.Name;");
            writer.WriteLine("        }");
            writer.WriteLine("");
            writer.WriteLine("        /// <summary>Retrieve a SopClass object associated with the Uid.</summary>");
            writer.WriteLine("        public static SopClass GetSopClass(String uid)");
            writer.WriteLine("        {");
			writer.WriteLine("            SopClass theSop;");
			writer.WriteLine("            if (!_sopList.TryGetValue(uid, out theSop))");
            writer.WriteLine("            {");
            writer.WriteLine("                theSop = new SopClass(string.Format(\"Generated: '{0}'\", uid), uid, false); ");
            writer.WriteLine("                _sopList.Add(uid, theSop);  ");
            writer.WriteLine("            }");
            writer.WriteLine("            return theSop;");
            writer.WriteLine("        }");

            writer.WriteLine("        static SopClass()");
            writer.WriteLine("        {");

            // Standard Sops
            iter = _sopList.GetEnumerator();

            while (iter.MoveNext())
            {
                var sopClass = (SopClass)((DictionaryEntry)iter.Current).Value;

                writer.WriteLine("            _sopList.Add(" + sopClass.VarName + "Uid, ");
                writer.WriteLine("                         " + sopClass.VarName + ");");
                writer.WriteLine("");
            }

            // Now, Meta Sops
            iter = _metaSopList.GetEnumerator();

            while (iter.MoveNext())
            {
                var sopClass = (SopClass)((DictionaryEntry)iter.Current).Value;

                writer.WriteLine("            _sopList.Add(" + sopClass.VarName + "Uid, ");
                writer.WriteLine("                         " + sopClass.VarName + ");");
                writer.WriteLine("");
            }
            writer.WriteLine("        }");
            writer.WriteLine("");
            
            writer.WriteLine("    }");
            WriterFooter(writer);

            writer.Close();
        }
    }
}
