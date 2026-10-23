using HealthCommShared.Services;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;


namespace HealthComm;

[QueryProperty(nameof(PractitionerId), "practitionerid")]
[QueryProperty(nameof(PractitionerName), "practitionername")]
public partial class PractionerAppointment : ContentPage
{
    private readonly LocalDBService _dbService;
    public string PractitionerId { get; set; }
    public string PractitionerName { get; set; }
    private string _practitionerId = "14927";
    private readonly string _fhirBaseUrl = "https://hapi.fhir.org/baseR4";
    public PractionerAppointment(LocalDBService dbService)
	{
		InitializeComponent();
		_dbService = dbService;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //  Lv.ItemsSource = null;
        PatientList.ItemsSource = await _dbService.GetSessions();
        PractitionerIdLabel.Text = $"Practitioner ID: {PractitionerId}";
        PractitionerNameLabel.Text = $"Practitioner Name: {PractitionerName}";
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

        Shell.Current.GoToAsync(nameof(AdminPage));
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Cancel Appointment",
            "Are you sure you want to cancel?",
            "Yes", "No");

       // if (confirm)
       // {
            Shell.Current.GoToAsync(nameof(AdminPage));
      //  }
    }

}