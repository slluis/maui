﻿using System.Collections.Generic;

namespace Microsoft.Maui.Controls
{
	public partial class Picker : IPicker
	{
		Font ITextStyle.Font => this.ToFont();

		IList<string> IPicker.Items => Items;

		int IItemDelegate<string>.GetCount() => Items?.Count ?? ItemsSource?.Count ?? 0;

		string IItemDelegate<string>.GetItem(int index)
		{
			if (index < 0)
				return "";
			if (index < Items?.Count)
				return Items[index];
			if (index < ItemsSource?.Count)
				return GetDisplayMember(ItemsSource[index]);
			return "";
		}
	}
}