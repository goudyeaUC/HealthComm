namespace HealthComm;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    private void loginbutton_Clicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(Username.Text) ||
            string.IsNullOrWhiteSpace(UserEmail.Text))
        {
            ShowError("All fields are required.");
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