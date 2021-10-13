using AppKit;

namespace Microsoft.Maui
{
	public static class ProgressBarExtensions
	{
		public static void UpdateProgress(this NSProgressIndicator nativeProgressBar, IProgress progress)
		{
			nativeProgressBar.Progress = (float)progress.Progress;
		}

		public static void UpdateProgressColor(this NSProgressIndicator nativeProgressBar, IProgress progress)
		{
			nativeProgressBar.ProgressTintColor = progress.ProgressColor?.ToNative();
		}
	}
}