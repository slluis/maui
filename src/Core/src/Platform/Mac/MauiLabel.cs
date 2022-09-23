﻿using CoreAnimation;
using CoreGraphics;
using AppKit;

namespace Microsoft.Maui.Platform
{
	public class MauiLabel : NSTextField
	{
		public override bool IsFlipped => true;

		// TODO COCOA
		// public UIEdgeInsets TextInsets { get; set; }

		public MauiLabel(CGRect frame) : base(frame)
		{
			Initialize();
		}

		public MauiLabel()
		{
			Initialize();
		}

		void Initialize()
		{
			Editable = false;
			DrawsBackground = false;
			Bordered = false;
		}

/*		public override void DrawText(CGRect rect) =>
			base.DrawText(TextInsets.InsetRect(rect));*/

		public override void InvalidateIntrinsicContentSize()
		{
			base.InvalidateIntrinsicContentSize();

/*			if (Frame.Width == 0 && Frame.Height == 0)
			{
				// The Label hasn't actually been laid out on screen yet; no reason to request a layout
				return;
			}

			if (!Frame.Size.IsCloseTo(AddInsets(IntrinsicContentSize), (nfloat)0.001))
			{
				// The text or its attributes have changed enough that the size no longer matches the set Frame. It's possible
				// that the Label needs to be laid out again at a different size, so we request that the parent do so. 
				Superview?.SetNeedsLayout();
			}*/
		}

		//		public override CGSize SizeThatFits(CGSize size) => AddInsets(base.SizeThatFits(size));

		/*	CGSize AddInsets(CGSize size) => new CGSize(
				width: size.Width + TextInsets.Left + TextInsets.Right,
				height: size.Height + TextInsets.Top + TextInsets.Bottom);*/
	}
}