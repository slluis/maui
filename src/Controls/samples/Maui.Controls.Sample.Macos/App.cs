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
			stack.Add(new Label { Text = "Hi Maui" });
			stack.Add(new Label { Text = "Hi Maui 2" });
			stack.Add(new Label { Text = "Hi Maui 3" });

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