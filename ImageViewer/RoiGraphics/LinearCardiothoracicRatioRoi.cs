
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
using System.Drawing;
using Macro.Common;
using Macro.ImageViewer.Graphics;
using Macro.ImageViewer.Mathematics;

namespace Macro.ImageViewer.RoiGraphics
{

    public class LinearCardiothoracicRatioRoi : Roi,IRoiRatioProvider
    {

        private readonly IList<PointF> _points;
        private Units _units;

        /// <summary>
		/// Constructs a new linear region of interest, specifying an <see cref="IPointsGraphic"/> as the source of the definition and pixel data.
		/// </summary>
		/// <param name="polyline">The linear graphic that represents the region of interest.</param>
        public LinearCardiothoracicRatioRoi(IPointsGraphic polyline)
            : base(polyline.ParentPresentationImage)
		{
			polyline.CoordinateSystem = CoordinateSystem.Source;
			try
			{
				List<PointF> points = new List<PointF>();
				foreach(PointF point in polyline.Points)
				{
					points.Add(point);
				}
				_points = points.AsReadOnly();
			}
			finally
			{
				polyline.ResetCoordinateSystem();
			}

			Platform.CheckTrue(_points.Count == 4, "At least 4 points must be specified.");
		}


        /// <summary>
        /// Called by <see cref="Roi.BoundingBox"/> to compute the tightest bounding box of the region of interest.
        /// </summary>
        /// <remarks>
        /// <para>This method is only called once and the result is cached for future accesses.</para>
        /// <para>
        /// Regions of interest have no notion of coordinate system. All coordinates are inherently
        /// given relative to the image pixel space (i.e. <see cref="CoordinateSystem.Source"/>.)
        /// </para>
        /// </remarks>
        /// <returns>A rectangle defining the bounding box.</returns>
        protected override RectangleF ComputeBounds()
        {
            return RectangleUtilities.ComputeBoundingRectangle(_points);
        }

        /// <summary>
        /// Creates a copy of this <see cref="Roi"/> using the same region of interest shape but using a different image as the source pixel data.
        /// </summary>
        /// <param name="presentationImage">The image upon which to copy this region of interest.</param>
        /// <returns>A new <see cref="Roi"/> of the same type and shape as the current region of interest.</returns>
        public override Roi CopyTo(IPresentationImage presentationImage)
        {
            return new LinearRoi(_points, presentationImage);
        }

        /// <summary>
        /// Tests to see if the given point is contained within the region of interest.
        /// </summary>
        /// <remarks>
        /// Regions of interest have no notion of coordinate system. All coordinates are inherently
        /// given relative to the image pixel space (i.e. <see cref="CoordinateSystem.Source"/>.)
        /// </remarks>
        /// <param name="point">The point to test.</param>
        /// <returns>True if the point is defined as within the region of interest; False otherwise.</returns>
        public override bool Contains(PointF point)
        {
            //TODO (cr Feb 2010): use a graphics path, or orthogonal distance to line?  This seems inaccurate.

            PointF topLeft = new PointF((float)Math.Floor(point.X), (float)Math.Floor(point.Y));
            for (int n = 1; n < _points.Count; n++)
            {
                PointF intersection;
                if (Vector.IntersectLineSegments(topLeft, topLeft + new SizeF(1, 1), _points[n - 1], _points[n], out intersection))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Gets the points that define the linear region of interest.
        /// </summary>
        public IList<PointF> Points
        {
            get { return _points; }
        }

        #region IRoiRatioProvider 成员

        private RoiRatio _roiRatio;

        public double Line1Lenght
        {
            get
            {
                if (_roiRatio==null)
                {
                    _roiRatio = RoiRatio.Calculate(this);
                }
                return _roiRatio.Line1Length;
            }
        }

        public double Line3Lenght
        {
            get
            {
                if (_roiRatio == null)
                {
                    _roiRatio = RoiRatio.Calculate(this);
                }
                return _roiRatio.Line3Length;
            }
        }

        public double Ratio
        {
            get
            {
                if (_roiRatio == null)
                {
                    _roiRatio = RoiRatio.Calculate(this);
                }
                return _roiRatio.Ratio;
            }
        }

        public Units Units
        {
            get { return _units; }
            set { _units = value; }
        }

        #endregion
    }
}
