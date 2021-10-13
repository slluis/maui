using System;
using CoreGraphics;
using Foundation;
using AppKit;

namespace Microsoft.Maui.Platform.Mac
{
	public class MauiTextView : NSTextView
	{
		NSTextView PlaceholderLabel { get; } = new NSTextView
		{
			BackgroundColor = NSColor.Clear,
			// TODO COCOA
//			Lines = 0
		};

		public MauiTextView(CGRect frame) : base(frame)
		{
			InitPlaceholderLabel();
		}

		public string? PlaceholderText
		{
			get => PlaceholderLabel.Value;
			set
			{
				PlaceholderLabel.Value = value ?? "";
				PlaceholderLabel.SizeToFit();
			}
		}

		public NSColor? PlaceholderTextColor
		{
			get => PlaceholderLabel.TextColor;
			set => PlaceholderLabel.TextColor = value ?? NSColor.Black;
		}

		public void HidePlaceholder(bool hide)
		{
			PlaceholderLabel.Hidden = hide;
		}

		void InitPlaceholderLabel()
		{
			AddSubview(PlaceholderLabel);

			var edgeInsets = new CGRect(TextContainerOrigin, TextContainerInset);
			var lineFragmentPadding = TextContainer.LineFragmentPadding;

			var vConstraints = NSLayoutConstraint.FromVisualFormat(
				"V:|-" + edgeInsets.Top + "-[PlaceholderLabel]-" + edgeInsets.Bottom + "-|", 0, new NSDictionary(),
				NSDictionary.FromObjectsAndKeys(
					new NSObject[] { PlaceholderLabel }, new NSObject[] { new NSString("PlaceholderLabel") })
			);

			var hConstraints = NSLayoutConstraint.FromVisualFormat(
				"H:|-" + lineFragmentPadding + "-[PlaceholderLabel]-" + lineFragmentPadding + "-|",
				0, new NSDictionary(),
				NSDictionary.FromObjectsAndKeys(
					new NSObject[] { PlaceholderLabel }, new NSObject[] { new NSString("PlaceholderLabel") })
			);

			PlaceholderLabel.TranslatesAutoresizingMaskIntoConstraints = false;

			AddConstraints(hConstraints);
			AddConstraints(vConstraints);
		}

		public override string Value
		{
			get => base.Value;
			set
			{
				var old = base.Value;

				base.Value = value;

				if (old != value)
					TextPropertySet?.Invoke(this, EventArgs.Empty);
			}
		}

		// TODO COCOA
/*		public override NSAttributedString AttributedString
		{
			get => base.AttributedString;
			set
			{
				var old = base.AttributedString;

				base.AttributedString = value;

				if (old?.Value != value?.Value)
					TextPropertySet?.Invoke(this, EventArgs.Empty);
			}
		}*/

		public event EventHandler? TextPropertySet;
	}
}
