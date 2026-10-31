
using HealthCommShared.DTOs;     
using System.Net.Http;
using System.Text.Json;


namespace HealthComm;

public partial class AppointmentManagerPage : ContentPage
{
	public AppointmentManagerPage()
	{
		InitializeComponent();
        retrieveAppointments();
    }

    private async void retrieveAppointments()
    {
        try
        {
            using var client = new HttpClient();
            string url = "http://localhost:5167/api/Appointment/sample";
            var response = await client.GetAsync(url);

            string str2 = await response.Content.ReadAsStringAsync();
            List<AppointmentDTO> appointments =
            JsonSerializer.Deserialize<List<AppointmentDTO>>(
                str2,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<AppointmentDTO>();
            IsBusy = true;

            

            if (appointments == null || appointments.Count == 0)
            {
                await DisplayAlert(
                    "Appointments",
                    "The API returned no appointments.",
                    "OK");
            }
            else
            {
                await DisplayAlert(
                "Appointments",
                $"The API returned {appointments.Count} appointment(s).",
                "OK");
            }
            PatientList.ItemsSource = appointments;
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "API Error",
                $"Could not load appointments.\n{ex.Message}",
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
        


    }
    private void AppointmentDatePicker_DateSelected(
    object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate =
                 AppointmentDatePicker.Date ?? DateTime.Today;
        TimeSpan selectedTime = AppointmentTimePicker.Time ?? TimeSpan.Zero;

        DateTime timestamp = selectedDate.Add(selectedTime);

        AppointmentInfoLabel.Text =
            $"Appointment: {timestamp:MM/dd/yyyy hh:mm tt}";
    }

    private void AppointmentTimePicker_PropertyChanged(
    object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimePicker.Time))
        {
            DateTime selectedDate =
                AppointmentDatePicker.Date ?? DateTime.Today;

            TimeSpan selectedTime =
                AppointmentTimePicker.Time ?? TimeSpan.Zero;

            DateTime timestamp = selectedDate.Add(selectedTime);

            AppointmentInfoLabel.Text =
                $"Appointment: {timestamp:MM/dd/yyyy hh:mm tt}";
        }
    }
    private async void OkButton_Clicked(object sender, EventArgs e)
    {
        /* if (PatientList.SelectedItem == null)
         {
             await DisplayAlert("Missing Information",
                 "Please select a patient.", "OK");
             return;
         }

         DateTime appointmentTimestamp =
             AppointmentDatePicker.Date.Add(
                 AppointmentTimePicker.Time);

         // TODO: Save the selected patient and appointment timestamp.
         StatusLabel.Text =
             $"Appointment selected: {appointmentTimestamp:MM/dd/yyyy hh:mm tt}";*/

        Shell.Current.GoToAsync(nameof(GoalStateLoginPage));
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Cancel Appointment",
            "Are you sure you want to cancel?",
            "Yes", "No");

        // if (confirm)
        // {
        Shell.Current.GoToAsync(nameof(GoalStateLoginPage));
        //  }
    }
}