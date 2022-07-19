﻿using System;
using System.Linq;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery.GalleryPages.SwipeViewGalleries
{
	[Preserve(AllMembers = true)]
	public class SwipeTransitionModeGallery : ContentPage
	{
		public SwipeTransitionModeGallery()
		{
			Title = "SwipeTransitionMode Gallery";

			var scroll = new ScrollView();

			var swipeLayout = new StackLayout
			{
				Margin = new Thickness(12)
			};

			var instructions = new Label
			{
				BackgroundColor = Colors.Black,
				TextColor = Colors.White,
				Text = "This Gallery use a Platform Specific only available for Android and iOS."
			};

			swipeLayout.Children.Add(instructions);

			var swipeItemSwipeTransitionModeLabel = new Label
			{
				FontSize = 10,
				Text = "SwipeTransitionMode:"
			};

			swipeLayout.Children.Add(swipeItemSwipeTransitionModeLabel);

			var swipeItemSwipeTransitionModePicker = new Picker();

			var swipeTransitionModes = Enum.GetNames(typeof(SwipeTransitionMode)).Select(t => t).ToList();

			swipeItemSwipeTransitionModePicker.ItemsSource = swipeTransitionModes;
			swipeItemSwipeTransitionModePicker.SelectedIndex = 0;   // Reveal

			swipeLayout.Children.Add(swipeItemSwipeTransitionModePicker);

			var deleteSwipeItem = new SwipeItem
			{
				BackgroundColor = Colors.Red,
				IconImageSource = "calculator.png",
				Text = "Delete"
			};

			deleteSwipeItem.Invoked += (sender, e) => { DisplayAlert("SwipeView", "Delete Invoked", "Ok"); };

			var swipeItems = new SwipeItems { deleteSwipeItem };

			swipeItems.Mode = SwipeMode.Reveal;

			var leftSwipeContent = new Grid
			{
				BackgroundColor = Colors.Gray
			};

			var leftSwipeLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Swipe to Right"
			};

			leftSwipeContent.Children.Add(leftSwipeLabel);

			var leftSwipeView = new SwipeView
			{
				HeightRequest = 60,
				WidthRequest = 300,
				LeftItems = swipeItems,
				Content = leftSwipeContent
			};

			swipeLayout.Children.Add(leftSwipeView);

			var rightSwipeContent = new Grid
			{
				BackgroundColor = Colors.Gray
			};

			var rightSwipeLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Swipe to Left"
			};

			rightSwipeContent.Children.Add(rightSwipeLabel);

			var rightSwipeView = new SwipeView
			{
				HeightRequest = 60,
				WidthRequest = 300,
				RightItems = swipeItems,
				Content = rightSwipeContent
			};

			swipeLayout.Children.Add(rightSwipeView);

			var topSwipeContent = new Grid
			{
				BackgroundColor = Colors.Gray
			};

			var topSwipeLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Swipe to Top"
			};

			topSwipeContent.Children.Add(topSwipeLabel);

			var topSwipeView = new SwipeView
			{
				HeightRequest = 60,
				WidthRequest = 300,
				BottomItems = swipeItems,
				Content = topSwipeContent
			};

			swipeLayout.Children.Add(topSwipeView);

			var bottomSwipeContent = new Grid
			{
				BackgroundColor = Colors.Gray
			};

			var bottomSwipeLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Swipe to Bottom"
			};

			bottomSwipeContent.Children.Add(bottomSwipeLabel);

			var bottomSwipeView = new SwipeView
			{
				HeightRequest = 60,
				WidthRequest = 300,
				TopItems = swipeItems,
				Content = bottomSwipeContent
			};

			swipeLayout.Children.Add(bottomSwipeView);

			swipeItemSwipeTransitionModePicker.SelectedIndexChanged += (sender, e) =>
			{
				var swipeTransitionMode = swipeItemSwipeTransitionModePicker.SelectedItem;

				switch (swipeTransitionMode)
				{
					case "Drag":
						leftSwipeView.On<Android>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						leftSwipeView.On<iOS>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);

						rightSwipeView.On<Android>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						rightSwipeView.On<iOS>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);

						topSwipeView.On<Android>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						topSwipeView.On<iOS>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);

						bottomSwipeView.On<Android>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						bottomSwipeView.On<iOS>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						break;
					case "Reveal":
						leftSwipeView.On<Android>().SetSwipeTransitionMode(SwipeTransitionMode.Reveal);
						leftSwipeView.On<iOS>().SetSwipeTransitionMode(SwipeTransitionMode.Reveal);

						rightSwipeView.On<Android>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						rightSwipeView.On<iOS>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);

						topSwipeView.On<Android>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						topSwipeView.On<iOS>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);

						bottomSwipeView.On<Android>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						bottomSwipeView.On<iOS>().SetSwipeTransitionMode(SwipeTransitionMode.Drag);
						break;
				}
			};

			scroll.Content = swipeLayout;

			Content = scroll;
		}
	}
}