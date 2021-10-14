using Foundation;
using AppKit;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls;

namespace Maui.Controls.Sample.Macos
{
	public class App: Application
	{
		public App()
		{
			var page = new ContentPage();
			page.Title = "Hello!";
			StackLayout stack = new StackLayout();
			stack.Orientation = StackOrientation.Vertical;
			stack.Add(new Label { Text = "Hi Maui 1", HorizontalTextAlignment = TextAlignment.Start, TextColor = Microsoft.Maui.Graphics.Colors.Azure, LineBreakMode = LineBreakMode.NoWrap });
			stack.Add(new Label { Text = "Hi Maui 2", HorizontalTextAlignment = TextAlignment.End, TextColor = Microsoft.Maui.Graphics.Colors.Red, LineBreakMode = LineBreakMode.HeadTruncation });
			stack.Add(new Label { Text = "Hi Maui 3", HorizontalTextAlignment = TextAlignment.Center, TextColor = Microsoft.Maui.Graphics.Colors.Purple, LineBreakMode = LineBreakMode.MiddleTruncation });
			stack.Add(new Label { Text = "Hi Maui 4", TextColor = Microsoft.Maui.Graphics.Colors.Plum, LineBreakMode = LineBreakMode.CharacterWrap });
			stack.Add(new Label { Text = "Hi Maui 5", TextColor = Microsoft.Maui.Graphics.Colors.DarkGreen, LineBreakMode = LineBreakMode.TailTruncation });
			stack.Add(new Label { Text = "Hi Maui 6", TextColor = Microsoft.Maui.Graphics.Colors.Green, LineBreakMode = LineBreakMode.WordWrap });

			var okButton = new Button { Text = "OK", TextColor = Microsoft.Maui.Graphics.Colors.Green };
			stack.Add(okButton);

			okButton.Clicked += (s,e) =>
			{
				page.DisplayAlert("Alert", "You pressed OK button", "OK");
			};

			var cancelButton = new Button { Text = "Cancel", TextColor = Microsoft.Maui.Graphics.Colors.Aqua };
			stack.Add(cancelButton);

			cancelButton.Clicked += (s, e) =>
			{
				page.DisplayAlert("Alert", "You pressed Cancel button", "OK");
			};

			var grid = new GridLayout
			{
				RowDefinitions = {
					new RowDefinition (),
					new RowDefinition ()
				},
				ColumnDefinitions = {
					new ColumnDefinition (),
					new ColumnDefinition ()
				}
			};
			var cell = new Label { Text = "Cell 1" };
			grid.Add(cell);

			cell = new Label { Text = "Cell 2" };
			grid.Add(cell);
			grid.SetColumn(cell, 1);

			cell = new Label { Text = "Cell 3" };
			grid.Add(cell);
			grid.SetRow(cell, 1);

			cell = new Label { Text = "Cell 4" };
			grid.Add(cell);
			grid.SetRow(cell, 1);
			grid.SetColumn(cell, 1);

			stack.Add(grid);

			page.Content = stack;
			MainPage = page;
		}
	}
}