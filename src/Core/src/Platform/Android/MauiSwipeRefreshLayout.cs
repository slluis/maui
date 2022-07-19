﻿using System;
using System.Collections.Generic;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Widget;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Microsoft.Maui.Graphics;
using AView = Android.Views.View;
using AWebView = Android.Webkit.WebView;

namespace Microsoft.Maui.Platform
{
	public class MauiSwipeRefreshLayout : SwipeRefreshLayout
	{
		AView? _contentView;

		public MauiSwipeRefreshLayout(Context context) : base(context)
		{
		}

		public void UpdateContent(IView? content, IMauiContext? mauiContext)
		{
			_contentView?.RemoveFromParent();

			if (content != null && mauiContext != null)
			{
				_contentView = content.ToPlatform(mauiContext);
				var layoutParams = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
				AddView(_contentView, layoutParams);
			}
		}

		internal ImageView? CircleImageView
		{
			get
			{
				for (int i = 0; i < ChildCount; i++)
				{
					var child = GetChildAt(i);

					if (child is ImageView iv && child != _contentView)
						return iv;
				}

				return null;
			}
		}

		public override bool CanChildScrollUp()
		{
			if (ChildCount == 0)
				return base.CanChildScrollUp();

			return CanScrollUp(_contentView);
		}

		bool CanScrollUp(AView? view)
		{
			if (!(view is ViewGroup viewGroup))
				return base.CanChildScrollUp();

			if (!CanScrollUpViewByType(view))
				return false;

			for (int i = 0; i < viewGroup.ChildCount; i++)
			{
				var child = viewGroup.GetChildAt(i);

				if (!CanScrollUpViewByType(child))
					return false;

				if (child is SwipeRefreshLayout)
				{
					return CanScrollUp(child as ViewGroup);
				}
			}

			return true;
		}

		bool CanScrollUpViewByType(AView? view)
		{
			if (view is AbsListView absListView)
			{
				if (absListView.FirstVisiblePosition == 0)
				{
					var subChild = absListView.GetChildAt(0);

					return subChild != null && subChild.Top != 0;
				}

				return true;
			}

			if (view is RecyclerView recyclerView)
				return recyclerView.ComputeVerticalScrollOffset() > 0;

			if (view is NestedScrollView nestedScrollView)
				return nestedScrollView.ComputeVerticalScrollOffset() > 0;

			if (view is AWebView webView)
				return webView.ScrollY > 0;

			return true;
		}
	}
}
