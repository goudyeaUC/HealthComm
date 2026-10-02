using HealthComm.ViewModels;

namespace HealthComm
{
    [QueryProperty(nameof(User_Name), "username")]
    [QueryProperty(nameof(UserEmail), "useremail")]
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public string User_Name { get; set; }
        public string UserEmail { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext =
            new PatientDashboardViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Add blank line after the text

            string displayName = string.IsNullOrWhiteSpace(User_Name)
            ? "Guest"
            : User_Name;

            LblUsername.Text = $"Welcome {displayName}{Environment.NewLine}";
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            // Shell.Current.GoToAsync("..");
            Shell.Current.GoToAsync(nameof(LoginPage));
        }
        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            // Shell.Current.GoToAsync("..");
            Shell.Current.GoToAsync(nameof(LoginPage));
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
}
