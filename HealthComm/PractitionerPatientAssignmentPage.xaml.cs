
using HealthComm.Models;
using HealthCommShared.DTOs;
using Hl7.Fhir.Model;
using System.Net.Http.Json;

namespace HealthComm;


public partial class PractitionerPatientAssignmentPage : ContentPage
{
	public PractitionerPatientAssignmentPage()
	{
		InitializeComponent();
        LoadDropdowns();
    }
    private HttpClient _httpClient;
    PatientAppointmentDTO patientSelected;
    PractitionerAppointmentDTO practitionerSelected = null;
    private List<PatientAppointmentDTO> _patients = new();
    private List<PractitionerAppointmentDTO> _practitioners = new();

    public void PractitionerAppointment()
    {
        InitializeComponent();

       
       
    }

    private async void LoadDropdowns()
    {
        try
        {
            _httpClient = new HttpClient();
            //TODO Note: If you're running the MAUI app on an Android emulator, replace localhost with 10.0.2.2
            _patients =
                await _httpClient.GetFromJsonAsync<List<PatientAppointmentDTO>>(
                    "http://localhost:5167/api/PatientAppointments/patients")
                ?? new List<PatientAppointmentDTO>();

            _practitioners =
                await _httpClient.GetFromJsonAsync<List<PractitionerAppointmentDTO>>(
                    "http://localhost:5167/api/PatientAppointments/practitioners")
                ?? new List<PractitionerAppointmentDTO>();

            PatientPicker.ItemsSource = _patients;
            PractitionerPicker.ItemsSource = _practitioners;

        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Loading Error",
                $"Could not load patients or practitioners: {ex.Message}",
                "OK");
        }
    }

    private void PatientPicker_SelectedIndexChanged(
        object sender, EventArgs e)
    {
        if (PatientPicker.SelectedItem is PatientAppointmentDTO patient)
        {
            PatientInfoLabel.Text = $"Patient ID: {patient.Id} | Name: {patient.FirstName} {patient.LastName}";
            patientSelected = patient;
        }
        if(practitionerSelected != null && patientSelected != null)
        {
            PractitionerPicker_SelectedIndexChanged(sender, e);
        }
       
    }

    private async void PractitionerPicker_SelectedIndexChanged(
        object sender, EventArgs e)
    {
        if (PractitionerPicker.SelectedItem
            is PractitionerAppointmentDTO practitioner)
        {
            PractitionerNameLabel.Text =
                $"Practitioner ID: {practitioner.Id} | Name: {practitioner.FirstName} {practitioner.LastName}";
            practitionerSelected = practitioner;
        }
        try
        {
            using var httpClient = new HttpClient();

            var url =
                $"http://localhost:5167/api/PatientAppointments/" +
                $"patient/{patientSelected.Id}/practitioner/{practitionerSelected.Id}";

            var appointments =
                await httpClient.GetFromJsonAsync<List<AppointmentDTO>>(url);

            PatientList.ItemsSource = appointments ?? new List<AppointmentDTO>();
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Connection Error",
                ex.Message,
                "OK");
        }
    }

    private void AppointmentDatePicker_DateSelected(
        object sender, DateChangedEventArgs e)
    {
        UpdateAppointmentInfo();
    }

    private void AppointmentTimePicker_PropertyChanged(
        object sender,
        System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimePicker.Time))
        {
            UpdateAppointmentInfo();
        }
    }

    private void UpdateAppointmentInfo()
    {
        AppointmentInfoLabel.Text =
            $"Selected: {AppointmentDatePicker.Date:MM/dd/yyyy} " +
            $"{AppointmentTimePicker.Time:hh\\:mm}";
    }

    private async void OkButton_Clicked(
        object sender, EventArgs e)
    {
        /*
        if (PatientPicker.SelectedItem is not PatientProfile patient ||
            PractitionerPicker.SelectedItem
                is not PractitionerProfile practitioner)
        {
            await DisplayAlert(
                "Selection Required",
                "Please select both a patient and a practitioner.",
                "OK");
            return;
        }

        await DisplayAlert(
            "Appointment Selection",
            $"Patient ID: {patient.Id}\n" +
            $"Practitioner ID: {practitioner.Id}\n" +
            $"Date: {AppointmentDatePicker.Date:MM/dd/yyyy}\n" +
            $"Time: {AppointmentTimePicker.Time}",
            "OK");
        */
        if (PatientPicker.SelectedItem is not PatientAppointmentDTO patient)
        {
            await DisplayAlert("Required", "Select a patient.", "OK");
            return;
        }

        if (PractitionerPicker.SelectedItem
            is not PractitionerAppointmentDTO practitioner)
        {
            await DisplayAlert("Required", "Select a practitioner.", "OK");
            return;
        }

        var startDateTime =
            AppointmentDatePicker.Date +
            AppointmentTimePicker.Time;

        var request = new CreatePatientAppointmentRequest
        {
            PatientProfileId = patient.Id,
            PractitionerProfileId = practitioner.Id,
            StartDateTime = (DateTime)startDateTime,
            VisitType = "General Consultation",
            Description = "Appointment created from HealthComm MAUI"
        };
        request.EndDateTime = request.EndDateTime ?? request.StartDateTime.AddMinutes(30);

        try
        {
            using var httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync(
                "http://localhost:5167/api/PatientAppointments",
                request);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert(
                    "Success",
                    "Appointment created successfully.",
                    "OK");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();

                await DisplayAlert(
                    "Error",
                    $"Could not create appointment. " +
                    $"HTTP {(int)response.StatusCode}: {error}",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Connection Error",
                ex.Message,
                "OK");
        }
    }

    private async void CancelButton_Clicked(
        object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}