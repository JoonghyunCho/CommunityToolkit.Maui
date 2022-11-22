using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace ToolkitSample;

public partial class ToastPage : ContentPage
{
	public ToastPage()
	{
		InitializeComponent();
	}

	async void OnClicked(object sender, EventArgs e)
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		string text = "This is a Toast";
		ToastDuration duration = ToastDuration.Short;
		double fontSize = 14;

		var toast = Toast.Make(text, duration, fontSize);

		await toast.Show(cancellationTokenSource.Token);
	}
}

