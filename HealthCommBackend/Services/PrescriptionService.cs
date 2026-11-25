        using Microsoft.AspNetCore.Identity;
using HealthCommBackend.Models;
using HealthCommBackend.Data;
using HealthCommShared.DTOs;
using HealthCommBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using Hl7.Fhir.Model;

namespace HealthCommBackend.Services

{
    public class PrescriptionService
    {
        private readonly BackendDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PrescriptionService(BackendDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Prescription?> GetPrescriptionByIdAsync(int prescriptionId)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                    .ThenInclude(u => u.PatientProfile  )
                .Include(p => p.Prescriber)
                    .ThenInclude(u => u.PractitionerProfile)
                .Include(p => p.RefillRequests)
                .FirstOrDefaultAsync(p => p.Id == prescriptionId);
        }

        public async Task<List<Prescription>> GetPrescriptionsByPatientIdAsync(string patientId)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                    .ThenInclude(u => u.PatientProfile)
                .Include(p => p.Prescriber)
                    .ThenInclude(u => u.PractitionerProfile)
                .Include(p => p.RefillRequests)
                .Where(p => p.Patient.Id == patientId)
                .ToListAsync();
        }

        public async Task<List<Prescription>> GetPrescriptionsByPrescriberIdAsync(string prescriberId)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                    .ThenInclude(u => u.PatientProfile)
                .Include(p => p.Prescriber)
                    .ThenInclude(u => u.PractitionerProfile)
                .Include(p => p.RefillRequests)
                .Where(p => p.Prescriber.Id == prescriberId)
                .ToListAsync();
        }

        public async Task<List<Prescription>> GetPrescriptionsByPatientEmailAsync(string email)
        {
            var normalized = email.ToUpperInvariant();
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Prescriber)
                .Include(p => p.RefillRequests)
                .Where(p => p.Patient.NormalizedEmail == normalized)
                .OrderByDescending(p => p.DatePrescribed)
                .ToListAsync();
        }

        public async Task<Prescription> CreatePrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<Prescription> CreatePrescriptionAsync(CreatePrescriptionRequest request)
        {
            var patient = await _userManager.FindByIdAsync(request.PatientId)
                ?? throw new ArgumentException("Patient not found.");
            var prescriber = await _userManager.FindByIdAsync(request.PrescriberId)
                ?? throw new ArgumentException("Prescriber not found.");

            var prescription = new Prescription
            {
                Patient = patient,
                Prescriber = prescriber,
                MedicationName = request.MedicationName,
                Instructions = request.Instructions,
                SNOMEDDrugCode = request.SNOMEDDrugCode ?? string.Empty,
                ReasonForPrescription = request.ReasonForPrescription ?? string.Empty,
                SNOMEDReasonCode = request.SNOMEDReasonCode ?? string.Empty,
                ExpirationDate = request.ExpirationDate ?? DateTime.UtcNow.AddDays(30),
                RefillsRemaining = request.RefillsRemaining,
                ActiveStatus = "active",
                LastSyncedAt = DateTime.UtcNow
            };

            return await CreatePrescriptionAsync(prescription);
        }

        // Updates only the editable scalar fields; patient and prescriber are never reassigned.
        public async Task<Prescription?> UpdatePrescriptionAsync(int id, PrescriptionDTO dto)
        {
            var existing = await _context.Prescriptions.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            existing.MedicationName = dto.MedicationName;
            existing.Instructions = dto.Instructions;
            existing.SNOMEDDrugCode = dto.SNOMEDDrugCode ?? existing.SNOMEDDrugCode;
            existing.ReasonForPrescription = dto.ReasonForPrescription ?? existing.ReasonForPrescription;
            existing.SNOMEDReasonCode = dto.SNOMEDReasonCode ?? existing.SNOMEDReasonCode;
            existing.ExpirationDate = dto.ExpirationDate;
            existing.DateFilled = dto.DateFilled;
            existing.ActiveStatus = dto.ActiveStatus;
            existing.RefillsRemaining = dto.RefillsRemaining;
            existing.LastSyncedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public static PrescriptionDTO ToDto(Prescription p)
        {
            var latestRefill = p.RefillRequests
                .OrderByDescending(r => r.RequestedAt)
                .FirstOrDefault();

            var dto = BuildDto(p);
            dto.RefillStatus = latestRefill?.Status;
            dto.RefillEstimatedReadyDate = latestRefill?.EstimatedReadyDate;
            return dto;
        }

        private static PrescriptionDTO BuildDto(Prescription p) => new()
        {
            Id = p.Id,
            PatientId = p.Patient?.Id ?? string.Empty,
            PrescriberId = p.Prescriber?.Id ?? string.Empty,
            PrescriberName = p.Prescriber == null
                ? string.Empty
                : $"{p.Prescriber.FirstName} {p.Prescriber.LastName}".Trim(),
            MedicationName = p.MedicationName,
            Instructions = p.Instructions,
            SNOMEDDrugCode = p.SNOMEDDrugCode,
            ReasonForPrescription = p.ReasonForPrescription,
            SNOMEDReasonCode = p.SNOMEDReasonCode,
            DatePrescribed = p.DatePrescribed,
            ExpirationDate = p.ExpirationDate,
            DateFilled = p.DateFilled,
            ActiveStatus = p.ActiveStatus,
            RefillsRemaining = p.RefillsRemaining
        };

        public async Task<bool> DeletePrescriptionAsync(int prescriptionId)
        {
            var prescription = await _context.Prescriptions.FindAsync(prescriptionId);
            if (prescription == null)
            {
                return false;
            }
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
