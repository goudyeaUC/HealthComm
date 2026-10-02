using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthComm.Models
{
    [Table("Session")]
    public class Session
    {
        [PrimaryKey, AutoIncrement]

        [Column("id")]
        public int Id { get; set; }

        [Column("AppointmentDate")]
        public string AppointmentDate { get; set; }

        [Column("VisitType")]

        public string VisitType { get; set; }
        [Column("PatientName")]

        public string PatientName { get; set; }

        [Column("DrugName")]
        public string DrugName { get; set; }

        [Column("Dosage")]
        public string Dosage { get; set; }

        [Column("is_favorite")]
        public bool IsFavorite { get; set; }

        public string FavoriteIcon => IsFavorite ? "★" : "☆";
    }
}
