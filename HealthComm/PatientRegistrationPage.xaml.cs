using HealthCommShared.DTOs;
using HealthCommShared.Services;
namespace HealthComm;

public partial class PatientRegistrationPage : ContentPage
{
    private readonly LoginAPIService _loginAPIService;
    public PatientRegistrationPage()
	{
		InitializeComponent();
	}

    private async void RegisterPatientButton_Clicked(object sender, EventArgs e)
    {
        string patientFirstName = PatientFirstNameEntry.Text;
        string patientLastName = PatientLastNameEntry.Text;
        string patientEmail = PatientEmailEntry.Text;
        string patientPhone = PatientPhoneEntry.Text;
        string patientPassword = PatientPasswordEntry.Text;
        if (string.IsNullOrWhiteSpace(patientFirstName) ||
            string.IsNullOrWhiteSpace(patientLastName) ||
            string.IsNullOrWhiteSpace(patientEmail) ||
            string.IsNullOrWhiteSpace(patientPhone) ||
            string.IsNullOrWhiteSpace(patientPassword))
        {
            await DisplayAlert("Error", "All fields are required.", "OK");
            return;
        }
        // Create a new PatientRegistrationDTO object for the patient
        PatientRegistrationDTO newPatientDTO = new PatientRegistrationDTO
        {
            FirstName = patientFirstName,
            LastName = patientLastName,
            Email = patientEmail,
            PhoneNumber = patientPhone,
            Password = patientPassword,
            GenderSelection = PatientGenderPicker.SelectedItem?.ToString() ?? "unknown",
            DateOfBirth = (DateTime)PatientDOBPicker.Date,
        };
        // Send the new patient DTO to the backend
        await _loginAPIService.RegisterPatientAsync(newPatientDTO);

        await DisplayAlert("Success", "Patient registered successfully.", "OK");
        // Navigate back to the login page or main page after registration
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}