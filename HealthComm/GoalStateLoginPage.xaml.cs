using HealthCommShared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace HealthComm;

public partial class GoalStateLoginPage : ContentPage
{
	public GoalStateLoginPage()
	{
		InitializeComponent();
	}
    private async void loginbutton_Clicked(
    object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert(
                "Missing Information",
                "Please enter your email and password.",
                "OK");
            return;
        }

        try
        {
            var request = new LoginRequestDTO
            {
                Email = EmailEntry.Text.Trim(),
                GivenPassword = PasswordEntry.Text
            };

            /*     using var _httpClient = new HttpClient();
                 var response = await _httpClient.PostAsJsonAsync(
                     "api/Login/login", request); */
            // TODO call backend LoginService and handle the response appropriately.

            var response = new
            {
                message = "Login successful.",
                
            };
            if (response != null)
            {
                await DisplayAlert(
                    "Success", "Login successful.", "OK");

                // Navigate to the correct dashboard after login.
                // For example:
                // await Shell.Current.GoToAsync(nameof(PatientDashboardPage));
            }
            else
            {
                await DisplayAlert(
                    "Login Failed",
                    "Invalid email or password.",
                    "OK");
            }
        }
        catch (HttpRequestException)
        {
            await DisplayAlert(
                "Connection Error",
                "Cannot connect to HealthCommBackend. Check the server URL.",
                "OK");
        }
    }
}