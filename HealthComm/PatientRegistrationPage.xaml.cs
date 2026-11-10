using HealthCommShared.DTOs;

namespace HealthComm;

public partial class PatientRegistrationPage : ContentPage
{
	public PatientRegistrationPage()
	{
		InitializeComponent();
	}

    private async void RegisterPatientButton_Clicked(object sender, EventArgs e)
    {
        string patientName = PatientNameEntry.Text;
        string patientEmail = PatientEmailEntry.Text;
        string patientPhone = PatientPhoneEntry.Text;
        string patientPassword = PatientPasswordEntry.Text;
        if (string.IsNullOrWhiteSpace(patientName) ||
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
            Username = patientName,
            Email = patientEmail,
            PhoneNumber = patientPhone,
            Password = patientPassword
        };
        // Save the new patient session to the database
        await _dbService.AddSession(newPatientDTO);
        await DisplayAlert("Success", "Patient registered successfully.", "OK");
        // Navigate back to the login page or main page after registration
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}