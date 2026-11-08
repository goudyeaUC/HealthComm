using System;

namespace HealthCommShared.DTOs
{
    public class ReviewRefillRequest
    {
        public string ReviewerId { get; set; } = string.Empty;

        public bool Approve { get; set; }

        public string? ResponseNote { get; set; }

        public DateTime? EstimatedReadyDate { get; set; }
    }
}
