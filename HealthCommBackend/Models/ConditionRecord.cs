namespace HealthCommBackend.Models
{
    public class ConditionRecord
    {
        public int Id { get; set; }

        public ApplicationUser Patient { get; set; } = null!;

        public DateTime OnsetDate { get; set; }

        public DateTime? AbatementDate { get; set; } = null;
        // The date when the condition was resolved or went into remission, if applicable

        //INCOMPLETE: Needs SNOMED codes, 
    }
}
