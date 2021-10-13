using AppKit;

namespace Microsoft.Maui
{
	public static class ProgressBarExtensions
	{
		public static void UpdateProgress(this NSProgressIndicator nativeProgressBar, IProgress progress)
		{
			nativeProgressBar.DoubleValue = (float)progress.Progress;
		}

		public static void UpdateProgressColor(this NSProgressIndicator nativeProgressBar, IProgress progress)
		{
			// TODO COCOA
//			nativeProgressBar.ProgressTintColor = progress.ProgressColor?.ToNative();
		}
	}
}