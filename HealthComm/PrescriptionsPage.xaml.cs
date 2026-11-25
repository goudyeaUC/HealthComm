using HealthComm.ViewModels;

namespace HealthComm
{
    [QueryProperty(nameof(UserEmail), "useremail")]
    public partial class PrescriptionsPage : ContentPage
    {
        private readonly PrescriptionsViewModel _viewModel = new();

        public string UserEmail { get; set; }

        public PrescriptionsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadPrescriptionsAsync(UserEmail);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.Clear();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
