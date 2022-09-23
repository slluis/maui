using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class ProgressBarHandler : ViewHandler<IProgress, NSProgressIndicator>
	{
		protected override NSProgressIndicator CreatePlatformView()
		{
			return new NSProgressIndicator();
		}

		public static void MapProgress(ProgressBarHandler handler, IProgress progress)
		{
			//handler.NativeView?.UpdateProgress(progress);
		}

		public static void MapProgressColor(ProgressBarHandler handler, IProgress progress)
		{
			//handler.NativeView?.UpdateProgressColor(progress);
		}
	}
}