//#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Maui.Handlers;
using ObjCRuntime;
using AppKit;

namespace Microsoft.Maui.Platform
{
	internal class ControlsNavigationController : NSViewController
	{
		NavigationViewHandler viewHandler;
		public ControlsNavigationController(NavigationViewHandler viewHandler)
		{
			this.viewHandler = viewHandler;
		}
	}
}
