using HealthCommFrontend.Services;
using HealthCommShared.DTOs;
namespace HealthComm;

public partial class PractitionerRegistrationPage : ContentPage
{
    private readonly LoginAPIService _loginAPIService;

    public PractitionerRegistrationPage(LoginAPIService loginAPIService)
	{
		InitializeComponent();
		_loginAPIService = loginAPIService;
	}

    private async void RegisterPractitionerButton_Clicked(object sender, EventArgs e)
    {
        string practitionerFirstName = PractitionerFirstNameEntry.Text;
        string practitionerLastName = PractitionerLastNameEntry.Text;
        string practitionerEmail = PractitionerEmailEntry.Text;
    
        string practitionerPassword = PractitionerPasswordEntry.Text;
        if (string.IsNullOrWhiteSpace(practitionerFirstName) ||
            string.IsNullOrWhiteSpace(practitionerLastName) ||
            string.IsNullOrWhiteSpace(practitionerEmail) ||
            string.IsNullOrWhiteSpace(practitionerPassword))
        {
            await DisplayAlert("Error", "All fields are required.", "OK");
            return;
        }
        // Create a new PractitionerRegistrationDTO object for the practitioner
        PractitionerRegistrationDTO newPractitionerDTO = new PractitionerRegistrationDTO
        {
            FirstName = practitionerFirstName,
            LastName = practitionerLastName,
            Email = practitionerEmail,
            Password = practitionerPassword,
            Specialty = PractitionerSpecialty.Text,
            DateOfBirth = (DateTime)PractitionerDOBPicker.Date,
        };
        // Send the new practitioner DTO to the backend
        await _loginAPIService.RegisterPractitionerAsync(newPractitionerDTO);
        await DisplayAlert("Success", "Practitioner registered successfully.", "OK");
        // Navigate back to the login page or main page after registration
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}