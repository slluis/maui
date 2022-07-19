using System.Diagnostics;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery.Issues
{
#if UITEST
	[NUnit.Framework.Category(Compatibility.UITests.UITestCategories.Bugzilla)]
#endif
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Bugzilla, 39624, "CarouselPage.Children Appear Out of Order", PlatformAffected.WinRT)]
	internal class Bugzilla39624 : TestCarouselPage
	{
		protected override void Init()
		{
			var instructions =
				"Flip through each page of the carousel from 1 to 5; the pages should display in order. Then flip backward to page 1; if any of the pages display out of order, the test has failed.";

			Children.Add(GeneratePage("Page 1", Colors.Red, instructions));
			Children.Add(GeneratePage("Page 2", Colors.Green, instructions));
			Children.Add(GeneratePage("Page 3", Colors.Blue, instructions));
			Children.Add(GeneratePage("Page 4", Colors.Purple, instructions));
			Children.Add(GeneratePage("Page 5", Colors.Black, instructions));

			CurrentPageChanged += (sender, args) => Debug.WriteLine(CurrentPage.Title);
		}

		ContentPage GeneratePage(string title, Color color, string instructions)
		{
			var page = new ContentPage
			{
				Content = new StackLayout
				{
					Children = {
						new Label { Text = title, FontSize = 24, TextColor = Colors.White },
						new Label { Text = instructions, TextColor = Colors.White }
					}
				},
				BackgroundColor = color,
				Title = title
			};

			return page;
		}
	}
}
