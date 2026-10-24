namespace HealthComm;

public partial class HealthCommHomePage : ContentPage
{
	public HealthCommHomePage()
	{
		InitializeComponent();
	}
    private async void LoginButton_Clicked(
       object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GoalStateLoginPage));
    }

    private async void RegisterPractitionerButton_Clicked(
        object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PractitionerRegistrationPage));
    }

    private async void RegisterPatientButton_Clicked(
        object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PatientRegistrationPage));
    }
}