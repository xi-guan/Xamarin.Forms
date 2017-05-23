using System.Diagnostics;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Bugzilla, 25943, "[Android] TapGestureRecognizer does not work with a nested StackLayout", PlatformAffected.Android)]
	public class Bugzilla25943 : TestContentPage
	{
		Label _result;
		int _taps;

		protected override void Init()
		{
			StackLayout layout = GetNestedStackLayout();

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += (sender, e) =>
			{
				_taps = _taps + 1;
				_result.Text = $"Taps: {_taps}";
			};
			layout.GestureRecognizers.Add(tapGestureRecognizer);

			Content = layout;
		}

		public StackLayout GetNestedStackLayout()
		{
			_result = new Label();

			var innerLayout = new StackLayout
			{
				AutomationId = "innerlayout",
				HeightRequest = 100,
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.AntiqueWhite,
				Children =
						{
							new Label
							{
								Text = "inner label",
								FontSize = 20,
								HorizontalOptions = LayoutOptions.Center,
								VerticalOptions = LayoutOptions.CenterAndExpand
							}
						}
			};

			var outerLayout = new StackLayout
			{
				AutomationId = "outerlayout",
				Orientation = StackOrientation.Vertical,
				Children =
						{
							_result,
							innerLayout,
							new Label
							{
								Text = "outer label",
								FontSize = 20,
								HorizontalOptions = LayoutOptions.Center,
							}
						}
			};

			return outerLayout;
		}
	}
}