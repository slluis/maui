﻿using System;
using System.Collections.Concurrent;
using System.IO;
using Android.Graphics;
using Android.Util;
using Microsoft.Extensions.Logging;
using AApplication = Android.App.Application;

namespace Microsoft.Maui
{
	public class FontManager : IFontManager
	{
		static readonly string[] FontFolders = new[]
		{
			"Fonts/",
			"fonts/",
		};

		readonly ConcurrentDictionary<(string? fontFamilyName, FontWeight weight, bool italic), Typeface?> _typefaces = new();
		readonly IFontRegistrar _fontRegistrar;
		readonly IServiceProvider? _serviceProvider;

		Typeface? _defaultTypeface;

		public FontManager(IFontRegistrar fontRegistrar, IServiceProvider? serviceProvider = null)
		{
			_fontRegistrar = fontRegistrar;
			_serviceProvider = serviceProvider;
		}

		public double DefaultFontSize => 14; // 14sp

		public Typeface DefaultTypeface => _defaultTypeface ??= Typeface.Default!;

		public Typeface? GetTypeface(Font font)
		{
			if (font == Font.Default || (font.Weight == FontWeight.Regular && string.IsNullOrEmpty(font.Family) && font.Slant == FontSlant.Default))
				return DefaultTypeface;

			return _typefaces.GetOrAdd((font.Family, font.Weight, font.Slant != FontSlant.Default), CreateTypeface);
		}

		public FontSize GetFontSize(Font font, float defaultFontSize = 0)
		{
			var size = font.Size <= 0 || double.IsNaN(font.Size)
				? (defaultFontSize > 0 ? defaultFontSize : (float)DefaultFontSize)
				: (float)font.Size;

			ComplexUnitType units;

			if (font.AutoScalingEnabled)
				units = ComplexUnitType.Sp;
			else
				units = ComplexUnitType.Dip;

			return new FontSize(size, units);
		}


		Typeface? GetFromAssets(string fontName)
		{
			fontName = _fontRegistrar.GetFont(fontName) ?? fontName;

			// First check Alias
			var asset = LoadTypefaceFromAsset(fontName, warning: true);
			if (asset != null)
				return asset;

			// The font might be a file, such as a temporary file extracted from EmbeddedResource
			if (File.Exists(fontName))
				return Typeface.CreateFromFile(fontName);

			var fontFile = FontFile.FromString(fontName);
			if (!string.IsNullOrWhiteSpace(fontFile.Extension))
			{
				return FindFont(fontFile.FileNameWithExtension());
			}
			else
			{
				foreach (var ext in FontFile.Extensions)
				{
					var font = FindFont(fontFile.FileNameWithExtension(ext));
					if (font != null)
						return font;
				}
			}

			return null;
		}

		Typeface? FindFont(string fileWithExtension)
		{
			var result = LoadTypefaceFromAsset(fileWithExtension, warning: false);
			if (result != null)
				return result;

			foreach (var folder in FontFolders)
			{
				result = LoadTypefaceFromAsset(folder + fileWithExtension, warning: false);
				if (result != null)
					return result;
			}

			return null;
		}

		Typeface? LoadTypefaceFromAsset(string fontfamily, bool warning)
		{
			try
			{
				return Typeface.CreateFromAsset(AApplication.Context.Assets, FontNameToFontFile(fontfamily));
			}
			catch (Exception ex)
			{
				if (warning)
					_serviceProvider?.CreateLogger<FontManager>()?.LogWarning(ex, "Unable to load font '{Font}' from assets.", fontfamily);
			}

			return null;
		}

		Typeface? CreateTypeface((string? fontFamilyName, FontWeight weight, bool italic) fontData)
		{
			var (fontFamily, weight, italic) = fontData;
			fontFamily ??= string.Empty;
			var style = ToTypefaceStyle(weight, italic);

			var result = Typeface.Default;

			if (!string.IsNullOrWhiteSpace(fontFamily))
			{
				if (GetFromAssets(fontFamily) is Typeface typeface)
					result = typeface;
				else
					result = Typeface.Create(fontFamily, style);
			}

			if (OperatingSystem.IsAndroidVersionAtLeast(28))
				result = Typeface.Create(result, (int)weight, italic);
			else
				result = Typeface.Create(result, style);

			return result;
		}

		TypefaceStyle ToTypefaceStyle(FontWeight weight, bool italic)
		{
			var style = TypefaceStyle.Normal;
			var bold = weight >= FontWeight.Bold;
			if (bold && italic)
				style = TypefaceStyle.BoldItalic;
			else if (bold)
				style = TypefaceStyle.Bold;
			else if (italic)
				style = TypefaceStyle.Italic;
			return style;
		}

		string FontNameToFontFile(string fontFamily)
		{
			fontFamily ??= string.Empty;

			int hashtagIndex = fontFamily.IndexOf("#", StringComparison.Ordinal);
			if (hashtagIndex >= 0)
				return fontFamily.Substring(0, hashtagIndex);

			return fontFamily;
		}
	}
}