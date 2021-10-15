﻿using System;
using Foundation;
using AppKit;

namespace Microsoft.Maui
{
	public static class AttributedStringExtensions
	{
		public static NSMutableAttributedString? WithCharacterSpacing(this NSAttributedString attributedString, double characterSpacing)
		{
			if (attributedString == null || attributedString.Length == 0)
				return null;

			var attribute = attributedString.GetAttribute(NSStringAttributeKey.KerningAdjustment, 0, out _);

			// if we are going to un-set, but there is no adjustment, then bail out
			if (characterSpacing == 0 && attribute == null)
				return null;

			var mutableAttributedString = new NSMutableAttributedString(attributedString);
			mutableAttributedString.AddAttribute
			(
				NSStringAttributeKey.KerningAdjustment,
				NSObject.FromObject(characterSpacing),
				new NSRange(0, mutableAttributedString.Length)
			);
			return mutableAttributedString;
		}

		public static NSMutableAttributedString? WithColor(this NSAttributedString attributedString, NSColor color)
		{
			if (attributedString == null || attributedString.Length == 0)
				return null;

			var mutableAttributedString = new NSMutableAttributedString(attributedString);
			mutableAttributedString.AddAttribute
			(
				NSStringAttributeKey.ForegroundColor,
				color,
				new NSRange(0, mutableAttributedString.Length)
			);

			return mutableAttributedString;
		}

		public static NSColor? GetColor(this NSAttributedString attributedString)
		{
			if (attributedString == null || attributedString.Length == 0)
				return null;
			return (NSColor)attributedString.GetAttribute(NSStringAttributeKey.ForegroundColor, 0, out _);
		}

		public static NSMutableAttributedString? WithLineHeight(this NSAttributedString attributedString, double lineHeight)
		{
			if (attributedString == null || attributedString.Length == 0)
				return null;

			var attribute = (NSParagraphStyle)attributedString.GetAttribute(NSStringAttributeKey.ParagraphStyle, 0, out _);

			// if we need to un-set the line height but there is no attribute to modify then we do nothing
			if (lineHeight == -1 && attribute == null)
				return null;

			var mutableParagraphStyle = new NSMutableParagraphStyle();
			if (attribute != null)
				mutableParagraphStyle.SetParagraphStyle(attribute);

			mutableParagraphStyle.LineHeightMultiple = new nfloat(lineHeight >= 0 ? lineHeight : -1);

			var mutableAttributedString = new NSMutableAttributedString(attributedString);
			mutableAttributedString.AddAttribute
			(
				NSStringAttributeKey.ParagraphStyle,
				mutableParagraphStyle,
				new NSRange(0, mutableAttributedString.Length)
			);

			return mutableAttributedString;
		}

		public static NSMutableAttributedString? WithDecorations(this NSAttributedString attributedString, TextDecorations decorations)
		{
			if (attributedString == null || attributedString.Length == 0)
				return null;

			var mutable = new NSMutableAttributedString(attributedString);

			var range = new NSRange(0, mutable.Length);

			UpdateDecoration(mutable, NSStringAttributeKey.StrikethroughStyle, range,
				decorations & TextDecorations.Strikethrough);

			UpdateDecoration(mutable, NSStringAttributeKey.UnderlineStyle, range,
				decorations & TextDecorations.Underline);

			return mutable;
		}

		public static NSAttributedString? TrimToMaxLength(this NSAttributedString? attributedString, int maxLength) =>
			maxLength >= 0 && attributedString?.Length > maxLength
				? attributedString.Substring(0, maxLength)
				: attributedString;

		static void UpdateDecoration(NSMutableAttributedString attributedString, NSString key,
			NSRange range, TextDecorations decorations)
		{
			if (decorations == 0)
			{
				attributedString.RemoveAttribute(key, range);
			}
			else
			{
				attributedString.AddAttribute(key, NSNumber.FromInt32((int)NSUnderlineStyle.Single), range);
			}
		}
	}
}