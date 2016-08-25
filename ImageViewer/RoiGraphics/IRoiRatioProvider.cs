
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
using Macro.ImageViewer.Imaging;
using Macro.ImageViewer.RoiGraphics.Analyzers;

namespace Macro.ImageViewer.RoiGraphics
{
    public interface IRoiRatioProvider
    {

        /// <summary>
        /// Gets or sets the units of length with which to compute the value of <see cref="Length"/>.
        /// </summary>
        Units Units { get; set; }

        /// <summary>
        /// 第一条线的长度
        /// </summary>
        double Line1Lenght { get; }

        /// <summary>
        /// 第三条线的长度
        /// </summary>
        double Line3Lenght { get; }

        /// <summary>
        /// 第一条线和第三条线的比例
        /// </summary>
        double Ratio { get; }

    }

    internal class RoiRatio
    {
        public readonly double Line1Length;
        public readonly double Line3Length;
        public readonly double Ratio;
        public readonly bool Valid;


        private RoiRatio()
        {
            this.Valid = false;
        }


        private RoiRatio(double line1Length, double line3Length, double ratio)
        {
            this.Valid = true;
            this.Line1Length = line1Length;
            this.Line3Length = line3Length;
            this.Ratio = ratio;
        }

        public static RoiRatio Calculate(Roi roi)
        {
            if (!(roi.PixelData is GrayscalePixelData))
                return new RoiRatio();

            if (roi is LinearCardiothoracicRatioRoi)
            {

                var ratioRoi = roi as LinearCardiothoracicRatioRoi;

                //计算第一条线的长度
                double line1 = CalculateLenght(ratioRoi.Points[0], ratioRoi.Points[1], ratioRoi);
                //计算第三条线的长度
                double line2 = CalculateLenght(ratioRoi.Points[2], ratioRoi.Points[3], ratioRoi);
                //计算比例
                double ratio = CalculateRatio(line1, line2);
                ratio = Math.Round(ratio * 10000) / 100;
                return new RoiRatio(line1, line2, ratio);
            }

            return new RoiRatio();

        }

        private static double CalculateLenght(PointF pointf1, PointF pointf2, Roi roi)
        {
            Units units = Units.Centimeters;

            double lenght = RoiLengthAnalyzer.CalculateLength(pointf1, pointf2, roi.NormalizedPixelSpacing, ref units);
            return lenght;
        }

        private static double CalculateRatio(double line1Length, double line3Length)
        {
            if (line1Length == 0 || line3Length == 0)
            {
                return 0;
            }

            double ratio = 0;
            if (line1Length > line3Length)
            {
                ratio = line3Length / line1Length;
            }
            else
            {
                ratio = line1Length / line3Length;
            }

            return ratio;
        }
    }
}
