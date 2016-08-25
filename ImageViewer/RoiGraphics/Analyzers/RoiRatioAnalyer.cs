
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
using Macro.ImageViewer.Imaging;

namespace Macro.ImageViewer.RoiGraphics.Analyzers
{
    [ExtensionOf(typeof(RoiAnalyzerExtensionPoint))]
    public class RoiRatioAnalyer : IRoiAnalyzer
    {

        private RoiAnalyzerUpdateCallback _updateCallback;

        #region IRoiAnalyzer 成员

        Units IRoiAnalyzer.Units
        {
            get { return Units.Centimeters; }
            set { }
        }

        public bool SupportsRoi(Roi roi)
        {
            return roi is IRoiRatioProvider;
        }

        public IRoiAnalyzerResult Analyze(Roi roi, RoiAnalysisMode mode)
        {
            if (!SupportsRoi(roi))
            {
                return null;
            }

            IRoiRatioProvider ratioProvider = (IRoiRatioProvider)roi;
            MultiValueRoiAnalyzerResult result = new MultiValueRoiAnalyzerResult("Ratio") { ShowHeader = false };

            bool isGrayscale = roi.PixelData is GrayscalePixelData;

            var line1LengthValue = SR.StringNotApplicable;
            var line3LengthValue = SR.StringNotApplicable;
            var ratioValue = SR.StringNotApplicable;

            if (isGrayscale && roi.ContainsPixelData)
            {
                if (mode == RoiAnalysisMode.Responsive)
                {
                    line1LengthValue = line3LengthValue = ratioValue = SR.StringNoValue;
                    result.Add(new RoiAnalyzerResultNoValue("Line1", String.Format(SR.FormatLengthCm, line1LengthValue)));
                    result.Add(new RoiAnalyzerResultNoValue("Line3", String.Format(SR.FormatLengthCm, line3LengthValue)));
                    result.Add(new RoiAnalyzerResultNoValue("Ratio", String.Format(SR.FormatRatio, ratioValue)));

                }
                else
                {

                    var line1Length = ratioProvider.Line1Lenght;
                    var line3Length = ratioProvider.Line3Lenght;
                    var ratio = ratioProvider.Ratio;
                    var units = ratioProvider.Units.ToString();

                    result.Add(new SingleValueRoiAnalyzerResult("Line1", units, line1Length,
                                                                       String.Format("Line1: " + SR.FormatLengthCm, line1Length)));
                    result.Add(new SingleValueRoiAnalyzerResult("Line3", units, line3Length,
                                                                       String.Format("Line3: " + SR.FormatLengthCm, line3Length)));
                    result.Add(new SingleValueRoiAnalyzerResult("Ratio", units, ratio,
                                                                     String.Format(SR.FormatRatio, ratio)));



                }
            }
            else
            {
                result.Add(new RoiAnalyzerResultNoValue("Line1", String.Format(SR.FormatLengthCm, line1LengthValue)));
                result.Add(new RoiAnalyzerResultNoValue("Line3", String.Format(SR.FormatLengthCm, line3LengthValue)));
                result.Add(new RoiAnalyzerResultNoValue("Ratio", String.Format(SR.FormatRatio, ratioValue)));

            }

            return result;

        }

        public void SetRoiAnalyzerUpdateCallback(RoiAnalyzerUpdateCallback callback)
        {
            _updateCallback = callback;
        }

        #endregion
    }
}
