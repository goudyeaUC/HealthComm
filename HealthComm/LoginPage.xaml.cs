
using HealthComm.Models;
using HealthCommFrontend.Services;
using HealthCommShared.DTOs;
using HealthCommShared.Services;


namespace HealthComm;

public partial class LoginPage : ContentPage
{
    private readonly LoginAPIService _loginAPIService;
    public LoginPage(LoginAPIService loginAPIService)
    {
        InitializeComponent();
        _loginAPIService = loginAPIService;
    }
    private async void loginbutton_Clicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(UserEmail.Text) ||
            string.IsNullOrWhiteSpace(UserPassword.Text))
        {
            ShowError("All fields are required.");
            return;
        }

        var result = await _loginAPIService.LoginAsync( new LoginRequestDTO{ Email = UserEmail.Text, Password = UserPassword.Text });

        if (result == null)
        {
            ShowError("Invalid email or password.");
            return;
        }
        else { await Shell.Current.GoToAsync(nameof(MainPage)); }
    }
    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }

    private void RegisterPatientButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(PatientRegistrationPage));
    }

    private void RegisterPractitionerButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(PractitionerRegistrationPage));
    }

    private void guestbutton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(MainPage));
        // Shell.Current.GoToAsync(nameof(DashboardPage));
    }
    private void backButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
        // Shell.Current.GoToAsync(nameof(HomePage));
    }
    private void HomeButton_Clicked(object sender, EventArgs e)
    {
        // Shell.Current.GoToAsync("..");
        Shell.Current.GoToAsync(nameof(HomePage));
    }
    private void AdminButton_Clicked(object sender, EventArgs e)
    {
        // Shell.Current.GoToAsync("..");
        Shell.Current.GoToAsync(nameof(AdminPage));
    }


}