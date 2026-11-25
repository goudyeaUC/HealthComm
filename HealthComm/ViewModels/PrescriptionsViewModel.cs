using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthCommShared.DTOs;

namespace HealthComm.ViewModels
{
    class PrescriptionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // developer needs to check port for the backend API and update the URL accordingly
        private const string ApiBaseUrl = "http://localhost:8080/api";

        public ObservableCollection<PrescriptionDTO> Prescriptions { get; } = new();

        private string prescriptionStatus = string.Empty;
        public string PrescriptionStatus
        {
            get => prescriptionStatus;
            set
            {
                prescriptionStatus = value;
                PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(PrescriptionStatus)));
            }
        }

        private string currentEmail = string.Empty;

        public ICommand RequestRefillCommand { get; }

        public PrescriptionsViewModel()
        {
            RequestRefillCommand = new Command<PrescriptionDTO>(
                async p => await RequestRefillAsync(p));
        }

        private async Task RequestRefillAsync(PrescriptionDTO prescription)
        {
            if (prescription == null)
            {
                return;
            }

            try
            {
                using var client = new HttpClient();
                var request = new CreateRefillRequest
                {
                    PrescriptionId = prescription.Id,
                    PatientId = prescription.PatientId
                };

                var response = await client.PostAsJsonAsync($"{ApiBaseUrl}/refillrequests", request);

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert(
                        "Refill Requested",
                        $"Your refill request for {prescription.MedicationName} was sent for review.",
                        "OK");
                    await LoadPrescriptionsAsync(currentEmail);
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    await Shell.Current.DisplayAlert("Refill Not Requested", message, "OK");
                }
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert(
                    "Refill Not Requested",
                    "Unable to reach the server. Please try again.",
                    "OK");
            }
        }

        public async Task LoadPrescriptionsAsync(string email)
        {
            currentEmail = email;
            Prescriptions.Clear();

            if (string.IsNullOrWhiteSpace(email))
            {
                PrescriptionStatus = "Sign in with your email to see prescriptions.";
                return;
            }

            try
            {
                using var client = new HttpClient();
                var url = $"{ApiBaseUrl}/prescriptions/patient?email={Uri.EscapeDataString(email)}";
                var list = await client.GetFromJsonAsync<List<PrescriptionDTO>>(url);

                foreach (var p in list ?? new List<PrescriptionDTO>())
                {
                    Prescriptions.Add(p);
                }

                PrescriptionStatus = Prescriptions.Count == 0
                    ? "No prescriptions on file."
                    : string.Empty;
            }
            catch (Exception)
            {
                PrescriptionStatus = "Unable to load prescriptions.";
            }
        }

        // Clears prescription data from memory when the user leaves the page.
        public void Clear()
        {
            Prescriptions.Clear();
            PrescriptionStatus = string.Empty;
        }
    }
}
