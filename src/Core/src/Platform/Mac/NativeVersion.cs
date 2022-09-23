using AppKit;

namespace Microsoft.Maui.Platform
{
	public static class NativeVersion
	{
		public static bool IsAtLeast(int version)
		{
			// TODO COCOA
			return true;
		}

		private static bool? SetNeedsUpdateOfHomeIndicatorAutoHidden;

		public static bool Supports(string capability)
		{
			switch (capability)
			{
				case NativeApis.RespondsToSetNeedsUpdateOfHomeIndicatorAutoHidden:
					if (!SetNeedsUpdateOfHomeIndicatorAutoHidden.HasValue)
					{
						SetNeedsUpdateOfHomeIndicatorAutoHidden = new NSViewController().RespondsToSelector(new ObjCRuntime.Selector("setNeedsUpdateOfHomeIndicatorAutoHidden"));
					}
					return SetNeedsUpdateOfHomeIndicatorAutoHidden.Value;
			}

			return false;
		}

		public static bool Supports(int capability)
		{
			return IsAtLeast(capability);
		}
	}

	public static class NativeApis
	{
		public const string RespondsToSetNeedsUpdateOfHomeIndicatorAutoHidden = "RespondsToSetNeedsUpdateOfHomeIndicatorAutoHidden";
		public const int UIActivityIndicatorViewStyleMedium = 13;
	}
}