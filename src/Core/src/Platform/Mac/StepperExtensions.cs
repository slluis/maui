using AppKit;

namespace Microsoft.Maui.Platform
{
	public static class StepperExtensions
	{
		public static void UpdateMinimum(this NSStepper nativeStepper, IStepper stepper)
		{
			nativeStepper.MinValue = stepper.Minimum;
		}

		public static void UpdateMaximum(this NSStepper nativeStepper, IStepper stepper)
		{
			nativeStepper.MaxValue = stepper.Maximum;
		}

		public static void UpdateIncrement(this NSStepper nativeStepper, IStepper stepper)
		{
			var increment = stepper.Interval;

			if (increment > 0)
				nativeStepper.Increment = stepper.Interval;
		}

		public static void UpdateValue(this NSStepper nativeStepper, IStepper stepper)
		{
			if (nativeStepper.DoubleValue != stepper.Value)
				nativeStepper.DoubleValue = stepper.Value;
		}
	}
}