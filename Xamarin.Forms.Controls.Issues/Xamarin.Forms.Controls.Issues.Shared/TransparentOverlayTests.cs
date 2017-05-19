using System.Diagnostics;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.None, 618, "Transparent Overlays", PlatformAffected.All)]
	public class TransparentOverlayTests : TestContentPage
	{
		protected override void Init()
		{
			var grid = new Grid
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill
			};
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

			var button1 = new Button
			{
				Text = "Button 1",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};
			button1.Clicked += (sender, args) =>
			{
				// This line executes on Android, but not on iOS or UWP
				Debug.WriteLine($">>>>> TransparentOverlayTests Init 81: Button1");
			};
			grid.Children.Add(button1);
			Grid.SetRow(button1, 0);

			var button2 = new Button
			{
				Text = "Button 2",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};
			button2.Clicked += (sender, args) =>
			{
				// This line doesn't execute on any platform
				Debug.WriteLine($">>>>> TransparentOverlayTests Init 38: Button2");
			};
			grid.Children.Add(button2);
			Grid.SetRow(button2, 1);

			// Put a stack layout over the button in the top half of the grid
			var layout1 = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.Transparent, // and make it "transparent",
				Opacity = 0
			};

			grid.Children.Add(layout1);
			Grid.SetRow(layout1, 0);

			// Put a stack layout over the button in the bottom half of the grid
			var layout2 = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.Blue, // make it "not transparent"
				Opacity = 0 
			};

			grid.Children.Add(layout2);
			Grid.SetRow(layout2, 1);

			Content = grid;
		}
	}
}