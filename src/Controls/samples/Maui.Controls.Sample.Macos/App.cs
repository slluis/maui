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
			var stack = new StackLayout() { Spacing = 10, Orientation = StackOrientation.Vertical };

			var horizontal = new StackLayout() { Spacing = 10, Orientation = StackOrientation.Horizontal };
			stack.Add(horizontal);
			horizontal.Add(new Label { Text = "Name" });
			horizontal.Add(new Label { Text = "Lluis" });

			stack.Add(new Label { Text = "Hi Maui 1 !!!! this is a super long text", HorizontalTextAlignment = TextAlignment.Start, TextColor = Microsoft.Maui.Graphics.Colors.Azure, LineBreakMode = LineBreakMode.NoWrap });
			stack.Add(new Label { Text = "Hi Maui 2 !!!! this is a super long text", HorizontalTextAlignment = TextAlignment.End, TextColor = Microsoft.Maui.Graphics.Colors.Red, LineBreakMode = LineBreakMode.HeadTruncation });
			stack.Add(new Label { Text = "Hi Maui 3 !!!! this is a super long text", HorizontalTextAlignment = TextAlignment.Center, TextColor = Microsoft.Maui.Graphics.Colors.Purple, LineBreakMode = LineBreakMode.MiddleTruncation });
			stack.Add(new Label { Text = "Hi Maui 4 !!!! this is a super long text", TextColor = Microsoft.Maui.Graphics.Colors.Plum, LineBreakMode = LineBreakMode.CharacterWrap });
			stack.Add(new Label { Text = "Hi Maui 5 !!!! this is a super long text", TextColor = Microsoft.Maui.Graphics.Colors.DarkGreen, LineBreakMode = LineBreakMode.TailTruncation });
			stack.Add(new Label { Text = "Hi Maui 6 !!!! this is a super long text", TextColor = Microsoft.Maui.Graphics.Colors.Green, LineBreakMode = LineBreakMode.WordWrap });

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