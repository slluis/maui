using System;
using System.Collections.Generic;
using System.Text;
using AppKit;

namespace Microsoft.Maui.Controls
{
	public partial class Element
	{
		public static void MapAutomationPropertiesIsInAccessibleTree(IElementHandler handler, Element element)
		{
			// If the user hasn't set IsInAccessibleTree then just don't do anything
			if (!element.IsSet(AutomationProperties.IsInAccessibleTreeProperty))
				return;

			var Control = handler.PlatformView as NSView;

			// iOS sets the default value for IsAccessibilityElement late in the layout cycle
			// But if we set it to false ourselves then that causes it to act like it's false

			// from the docs:
			// https://developer.apple.com/documentation/objectivec/nsobject/1615141-isaccessibilityelement
			// The default value for this property is false unless the receiver is a standard UIKit control,
			// in which case the value is true.
			//
			// So we just base the default on that logic
			
			var _defaultIsAccessibilityElement = Control.AccessibilityElement || Control is NSControl;

			Control.AccessibilityElement = (bool)((bool?)element.GetValue(AutomationProperties.IsInAccessibleTreeProperty) ?? _defaultIsAccessibilityElement);
		}

		public static void MapAutomationPropertiesExcludedWithChildren(IElementHandler handler, Element view)
		{
			if (!view.IsSet(AutomationProperties.ExcludedWithChildrenProperty))
				return;

			var Control = handler.PlatformView as NSView;

			var _defaultAccessibilityElementsHidden = Control.AccessibilityHidden || Control is NSControl;
			Control.AccessibilityHidden = (bool)((bool?)view.GetValue(AutomationProperties.ExcludedWithChildrenProperty) ?? _defaultAccessibilityElementsHidden);
		}
	}
}
