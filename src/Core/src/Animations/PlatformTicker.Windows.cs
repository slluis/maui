﻿using Windows.ApplicationModel.Core;
using System;
using Microsoft.UI.Xaml.Media;

namespace Microsoft.Maui.Animations
{
	public class PlatformTicker : Ticker
	{
		public override void Start()
		{
			CompositionTarget.Rendering += RenderingFrameEventHandler;
		}

		public override void Stop()
		{
			CompositionTarget.Rendering -= RenderingFrameEventHandler;
		}

		void RenderingFrameEventHandler(object? sender, object? args)
		{
			Fire?.Invoke();
		}
	}
}