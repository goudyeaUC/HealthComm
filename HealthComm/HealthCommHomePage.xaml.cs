namespace HealthComm;

public partial class HealthCommHomePage : ContentPage
{
	public HealthCommHomePage()
	{
		InitializeComponent();
	}

    private async void PractitionerWaitTimeButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PractitionerDashboardPage());
    }

    private async void AppointmentManagerButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentManagerPage());
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


    private async void PractitionerPatientAssignmentUIButton_Clicked(
       object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PractitionerPatientAssignmentPage));
    }
}