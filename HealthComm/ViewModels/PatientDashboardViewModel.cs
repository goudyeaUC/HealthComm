using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using HealthComm.Models;

namespace HealthComm.ViewModels
{
    class PatientDashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Prescription> Prescriptions { get; set; }

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
            Prescriptions = new ObservableCollection<Prescription>
            {
            new Prescription
            {
            DrugName="Lisinopril",
            Dosage="10mg Daily"
            },
            new Prescription
            {
            DrugName="Metformin",
            Dosage="500mg Twice Daily"
            }
        };

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
