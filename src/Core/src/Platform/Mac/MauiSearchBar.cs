using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using AppKit;

namespace Microsoft.Maui.Platform
{
	public class MauiSearchBar : NSView
	{
		public bool ShowsCancelButton { get; set; }

		internal NSSearchField? SearchTextField;

		public string Text
		{
			get
			{
				return SearchTextField?.StringValue ?? string.Empty;
			}

			set
			{
				if (SearchTextField != null)
					SearchTextField.StringValue = value;
			}
		}

		public MauiSearchBar() : this(RectangleF.Empty)
		{

		}

		public MauiSearchBar(NSCoder coder) : base(coder)
		{
		}

		public MauiSearchBar(CGRect frame) : base(frame)
		{
		}

		protected MauiSearchBar(NSObjectFlag t) : base(t)
		{
		}

		internal void UpdateMaxLength(ISearchBar bar)
		{
			throw new NotImplementedException();
		}

		protected internal MauiSearchBar(IntPtr handle) : base(handle)
		{
		}

/*		public override string? Text
		{
		r	get => base.Text;
			set
			{
				var old = base.Text;

				base.Text = value;

				if (old != value)
					TextPropertySet?.Invoke(this, EventArgs.Empty);
			}
		}*/

		public event EventHandler? TextPropertySet
		{
			add { }
			remove { }
		}

		internal void UpdateCancelButton(ISearchBar searchBar, NSColor? cancelButtonTextColorDefaultNormal, NSColor? cancelButtonTextColorDefaultHighlighted, NSColor? cancelButtonTextColorDefaultDisabled)
		{
			throw new NotImplementedException();
		}
	}
}