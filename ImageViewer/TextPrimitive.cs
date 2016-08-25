
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
using System.Linq;
using System.Text;
using Macro.Common;
using Macro.Common.Utilities;
using Macro.ImageViewer.Graphics;
using Macro.ImageViewer.Mathematics;
using Macro.ImageViewer.PresentationStates.Dicom;
using Macro.ImageViewer.PresentationStates.Dicom.GraphicAnnotationSerializers;
using Macro.ImageViewer.Rendering;

namespace Macro.ImageViewer
{
    [Cloneable(true)]
    [DicomSerializableGraphicAnnotation(typeof(TextGraphicAnnotationSerializer))]
    public class TextPrimitive : InvariantTextPrimitive
    {
        private float _scale = -1;
        public TextPrimitive():base()
        {}

        /// <summary>
        /// Initializes a new instance of <see cref="InvariantTextPrimitive"/> with
        /// the specified text.
        /// </summary>
        /// <param name="text"></param>
        public TextPrimitive(string text)
            :base(text)
        {
            _text = text;
            _sizeInPoints = 30;
        }

   
        public float Scale
        {
            set { _scale = value; }
            get { return _scale; }
        }

        /// <summary>
        /// Gets or sets the size in points.
        /// </summary>
        /// <remarks>
        /// Default value is 10 points.
        /// </remarks>
        public override float SizeInPoints
        {
            get
            {
                if (_scale == -1)
                {
                    Scale = _sizeInPoints / (float)this.ParentPresentationImage.ClientRectangle.Height;
                }

                _sizeInPoints = Scale * this.ParentPresentationImage.ClientRectangle.Height;

                if (_sizeInPoints <= 0)
                {
                    _sizeInPoints = 30;
                }
                return _sizeInPoints;

            }
            set
            {
                if (!FloatComparer.AreEqual(_sizeInPoints, value))
                {
                    _sizeInPoints = value;
                    base.NotifyVisualStateChanged("SizeInPoints");
                }
            }
        }

    }
}
