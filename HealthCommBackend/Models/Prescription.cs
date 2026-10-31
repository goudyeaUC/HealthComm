namespace HealthCommBackend.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public ApplicationUser Prescriber { get; set; } = null!;
        //foreign key to the ApplicationUser underlying the prescriber (practitioner) of the prescription

        public ApplicationUser Patient { get; set; } = null!;
        //foreign key to the ApplicationUser underlying the patient for whom the prescription is intended

        public string MedicationName { get; set; } = null!;

        public String Instructions { get; set; } = null!;

        public String SNOMEDDrugCode { get; set; } = null!;
        //Standardized code for the prescribed drug, according to the SNOMED CT terminology system

        public String ReasonForPrescription { get; set; } = null!;

        public String SNOMEDReasonCode { get; set; } = null!;
        //Standardized code for the reason for the prescription, according to the SNOMED CT terminology system

        public DateTime DatePrescribed { get; set; } = DateTime.UtcNow;

        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(30);

        public DateTime? DateFilled { get; set; } = null;

        public String ActiveStatus { get; set; }= null!;

        public int RefillsRemaining { get; set; } = 0;

        public DateTime? LastSyncedAt { get; set; } = null!;
    }
}
