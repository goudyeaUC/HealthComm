using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCommShared.DTOs
{
    public class PractitionerAppointmentDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key to ApplicationUser
                                           //   public ApplicationUser UnderlyingUserKey { get; set; } // Navigation property to ApplicationUser

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Specialty { get; set; } // FHIR field for description of the practitioner's specialty

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true; //FHIR field; indicates if the practitioner is currently active

        public DateTime? LastSyncedAt { get; set; }
        //FHIR field; indicates the last time the practitioner's information was synchronized with an external system
    }
}
