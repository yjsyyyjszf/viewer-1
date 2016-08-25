
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
using System.Linq;
using Macro.Common;
using Macro.Common.Utilities;
using Macro.ImageViewer.Annotations;
using Macro.ImageViewer.StudyManagement;

namespace Macro.ImageViewer.AnnotationProviders.Presentation
{
	[ExtensionPoint]
	public sealed class LossyImagePresentationInfoProviderExtensionPoint : ExtensionPoint<ILossyImagePresentationInfoProvider> {}

	public interface ILossyImagePresentationInfoProvider
	{
		bool GetLossyImagePresentation(IPresentationImage presentationImage, out string lossyCompressionRatio);
	}

	internal sealed class LossyImagePresentationAnnotationItem : AnnotationItem
	{
		private readonly IList<ILossyImagePresentationInfoProvider> _providers;

		public LossyImagePresentationAnnotationItem()
			: this("Presentation.Lossy", new AnnotationResourceResolver(typeof (LossyImagePresentationAnnotationItem).Assembly)) {}

		public LossyImagePresentationAnnotationItem(string identifier, IAnnotationResourceResolver annotationResourceResolver)
			: base(identifier, annotationResourceResolver)
		{
			_providers = new LossyImagePresentationInfoProviderExtensionPoint().CreateExtensions().Cast<ILossyImagePresentationInfoProvider>().ToList().AsReadOnly();
		}

		public override string GetAnnotationText(IPresentationImage presentationImage)
		{
			var lossyPresentation = false;
			var lossyPresentationDescription = string.Empty;

			var imageSopProvider = presentationImage as IImageSopProvider;
			if (imageSopProvider != null)
				lossyPresentation = GetLossyImageCompression(imageSopProvider.Frame, out lossyPresentationDescription);

			foreach (var provider in _providers)
			{
				string description;
				var lossy = provider.GetLossyImagePresentation(presentationImage, out description);

				if (!string.IsNullOrWhiteSpace(description))
				{
					// if a provider returns a value for lossy presentation description, assume result is lossy
					lossyPresentation = true;

					// append the description to the ratio string
					if (string.IsNullOrEmpty(lossyPresentationDescription))
						lossyPresentationDescription = description;
					else
						lossyPresentationDescription += SR.SeparatorLossyCompressionRatio + description;
				}
				else if (lossy)
				{
					// if a provider indicates that presentation is lossy, the result is lossy
					lossyPresentation = true;
				}

				// even if a provider indicates presentation is lossless, result may still be lossy due to source or other applicable presentation layers
			}

			if (!string.IsNullOrWhiteSpace(lossyPresentationDescription))
				return string.Format(SR.FormatLossyCompressionRatio, SR.ValueLossy, lossyPresentationDescription);
			return lossyPresentation ? SR.ValueLossy : string.Empty;
		}

		private static bool GetLossyImageCompression(Frame frame, out string ratios)
		{
			ratios = string.Empty;

			if (frame.LossyImageCompressionRatio.Length > 0)
			{
				var lossyRatios = StringUtilities.Combine(frame.LossyImageCompressionRatio, SR.SeparatorLossyCompressionRatio, "F1");
				if (!string.IsNullOrEmpty(lossyRatios))
				{
					ratios = lossyRatios;
					return true;
				}
			}

			if (!string.IsNullOrEmpty(frame.LossyImageCompression))
			{
				int lossyValue;
				if (int.TryParse(frame.LossyImageCompression, out lossyValue) && lossyValue != 0)
					return true;
			}

			return false;
		}
	}
}