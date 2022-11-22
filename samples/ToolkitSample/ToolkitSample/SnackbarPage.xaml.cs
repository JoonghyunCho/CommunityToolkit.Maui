using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace ToolkitSample;

public partial class SnackbarPage : ContentPage
{
	public SnackbarPage()
	{
		InitializeComponent();
	}

	async void OnClicked(object sender, EventArgs e)
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		var snackbarOptions = new SnackbarOptions
		{
			BackgroundColor = Colors.Red,
			TextColor = Colors.Green,
			ActionButtonTextColor = Colors.Yellow,
			CornerRadius = new CornerRadius(10),
			Font = Font.SystemFontOfSize(14),
			ActionButtonFont = Font.SystemFontOfSize(14),
			CharacterSpacing = 0.5
		};

		string text = "This is a Snackbar";
		string actionButtonText = "Click Here to Dismiss";
		Action action = async () => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
		TimeSpan duration = TimeSpan.FromSeconds(3);

		var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

		await snackbar.Show(cancellationTokenSource.Token);
	}
}

