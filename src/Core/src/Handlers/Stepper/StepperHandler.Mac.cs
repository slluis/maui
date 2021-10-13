using System;
using System.Drawing;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class StepperHandler : ViewHandler<IStepper, NSStepper>
	{
		protected override NSStepper CreateNativeView()
		{
			return new NSStepper(RectangleF.Empty);
		}

		protected override void ConnectHandler(NSStepper nativeView)
		{
			base.ConnectHandler(nativeView);

			nativeView.Activated += OnValueChanged;
		}

		protected override void DisconnectHandler(NSStepper nativeView)
		{
			base.DisconnectHandler(nativeView);

			nativeView.Activated -= OnValueChanged;
		}

		public static void MapMinimum(StepperHandler handler, IStepper stepper)
		{
			handler.NativeView?.UpdateMinimum(stepper);
		}

		public static void MapMaximum(StepperHandler handler, IStepper stepper)
		{
			handler.NativeView?.UpdateMaximum(stepper);
		}

		public static void MapIncrement(StepperHandler handler, IStepper stepper)
		{
			handler.NativeView?.UpdateIncrement(stepper);
		}

		public static void MapValue(StepperHandler handler, IStepper stepper)
		{
			handler.NativeView?.UpdateValue(stepper);
		}

		void OnValueChanged(object? sender, EventArgs e)
		{
			if (NativeView == null || VirtualView == null)
				return;

			VirtualView.Value = NativeView.DoubleValue;
		}
	}
}