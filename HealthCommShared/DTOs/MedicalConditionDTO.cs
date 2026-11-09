using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCommShared.DTOs
{
    public class MedicalConditionDTO
    {
        public int ConditionId { get; set; }


        public int PatientId { get; set; }

        public string ConditionName { get; set; } = string.Empty;

        // Examples: Disease, Allergy, Injury

        public string Category { get; set; } = "Disease";

        // Examples: Active, Resolved, Inactive

        public string ClinicalStatus { get; set; } = "active";


    }
}
