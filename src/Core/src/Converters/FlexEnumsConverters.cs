using System;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Maui.Layouts;

#nullable disable
namespace Microsoft.Maui.Converters
{
	/// <inheritdoc/>
	public class FlexJustifyTypeConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			=> sourceType == typeof(string);

		/// <inheritdoc/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			=> true;

		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var strValue = value?.ToString();

			if (strValue != null)
			{
				if (Enum.TryParse(strValue, true, out FlexJustify justify))
					return justify;
				if (strValue.Equals("flex-start", StringComparison.OrdinalIgnoreCase))
					return FlexJustify.Start;
				if (strValue.Equals("flex-end", StringComparison.OrdinalIgnoreCase))
					return FlexJustify.End;
				if (strValue.Equals("space-between", StringComparison.OrdinalIgnoreCase))
					return FlexJustify.SpaceBetween;
				if (strValue.Equals("space-around", StringComparison.OrdinalIgnoreCase))
					return FlexJustify.SpaceAround;
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", strValue, typeof(FlexJustify)));
		}

		/// <inheritdoc/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is not FlexJustify fj)
				throw new NotSupportedException();

			return fj.ToString();
		}
	}

	/// <inheritdoc/>
	public class FlexDirectionTypeConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			=> sourceType == typeof(string);

		/// <inheritdoc/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			=> true;

		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var strValue = value?.ToString();

			if (strValue != null)
			{
				if (Enum.TryParse(strValue, true, out FlexDirection aligncontent))
					return aligncontent;
				if (strValue.Equals("row-reverse", StringComparison.OrdinalIgnoreCase))
					return FlexDirection.RowReverse;
				if (strValue.Equals("column-reverse", StringComparison.OrdinalIgnoreCase))
					return FlexDirection.ColumnReverse;
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", strValue, typeof(FlexDirection)));
		}

		/// <inheritdoc/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is not FlexDirection fd)
				throw new NotSupportedException();

			return fd.ToString();
		}
	}

	/// <inheritdoc/>
	public class FlexAlignContentTypeConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			=> sourceType == typeof(string);

		/// <inheritdoc/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			=> true;

		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var strValue = value?.ToString();

			if (strValue != null)
			{
				if (Enum.TryParse(strValue, true, out FlexAlignContent aligncontent))
					return aligncontent;
				if (strValue.Equals("flex-start", StringComparison.OrdinalIgnoreCase))
					return FlexAlignContent.Start;
				if (strValue.Equals("flex-end", StringComparison.OrdinalIgnoreCase))
					return FlexAlignContent.End;
				if (strValue.Equals("space-between", StringComparison.OrdinalIgnoreCase))
					return FlexAlignContent.SpaceBetween;
				if (strValue.Equals("space-around", StringComparison.OrdinalIgnoreCase))
					return FlexAlignContent.SpaceAround;
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", strValue, typeof(FlexAlignContent)));
		}

		/// <inheritdoc/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is not FlexAlignContent fac)
				throw new NotSupportedException();

			return fac.ToString();
		}
	}

	/// <inheritdoc/>
	public class FlexAlignItemsTypeConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			=> sourceType == typeof(string);

		/// <inheritdoc/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			=> true;

		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var strValue = value?.ToString();

			if (strValue != null)
			{
				if (Enum.TryParse(strValue, true, out FlexAlignItems alignitems))
					return alignitems;
				if (strValue.Equals("flex-start", StringComparison.OrdinalIgnoreCase))
					return FlexAlignItems.Start;
				if (strValue.Equals("flex-end", StringComparison.OrdinalIgnoreCase))
					return FlexAlignItems.End;
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", strValue, typeof(FlexAlignItems)));
		}

		/// <inheritdoc/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is not FlexAlignItems fai)
				throw new NotSupportedException();

			return fai.ToString();
		}
	}

	/// <inheritdoc/>
	public class FlexAlignSelfTypeConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			=> sourceType == typeof(string);

		/// <inheritdoc/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			=> true;

		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var strValue = value?.ToString();

			if (strValue != null)
			{
				if (Enum.TryParse(strValue, true, out FlexAlignSelf alignself))
					return alignself;
				if (strValue.Equals("flex-start", StringComparison.OrdinalIgnoreCase))
					return FlexAlignSelf.Start;
				if (strValue.Equals("flex-end", StringComparison.OrdinalIgnoreCase))
					return FlexAlignSelf.End;
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", strValue, typeof(FlexAlignSelf)));
		}

		/// <inheritdoc/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is not FlexAlignSelf fes)
				throw new NotSupportedException();

			return fes.ToString();
		}
	}

	/// <inheritdoc/>
	public class FlexWrapTypeConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			=> sourceType == typeof(string);

		/// <inheritdoc/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			=> true;

		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var strValue = value?.ToString();

			if (strValue != null)
			{
				if (Enum.TryParse(strValue, true, out FlexWrap wrap))
					return wrap;
				if (strValue.Equals("wrap-reverse", StringComparison.OrdinalIgnoreCase))
					return FlexWrap.Reverse;
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", strValue, typeof(FlexWrap)));
		}

		/// <inheritdoc/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is not FlexWrap fw)
				throw new NotSupportedException();

			return fw.ToString();
		}
	}

	/// <inheritdoc/>
	public class FlexBasisTypeConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			=> sourceType == typeof(string);

		/// <inheritdoc/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			=> true;

		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var strValue = value?.ToString();

			if (strValue != null)
			{
				strValue = strValue.Trim();

				if (strValue.Equals("auto", StringComparison.OrdinalIgnoreCase))
					return FlexBasis.Auto;
				if (strValue.EndsWith("%", StringComparison.OrdinalIgnoreCase) && float.TryParse(strValue.Substring(0, strValue.Length - 1), NumberStyles.Number, CultureInfo.InvariantCulture, out float relflex))
					return new FlexBasis(relflex / 100, isRelative: true);
				if (float.TryParse(strValue, NumberStyles.Number, CultureInfo.InvariantCulture, out float flex))
					return new FlexBasis(flex);
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", strValue, typeof(FlexBasis)));
		}

		/// <inheritdoc/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is not FlexBasis basis)
				throw new NotSupportedException();

			if (basis.IsAuto)
				return "auto";
			if (basis.IsRelative)
				return $"{(basis.Length * 100).ToString(CultureInfo.InvariantCulture)}%";

			return $"{basis.Length.ToString(CultureInfo.InvariantCulture)}";
		}
	}
}