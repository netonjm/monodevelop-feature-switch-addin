﻿//
// FeatureSwitchController.cs
//
// Author:
//       Matt Ward <matt.ward@microsoft.com>
//
// Copyright (c) 2019 Microsoft Corporation
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using MonoDevelop.Core.FeatureConfiguration;

namespace MonoDevelop.FeatureSwitch
{
	class FeatureSwitchController : IFeatureSwitchController
	{
		static bool bypass;

		public bool? IsFeatureEnabled (string featureName)
		{
			if (bypass)
				return null;

			FeatureSwitch feature = FeatureSwitchConfigurations.GetFeature (featureName);
			if (feature == null) {
				bool? enabled = IsFeatureEnabledIgnoringConfiguration (featureName);
				FeatureSwitchConfigurations.AddFeature (featureName, enabled);
			}

			return null;
		}

		internal static bool? IsFeatureEnabledIgnoringConfiguration (string featureName)
		{
			try {
				bypass = true;
				return FeatureSwitchService.IsFeatureEnabled (featureName);
			} finally {
				bypass = false;
			}
		}
	}
}
