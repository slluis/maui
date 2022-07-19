﻿using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls
{
	internal class ImmutableBrush : SolidColorBrush
	{
		static readonly BindablePropertyKey ColorPropertyKey =
			BindableProperty.CreateReadOnly(nameof(Color), typeof(Color), typeof(ImmutableBrush), null);

		public new static readonly BindableProperty ColorProperty = ColorPropertyKey.BindableProperty;

		public ImmutableBrush(Color color)
		{
			SetValue(ColorPropertyKey, color);
		}

		public override Color Color
		{
			get => (Color)GetValue(ColorProperty);
			set { }
		}
	}
}
