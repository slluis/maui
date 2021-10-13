using AppKit;

namespace Microsoft.Maui
{
	public static class StepperExtensions
	{
		public static void UpdateMinimum(this NSStepper nativeStepper, IStepper stepper)
		{
			nativeStepper.MinimumValue = stepper.Minimum;
		}

		public static void UpdateMaximum(this NSStepper nativeStepper, IStepper stepper)
		{
			nativeStepper.MaximumValue = stepper.Maximum;
		}

		public static void UpdateIncrement(this NSStepper nativeStepper, IStepper stepper)
		{
			var increment = stepper.Interval;

			if (increment > 0)
				nativeStepper.StepValue = stepper.Interval;
		}

		public static void UpdateValue(this NSStepper nativeStepper, IStepper stepper)
		{
			if (nativeStepper.Value != stepper.Value)
				nativeStepper.Value = stepper.Value;
		}
	}
}