using System.ComponentModel;

namespace Microsoft.Maui.Controls.Internals
{
	/// <include file="../../../docs/Microsoft.Maui.Controls.Internals/TextTransformUtilites.xml" path="Type[@FullName='Microsoft.Maui.Controls.Internals.TextTransformUtilites']/Docs" />
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TextTransformUtilites
	{
		/// <include file="../../../docs/Microsoft.Maui.Controls.Internals/TextTransformUtilites.xml" path="//Member[@MemberName='GetTransformedText']/Docs" />
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string GetTransformedText(string source, TextTransform textTransform)
		{
			if (string.IsNullOrEmpty(source))
				return string.Empty;

			switch (textTransform)
			{
				case TextTransform.None:
				default:
					return source;
				case TextTransform.Lowercase:
					return source.ToLowerInvariant();
				case TextTransform.Uppercase:
					return source.ToUpperInvariant();
			}
		}


		/// <include file="../../../docs/Microsoft.Maui.Controls.Internals/TextTransformUtilites.xml" path="//Member[@MemberName='SetPlainText']/Docs" />
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void SetPlainText(InputView inputView, string platformText)
		{
			if (inputView == null)
				return;

			var textTransform = inputView.TextTransform;
			var platformTextWithTransform = inputView.UpdateFormsText(platformText, textTransform);
			var formsText = inputView.UpdateFormsText(inputView.Text, textTransform);

			if ((string.IsNullOrEmpty(formsText) && platformText.Length == 0))
				return;

			if (formsText == platformTextWithTransform)
				return;

			inputView.SetValueFromRenderer(InputView.TextProperty, platformText);
		}
	}
}