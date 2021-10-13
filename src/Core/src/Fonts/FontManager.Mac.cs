﻿using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using AppKit;

namespace Microsoft.Maui
{
	public class FontManager : IFontManager
	{
		// NSFontWeight[Constant] is internal in Xamarin.iOS but the convertion from
		// the public (int-based) enum is not helpful in this case.
		// -1.0 (Thin / 100) to 1.0 (Black / 900) with 0 being Regular (400)
		// which is not quite the center, not are the constant values linear
		static readonly (float value, FontWeight weight)[] FontWeightMap = new (float, FontWeight)[] {
			(-0.80f, FontWeight.Ultralight),
			(-0.60f, FontWeight.Thin),
			(-0.40f, FontWeight.Light),
			(0.0f, FontWeight.Regular),
			(0.23f, FontWeight.Medium),
			(0.30f, FontWeight.Semibold),
			(0.40f, FontWeight.Bold),
			(0.56f, FontWeight.Heavy),
			(0.62f, FontWeight.Black)
		};

		readonly ConcurrentDictionary<Font, NSFont> _fonts = new();
		readonly IFontRegistrar _fontRegistrar;
		readonly ILogger<FontManager>? _logger;

		NSFont? _defaultFont;

		public FontManager(IFontRegistrar fontRegistrar, ILogger<FontManager>? logger = null)
		{
			_fontRegistrar = fontRegistrar;
			_logger = logger;
		}

		public NSFont DefaultFont =>
			_defaultFont ??= NSFont.SystemFontOfSize(NSFont.SystemFontSize);

		public NSFont GetFont(Font font, double defaultFontSize = 0) =>
			GetFont(font, defaultFontSize, CreateFont);

		double GetFontSize(Font font, double defaultFontSize = 0) =>
			font.Size <= 0
				? (defaultFontSize > 0 ? (float)defaultFontSize : DefaultFont.PointSize)
				: (nfloat)font.Size;

		static float GetWeightConstant(FontWeight self)
		{
			foreach (var (value, weight) in FontWeightMap)
			{
				if (self <= weight)
					return value;
			}
			return 1.0f;
		}

		NSFont GetFont(Font font, double defaultFont, Func<Font, NSFont> factory)
		{
			var size = GetFontSize(font, defaultFont);
			if (size != font.Size)
				font = font.WithSize(size);
			return _fonts.GetOrAdd(font, factory);
		}

/*		static NSFontAttributes GetFontAttributes(Font font)
		{
			var a = new NSFontAttributes
			{
				Traits = new NSFontTraits(),
			};
			var weight = font.Weight;
			if (font.Weight == 0)
				weight = FontWeight.Regular;
			var traits = (NSFontDescriptorSymbolicTraits)0;
			if (weight == FontWeight.Bold)
				traits |= NSFontDescriptorSymbolicTraits.Bold;
			else if (weight != FontWeight.Regular)
			{
				a.Traits = new NSFontTraits
				{
					Weight = GetWeightConstant(font.Weight),
					Slant = font.Slant == FontSlant.Oblique ? 30.0f : 0.0f
				};
			}
			if (font.Slant == FontSlant.Italic)
				traits |= NSFontDescriptorSymbolicTraits.Italic;

			a.Traits.SymbolicTrait = traits;
			return a;
		}*/

		NSFont CreateFont(Font font)
		{
			var family = font.Family;
			var size = (nfloat)font.Size;

			var hasAttributes =
				font.Weight != FontWeight.Regular ||
				font.Slant != FontSlant.Default;

			if (family != null && family != DefaultFont.FamilyName)
			{
				try
				{
					NSFont? result = null;

					if (Array.IndexOf(NSFont.FamilyNames, family) != -1)
					{
						var descriptor = new NSFontDescriptor().CreateWithFamily(family);
						if (hasAttributes)
							throw new NotImplementedException();
							//descriptor = descriptor.CreateWithAttributes(GetFontAttributes(font));

						result = NSFont.FromDescriptor(descriptor, size);
						if (result != null)
							return ApplyScaling(font, result);
					}

					var cleansedFont = CleanseFontName(family);
					result = NSFont.FromName(cleansedFont, size);
					if (result != null)
						return ApplyScaling(font, result);

					if (family.StartsWith(".SFUI", StringComparison.InvariantCultureIgnoreCase))
					{
						var weights = family.Split('-');
						var fontWeight = weights.Length == 0
							? null
							: weights[weights.Length - 1];

						if (!string.IsNullOrWhiteSpace(fontWeight) && Enum.TryParse<NSFontWeight>(fontWeight, true, out var NSFontWeight))
						{
							result = NSFont.SystemFontOfSize(size, NSFontWeight);
							if (result != null)
								return ApplyScaling(font, result);
						}

						result = NSFont.SystemFontOfSize(size, NSFontWeight.Regular);
						if (result != null)
							return ApplyScaling(font, result);
					}

					result = NSFont.FromName(family, size);
					if (result != null)
						return ApplyScaling(font, result);
				}
				catch (Exception ex)
				{
					_logger?.LogWarning(ex, "Unable to load font '{Font}'.", family);
				}
			}

			if (hasAttributes)
			{
				var defaultFont = NSFont.SystemFontOfSize(size);
				var descriptor = defaultFont.FontDescriptor.CreateWithAttributes(GetFontAttributes(font));
				return ApplyScaling(font, NSFont.FromDescriptor(descriptor, size));
			}

			return ApplyScaling(font, NSFont.SystemFontOfSize(size));

			NSFont ApplyScaling(Font font, NSFont NSFont)
			{
				if (font.AutoScalingEnabled)
					return NSFontMetrics.DefaultMetrics.GetScaledFont(NSFont);

				return NSFont;
			}
		}

		string? CleanseFontName(string fontName)
		{
			// First check Alias
			if (_fontRegistrar.GetFont(fontName) is string fontPostScriptName)
				return fontPostScriptName;

			var fontFile = FontFile.FromString(fontName);

			if (!string.IsNullOrWhiteSpace(fontFile.Extension))
			{
				if (_fontRegistrar.GetFont(fontFile.FileNameWithExtension()) is string filePath)
					return filePath ?? fontFile.PostScriptName;
			}
			else
			{
				foreach (var ext in FontFile.Extensions)
				{
					var formatted = fontFile.FileNameWithExtension(ext);
					if (_fontRegistrar.GetFont(formatted) is string filePath)
						return filePath;
				}
			}

			return fontFile.PostScriptName;
		}
	}
}