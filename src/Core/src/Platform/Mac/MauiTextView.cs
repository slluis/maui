﻿using System;
using CoreGraphics;
using Foundation;
using AppKit;

namespace Microsoft.Maui.Platform.Mac
{
	public class MauiTextView : NSTextView
	{
		NSTextView PlaceholderLabel { get; } = new NSTextView
		{
			BackgroundColor = UIColor.Clear,
			Lines = 0
		};

		public MauiTextView(CGRect frame) : base(frame)
		{
			InitPlaceholderLabel();
		}

		public string? PlaceholderText
		{
			get => PlaceholderLabel.Text;
			set
			{
				PlaceholderLabel.Text = value;
				PlaceholderLabel.SizeToFit();
			}
		}

		public NSColor? PlaceholderTextColor
		{
			get => PlaceholderLabel.TextColor;
			set => PlaceholderLabel.TextColor = value;
		}

		public void HidePlaceholder(bool hide)
		{
			PlaceholderLabel.Hidden = hide;
		}

		void InitPlaceholderLabel()
		{
			AddSubview(PlaceholderLabel);

			var edgeInsets = TextContainerInset;
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

		// TODO COCOA
/*		public override string? Text
		{
			get => base.Text;
			set
			{
				var old = base.Text;

				base.Text = value;

				if (old != value)
					TextPropertySet?.Invoke(this, EventArgs.Empty);
			}
		}

		public override NSAttributedString AttributedText
		{
			get => base.AttributedText;
			set
			{
				var old = base.AttributedText;

				base.AttributedText = value;

				if (old?.Value != value?.Value)
					TextPropertySet?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler? TextPropertySet;*/
	}
}
