
using HealthComm.Models;
using HealthCommShared.Services;


namespace HealthComm;

public partial class LoginPage : ContentPage
{
    private readonly LocalDBService _dbService;
    public LoginPage(LocalDBService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
    }
    private async void loginbutton_Clicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(Username.Text) ||
            string.IsNullOrWhiteSpace(UserEmail.Text))
        {
            ShowError("All fields are required.");
            return;
        }

        //      var session = await _dbService.GetSessionByEmail(UserEmail.Text);
        List<Session> sessions = await _dbService.GetSessions();
        Session session = sessions.FirstOrDefault(s =>
            s.Email == UserEmail.Text);

  
        if (session == null)
        {
            ShowError("User not found. Please check your email.");
            return;
        } 
        var myData = new Dictionary<string, object>
            {
                {"username",$"{Username.Text}" },
                {"useremail", $"{UserEmail.Text}" }
            };
        Shell.Current.GoToAsync(nameof(MainPage), myData);
    }
    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
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