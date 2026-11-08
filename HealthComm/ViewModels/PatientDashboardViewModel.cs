using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthComm.Models;
using HealthCommShared.DTOs;

namespace HealthComm.ViewModels
{
    class PatientDashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // developer needs to check port for the backend API and update the URL accordingly
        private const string ApiBaseUrl = "http://localhost:5167/api";

        public ObservableCollection<PrescriptionDTO> Prescriptions { get; set; }

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

        public async Task LoadPrescriptionsAsync(string email)
        {
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

        public ObservableCollection<Appointment> Appointments { get; set; }

        private string question;
        public string Question
        {
            get => question;
            set
            {
                question = value;
                PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Question)));
            }
        }

        public ICommand ViewUpdatesCommand { get; }
        public ICommand ScheduleVisitCommand { get; }
        public ICommand SendQuestionCommand { get; }

        public PatientDashboardViewModel()
        {
            Prescriptions = new ObservableCollection<PrescriptionDTO>();

            Appointments = new ObservableCollection<Appointment>
            {
                new Appointment
                {
                VisitType="Primary Care Visit",
                AppointmentDate=
                new DateTime(2026,10,15,10,30,0)
                },
                new Appointment
                {
                VisitType="Lab Follow-Up",
                AppointmentDate=
                new DateTime(2026,11,10,9,0,0)
                }
            };

            ViewUpdatesCommand =
            new Command(async () =>
            {
                await Shell.Current.DisplayAlert(
                "Live Update",
                "Laboratory Department is delayed.",
                "OK");
            });

            ScheduleVisitCommand =
            new Command(async () =>
            {
                await Shell.Current.DisplayAlert(
                "Schedule",
                "Open Visit Scheduler",
                "OK");
            });

            SendQuestionCommand =
            new Command(async () =>
            {
                await Shell.Current.DisplayAlert(
                "Message",
                $"Question Sent:\n{Question}",
                "OK");
            });
        }
    }
}
