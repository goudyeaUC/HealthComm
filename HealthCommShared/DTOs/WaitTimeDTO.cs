namespace HealthCommShared.DTOs
{
    public class HospitalWaitTimeDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int EstimatedWaitTimeMinutes { get; set; }

        public string Status { get; set; } = "Simulated";

        public DateTimeOffset LastUpdated { get; set; }
    }
}
