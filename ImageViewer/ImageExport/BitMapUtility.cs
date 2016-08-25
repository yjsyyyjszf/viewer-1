
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
using System.Drawing;
using System.Drawing.Imaging;
using Macro.ImageViewer.Common;

namespace Macro.ImageViewer.ImageExport
{
    public static class BitMapUtility
    {
        private static byte[] AllocateByteArray(int index)
        {
            System.Boolean ReflectorVariable0;
            if ((index % 2) != 0)
            {
                ReflectorVariable0 = true;
            }
            else
            {
                ReflectorVariable0 = false;
            }
            int count = ReflectorVariable0 ? (index + 1) : index;
            return MemoryManager.Allocate<byte>(count);
        }

        public static unsafe byte[] GetBitmap(Bitmap bitmap, Macro.Dicom.Network.Scu.ColorMode colorMode)
        {
            int depth = 0;
            int width = 0;
            int height = 0;
            byte[] buffer;
            if (colorMode != Macro.Dicom.Network.Scu.ColorMode.Grayscale)
            {
                depth = 3;
            }
            else
            {
                depth = 1;
            }
            width = bitmap.Width;
            height = bitmap.Height;
            buffer = BitMapUtility.AllocateByteArray((depth * width) * height);
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            if ((buffer != null) && (buffer.Length != 0))
            {
                fixed (byte* dis = buffer)
                {
                    IntPtr Iptr = bitmapdata.Scan0;
                    byte* sourcePtr = (byte*)Iptr.ToPointer();
                    byte* target = dis;

                    if (colorMode == Macro.Dicom.Network.Scu.ColorMode.Color)
                    {
                        for (int i = 0; i < bitmap.Height; i++)
                        {
                            for (int j = 0; j < bitmap.Width; j++)
                            {
                                target[0] = sourcePtr[2];
                                target[1] = sourcePtr[1];
                                target[2] = sourcePtr[0];
                                target += depth;
                                sourcePtr += 4;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < bitmap.Height; i++)
                        {
                            for (int j = 0; j < bitmap.Width; j++)
                            {
                                byte first = sourcePtr[0];
                                byte second = sourcePtr[1];
                                byte third = sourcePtr[2];
                                if ((second == third) && (second == first))
                                {
                                    target[0] = third;
                                }
                                else
                                {
                                    double forth = ((third * 0.3) + (second * 0.59)) + (first * 0.11);
                                    if (forth > 255.0)
                                    {
                                        forth = 255.0;
                                    }
                                    if (forth < 0)
                                    {
                                        forth = 0;
                                    }
                                    target[0] = (byte)forth;
                                }
                                target += depth;
                                sourcePtr += 4;
                            }
                        }
                    }
                    bitmap.UnlockBits(bitmapdata);
                }
            }

            return buffer;
        }
    }
}

