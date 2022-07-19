﻿using System;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.ControlGallery.WinUI;
using Microsoft.Maui.Controls.Compatibility.Platform.UWP;
using Microsoft.Maui.Controls.Platform;
using static Microsoft.Maui.Controls.Compatibility.ControlGallery.Issues.Issue9087;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(_9087Label), typeof(_9087CustomRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace Microsoft.Maui.Controls.Compatibility.ControlGallery.WinUI
{
	[System.Obsolete]
	public class _9087CustomRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null && e.NewElement.Text != "Success")
			{
				throw new Exception("Test Failed");
			}
		}
	}
}
