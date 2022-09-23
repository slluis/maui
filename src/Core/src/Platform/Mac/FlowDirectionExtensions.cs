using System;
using AppKit;

namespace Microsoft.Maui.Platform
{
	internal static class FlowDirectionExtensions
	{
		internal static FlowDirection ToFlowDirection(this NSUserInterfaceLayoutDirection direction)
		{
			switch (direction)
			{
				case NSUserInterfaceLayoutDirection.LeftToRight:
					return FlowDirection.LeftToRight;
				case NSUserInterfaceLayoutDirection.RightToLeft:
					return FlowDirection.RightToLeft;
				default:
					throw new NotSupportedException($"ToFlowDirection: {direction}");
			}
		}
	}
}