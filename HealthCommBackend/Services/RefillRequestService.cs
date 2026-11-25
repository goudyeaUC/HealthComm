using HealthCommBackend.Data;
using HealthCommBackend.Models;
using HealthCommShared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HealthCommBackend.Services
{
    public class RefillRequestService
    {
        private readonly BackendDBContext _context;

        public RefillRequestService(BackendDBContext context)
        {
            _context = context;
        }

        private IQueryable<RefillRequest> WithDetails() =>
            _context.RefillRequests
                .Include(r => r.Prescription).ThenInclude(p => p.Patient)
                .Include(r => r.Prescription).ThenInclude(p => p.Prescriber);

        public async Task<List<RefillRequest>> GetByPatientIdAsync(string patientId) =>
            await WithDetails()
                .Where(r => r.Prescription.Patient.Id == patientId)
                .OrderByDescending(r => r.RequestedAt)
                .ToListAsync();

        public async Task<List<RefillRequest>> GetByPrescriberIdAsync(string prescriberId, string? status) =>
            await WithDetails()
                .Where(r => r.Prescription.Prescriber.Id == prescriberId
                         && (status == null || r.Status == status))
                .OrderByDescending(r => r.RequestedAt)
                .ToListAsync();

        // A patient may only request a refill on their own prescription, and only
        // when it is active, unexpired, has refills left and has no open request.
        public async Task<RefillRequest> CreateAsync(CreateRefillRequest request)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.RefillRequests)
                .FirstOrDefaultAsync(p => p.Id == request.PrescriptionId)
                ?? throw new ArgumentException("Prescription not found.");

            if (prescription.Patient.Id != request.PatientId)
            {
                throw new ArgumentException("This prescription does not belong to the patient.");
            }

            if (!string.Equals(prescription.ActiveStatus, "active", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Only active prescriptions can be refilled.");
            }

            if (prescription.ExpirationDate <= DateTime.UtcNow)
            {
                throw new ArgumentException("This prescription has expired.");
            }

            if (prescription.RefillsRemaining <= 0)
            {
                throw new ArgumentException("No refills remaining. Contact your provider.");
            }

            if (prescription.RefillRequests.Any(r => r.Status == RefillStatus.Requested))
            {
                throw new ArgumentException("A refill request is already pending for this prescription.");
            }

            var refill = new RefillRequest
            {
                PrescriptionId = prescription.Id,
                PatientNote = request.PatientNote,
                RequestedAt = DateTime.UtcNow,
                Status = RefillStatus.Requested
            };

            _context.RefillRequests.Add(refill);
            await _context.SaveChangesAsync();
            return await WithDetails().FirstAsync(r => r.Id == refill.Id);
        }

        // Only the prescriber of the underlying prescription may approve or deny.
        // Approval consumes one refill.
        public async Task<RefillRequest?> ReviewAsync(int id, ReviewRefillRequest review)
        {
            var refill = await WithDetails().FirstOrDefaultAsync(r => r.Id == id);
            if (refill == null)
            {
                return null;
            }

            if (refill.Prescription.Prescriber.Id != review.ReviewerId)
            {
                throw new ArgumentException("Only the prescribing practitioner can review this request.");
            }

            if (refill.Status != RefillStatus.Requested)
            {
                throw new ArgumentException("This request has already been reviewed.");
            }

            if (review.Approve)
            {
                if (refill.Prescription.RefillsRemaining <= 0)
                {
                    throw new ArgumentException("No refills remaining on this prescription.");
                }

                refill.Prescription.RefillsRemaining--;
                refill.Status = RefillStatus.Approved;
                refill.EstimatedReadyDate = review.EstimatedReadyDate ?? DateTime.UtcNow.AddDays(2);
            }
            else
            {
                refill.Status = RefillStatus.Denied;
            }

            refill.ResponseNote = review.ResponseNote;
            refill.ReviewedAt = DateTime.UtcNow;
            refill.ReviewedById = review.ReviewerId;

            await _context.SaveChangesAsync();
            return refill;
        }

        public static RefillRequestDTO ToDto(RefillRequest r) => new()
        {
            Id = r.Id,
            PrescriptionId = r.PrescriptionId,
            MedicationName = r.Prescription.MedicationName,
            PatientId = r.Prescription.Patient.Id,
            PatientName = $"{r.Prescription.Patient.FirstName} {r.Prescription.Patient.LastName}".Trim(),
            RequestedAt = r.RequestedAt,
            Status = r.Status,
            PatientNote = r.PatientNote,
            ResponseNote = r.ResponseNote,
            ReviewedAt = r.ReviewedAt,
            EstimatedReadyDate = r.EstimatedReadyDate
        };
    }
}
