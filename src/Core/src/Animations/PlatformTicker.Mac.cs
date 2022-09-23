using CoreAnimation;
using Foundation;
using CoreVideo;

namespace Microsoft.Maui.Animations
{
	public class PlatformTicker : Ticker
	{
		CVDisplayLink? _link;

		public override bool IsRunning =>
			_link != null;

		public override void Start()
		{
			if (_link != null)
				return;

			_link = new CVDisplayLink();
			_link.SetOutputCallback(Callback);
			_link.Start();
		}

		CVReturn Callback(CVDisplayLink displayLink, ref CVTimeStamp inNow, ref CVTimeStamp inOutputTime, CVOptionFlags flagsIn, ref CVOptionFlags flagsOut)
		{
			Fire?.Invoke();
			return CVReturn.Success;
		}

		public override void Stop()
		{
			if (_link == null)
				return;

			_link.Stop();
			_link.Dispose();
			_link = null;
		}
	}
}