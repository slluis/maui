#nullable enable
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;
using AppKit;
using System;

namespace Microsoft.Maui
{
	public class MauiPicker : NoCaretField
	{
		public string Text {
			get;
			set;
		}

		readonly HashSet<string> _enableActions;

		public MauiPicker(/*UIPickerView? uIPickerView*/)
		{
//			UIPickerView = uIPickerView;

			string[] actions = { "copy:", "select:", "selectAll:" };
			_enableActions = new HashSet<string>(actions);
			Text = "";
		}

		internal void ReloadAllComponents()
		{
			throw new NotImplementedException();
		}

		//public UIPickerView? UIPickerView { get; set; }

		//		public override bool CanPerform(Selector action, NSObject? withSender)
		//			=> _enableActions.Contains(action.Name);
	}
}
