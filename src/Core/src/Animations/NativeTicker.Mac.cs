using CoreAnimation;
using Foundation;

namespace Microsoft.Maui.Animations
{
	public class NativeTicker : Ticker
	{
		bool isrunning;
		// TODO COCOA
		public override bool IsRunning => isrunning;

		public override void Start()
		{
			isrunning = true;
		}

		public override void Stop()
		{
			isrunning = false;
		}
	}
}