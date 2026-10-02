namespace HealthComm;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}
    private void startButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(LoginPage));
    }

    private void adminButton_Clicked(object sender, EventArgs e)
    {
        var page = Application.Current.Handler.MauiContext.Services.GetService<AdminPage>();

        Shell.Current.Navigation.PushAsync(page);
    }
}