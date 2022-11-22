using CommunityToolkit.Maui.Animations;

namespace ToolkitSample;

public partial class FadeAnimationPage : ContentPage
{
	public FadeAnimationPage()
	{
		InitializeComponent();
	}

	async void OnClicked(object sender, EventArgs e)
	{

		var fadeAnimation = new FadeAnimation();
		await fadeAnimation.Animate(label);
	}
}

