using HealthCommShared.DTOs;

namespace HealthCommBackend.Services
{
    public class WaitTimeService
    {
        // Hospital information
        private readonly List<(string Name, string Location)> _hospitals =
            new List<(string Name, string Location)>
        {
            ("Good Samaritan Hospital", "Clifton, Cincinnati"),
            ("Bethesda North Hospital", "Montgomery, Cincinnati"),
            ("Good Samaritan Western Ridge", "West Side, Cincinnati"),
            ("UC Medical Center", "Clifton, Cincinnati"),
            ("The Christ Hospital", "Mount Auburn, Cincinnati"),
            ("Mercy Health - Anderson Hospital", "Anderson Township"),
            ("Mercy Health - West Hospital", "Western Cincinnati")
        };

        // Generate a simulated wait time between 5 and 120 minutes.
        private int GenerateWaitTime()
        {
            return Random.Shared.Next(5, 121);
        }

        // Retrieve hospital wait-time information.
        public List<HospitalWaitTimeDTO> GetHospitalWaitTimes()
        {
            var lastUpdated = DateTimeOffset.UtcNow;

            return _hospitals.Select(hospital => new HospitalWaitTimeDTO
            {
                Name = hospital.Name,
                Location = hospital.Location,
                EstimatedWaitTimeMinutes = GenerateWaitTime(),
                Status = "Simulated",
                LastUpdated = lastUpdated
            }).ToList();
        }
    }
}
