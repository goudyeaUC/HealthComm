using System;
using System.Collections.Generic;
using System.Text;

namespace HealthComm.Models
{
    public class Prescription
    {
        public string DrugName { get; set; }
        public string Dosage { get; set; }

        public string DisplayText =>
        $"{DrugName} - {Dosage}";
    }
}
