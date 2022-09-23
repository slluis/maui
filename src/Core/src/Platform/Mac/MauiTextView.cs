using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using AppKit;

namespace Microsoft.Maui.Platform
{
	public class MauiTextView : NSTextView
	{
		readonly NSTextField _placeholderLabel;

		public MauiTextView()
		{
			_placeholderLabel = InitPlaceholderLabel();
			TextDidChange += OnChanged;
		}

		public MauiTextView(CGRect frame)
			: base(frame)
		{
			_placeholderLabel = InitPlaceholderLabel();
			TextDidChange += OnChanged;
		}

		public override bool ShouldChangeText(NSRange affectedCharRange, string replacementString)
		{
			if (ShouldChangeTextEvent != null)
				return ShouldChangeTextEvent.Invoke(this, affectedCharRange, replacementString);

			return base.ShouldChangeText(affectedCharRange, replacementString);
		}

		public event Func<NSTextView, NSRange, string, bool>? ShouldChangeTextEvent;

		// Native Changed doesn't fire when the Text Property is set in code
		// We use this event as a way to fire changes whenever the Text changes
		// via code or user interaction.
		public event EventHandler? TextSetOrChanged;

		public string PlaceholderText
		{
			get => _placeholderLabel.StringValue;
			set
			{
				_placeholderLabel.StringValue = value;
				_placeholderLabel.SizeToFit();
			}
		}

		public NSAttributedString AttributedPlaceholderText
		{
			get => _placeholderLabel.AttributedStringValue;
			set
			{
				_placeholderLabel.AttributedStringValue = value;
				_placeholderLabel.SizeToFit();
			}
		}

		public NSColor PlaceholderTextColor
		{
			get => _placeholderLabel.TextColor;
			set => _placeholderLabel.TextColor = value;
		}

		public TextAlignment VerticalTextAlignment { get; set; }

		public override string Value
		{
			get => base.Value;
			set
			{
				var old = base.Value;

				base.Value = value;

				if (old != value)
				{
					HidePlaceholderIfTextIsPresent(value);
					TextSetOrChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		/* TODO COCOA
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			ShouldCenterVertically();
		}
		*/

		NSTextField InitPlaceholderLabel()
		{
			var placeholderLabel = new NSTextField
			{
				BackgroundColor = NSColor.Clear,
				TextColor = ColorExtensions.PlaceholderColor,
				//Lines = 0
			};

			AddSubview(placeholderLabel);

			var edgeInsets = TextContainerInset;
			var lineFragmentPadding = TextContainer.LineFragmentPadding;

			var vConstraints = NSLayoutConstraint.FromVisualFormat(
				"V:|-" + edgeInsets.Height + "-[PlaceholderLabel]-" + edgeInsets.Height + "-|", 0, new NSDictionary(),
				NSDictionary.FromObjectsAndKeys(
					new NSObject[] { placeholderLabel }, new NSObject[] { new NSString("PlaceholderLabel") })
			);

			var hConstraints = NSLayoutConstraint.FromVisualFormat(
				"H:|-" + lineFragmentPadding + "-[PlaceholderLabel]-" + lineFragmentPadding + "-|",
				0, new NSDictionary(),
				NSDictionary.FromObjectsAndKeys(
					new NSObject[] { placeholderLabel }, new NSObject[] { new NSString("PlaceholderLabel") })
			);

			placeholderLabel.TranslatesAutoresizingMaskIntoConstraints = false;

			AddConstraints(hConstraints);
			AddConstraints(vConstraints);

			return placeholderLabel;
		}
		/*
		void ShouldCenterVertically()
		{
			var fittingSize = new CGSize(Bounds.Width, NFloat.MaxValue);
			var sizeThatFits = SizeThatFits(fittingSize);
			var availableSpace = (Bounds.Height - sizeThatFits.Height * ZoomScale);
			ContentOffset = VerticalTextAlignment switch
			{
				Maui.TextAlignment.Center => new CGPoint(0, -Math.Max(1, availableSpace / 2)),
				Maui.TextAlignment.End => new CGPoint(0, -Math.Max(1, availableSpace)),
				_ => new CGPoint(0, 0),
			};
		}
		*/

		void HidePlaceholderIfTextIsPresent(string? value)
		{
			_placeholderLabel.Hidden = !string.IsNullOrEmpty(value);
		}

		void OnChanged(object? sender, EventArgs e)
		{
			HidePlaceholderIfTextIsPresent(Value);
			TextSetOrChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}