namespace HealthComm;

using HealthCommShared.Services;
using HealthComm.Models;

public partial class AdminPage : ContentPage
{
    private readonly LocalDBService _dbService;
    private int _editSessionId;
    public AdminPage(LocalDBService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Lv.ItemsSource = await _dbService.GetSessions();
    }

    private async void saveButton_Clicked(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(appointmentdateEntry.Text) ||
            string.IsNullOrWhiteSpace(visittypeEntry.Text) ||
            string.IsNullOrWhiteSpace(patientnameEntry.Text) ||
            string.IsNullOrWhiteSpace(drugnameEntry.Text) ||
            string.IsNullOrWhiteSpace(dosageEntry.Text))
        {
            await DisplayAlert("Error", "All fields are required.", "OK");
            return;
        }

        if (_editSessionId == 0)
        {
            await _dbService.Create(new Session
            {
                AppointmentDate = appointmentdateEntry.Text,
                VisitType = visittypeEntry.Text,
                PatientName = patientnameEntry.Text,
                DrugName = drugnameEntry.Text,
                Dosage = dosageEntry.Text
            });

        }
        else
        {
            await _dbService.Update(new Session
            {
                Id = _editSessionId,
                AppointmentDate = appointmentdateEntry.Text,
                VisitType = visittypeEntry.Text,
                PatientName = patientnameEntry.Text,
                DrugName = drugnameEntry.Text,
                Dosage = dosageEntry.Text

            });
            _editSessionId = 0;
        }
        appointmentdateEntry.Text = "";
        visittypeEntry.Text = "";
        patientnameEntry.Text = "";
        drugnameEntry.Text = "";
        dosageEntry.Text = "";
        Lv.ItemsSource = await _dbService.GetSessions();
    }

    private async void Lv_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var session = e.Item as Session;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
        switch (action)
        {
            case "Edit":
                _editSessionId = session.Id;
                appointmentdateEntry.Text = session.AppointmentDate;
                visittypeEntry.Text = session.VisitType;
                patientnameEntry.Text = session.PatientName;
                drugnameEntry.Text = session.DrugName;
                dosageEntry.Text = session.Dosage;
                break;
            case "Delete":
                await _dbService.Delete(session);
                Lv.ItemsSource = await _dbService.GetSessions();
                break;
        }
    }
    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        // Shell.Current.GoToAsync("..");
        Shell.Current.GoToAsync(nameof(LoginPage));
    }
    private void backButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}