﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using AppKit;

namespace Microsoft.Maui.Controls.Platform
{
	internal partial class ModalNavigationManager
	{
		NSViewController? _renderer
		{
			get
			{
				if (_window?.Page?.Handler?.NativeView is NSView view)
				{
					return view.Window.ContentViewController;
				}

				return null;
			}
		}

		// do I really need this anymore?
		static void HandleChildRemoved(object? sender, ElementEventArgs e)
		{
			var view = e.Element;
			//view?.DisposeModalAndChildRenderers();
		}

		public Task<Page> PopModalAsync(bool animated)
		{
			var modal = _navModel.PopModal();
			//modal.DescendantRemoved -= HandleChildRemoved;

			//var controller = (modal.Handler as INativeViewHandler)?.ViewController;

			//if (ModalStack.Count >= 1 && controller != null)
			//	await controller.DismissViewControllerAsync(animated);
			//else if (_renderer != null)
			//	await _renderer.DismissViewControllerAsync(animated);

			// Yes?
			//modal.DisposeModalAndChildRenderers();

			return Task.FromResult(modal);
		}

		public Task PushModalAsync(Page modal, bool animated)
		{
			EndEditing();

			//var elementConfiguration = modal as IElementConfiguration<Page>;

			//var presentationStyle =
			//	elementConfiguration?
			//		.On<PlatformConfiguration.iOS>()?
			//		.ModalPresentationStyle()
			//		.ToNativeModalPresentationStyle();

			//_window.Page?.GetCurrentPage()?.SendDisappearing();

			//_navModel.PushModal(modal);

			//modal.DescendantRemoved += HandleChildRemoved;

			//if (_window?.Page?.Handler != null)
			//	return PresentModal(modal, animated && animated);

			return Task.CompletedTask;
		}

		//async Task PresentModal(Page modal, bool animated)
		//{
			//modal.ToNative(MauiContext);
			//var wrapper = new ModalWrapper(modal.Handler as INativeViewHandler);

			//if (ModalStack.Count > 1)
			//{
			//	var topPage = ModalStack[ModalStack.Count - 2];
			//	var controller = (topPage?.Handler as INativeViewHandler)?.ViewController;
			//	if (controller != null)
			//	{
			//		await controller.PresentViewControllerAsync(wrapper, animated);
			//		await Task.Delay(5);
			//		return;
			//	}
			//}

			//// One might wonder why these delays are here... well thats a great question. It turns out iOS will claim the 
			//// presentation is complete before it really is. It does not however inform you when it is really done (and thus 
			//// would be safe to dismiss the VC). Fortunately this is almost never an issue

			//if (_renderer != null)
			//{
			//	await _renderer.PresentViewControllerAsync(wrapper, animated);
			//	await Task.Delay(5);
			//}
		//}

		void EndEditing()
		{
			// If any text entry controls have focus, we need to end their editing session
			// so that they are not the first responder; if we don't some things (like the activity indicator
			// on pull-to-refresh) will not work correctly. 

			// The topmost modal on the stack will have the Window; we can use that to end any current
			// editing that's going on 
			//if (ModalStack.Count > 0)
			//{
			//	var uiViewController = (ModalStack[ModalStack.Count - 1].Handler as INativeViewHandler)?.ViewController;
			//	uiViewController?.View?.Window?.EndEditing(true);
			//	return;
			//}


			//// TODO MAUI
			//// If there aren't any modals, then the platform renderer will have the Window
			//_window?.NativeWindow?.EndEditing(true);
		}
	}
}
