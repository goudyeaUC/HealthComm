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
                .FirstOrDefaultAsync(p => p.Id == prescriptionId);
        }

        public async Task<List<Prescription>> GetPrescriptionsByPatientIdAsync(string patientId)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                    .ThenInclude(u => u.PatientProfile)
                .Include(p => p.Prescriber)
                    .ThenInclude(u => u.PractitionerProfile)
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
                .Where(p => p.Prescriber.Id == prescriberId)
                .ToListAsync();
        }

        public async Task<Prescription> CreatePrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<Prescription> UpdatePrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

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
