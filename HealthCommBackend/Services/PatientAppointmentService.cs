using HealthCommBackend.Data;
using HealthCommBackend.Models;
using HealthCommShared.DTOs;
using Hl7.Fhir.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthCommBackend.Services
{
    public class PatientAppointmentService
    {
        private readonly BackendDBContext _context;

        public PatientAppointmentService(BackendDBContext context)
        {
            _context = context;
        }

        public async Task<List<PatientAppointment>> GetAllAsync()
        {
            return await _context.Set<PatientAppointment>()
                .AsNoTracking()
                .OrderBy(a => a.StartDateTime)
                .ToListAsync();
        }

        public async Task<PatientAppointment?> GetByIdAsync(int id)
        {
            return await _context.Set<PatientAppointment>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<PractitionerProfile>> GetAllPractitionersAsync()
        {
            return await _context.Set<PractitionerProfile>()
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<PatientProfile>> GetAllPatientsAsync()
        {
            return await _context.Set<PatientProfile>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PatientAppointment> CreatePatientAppointmentAsync(
    CreatePatientAppointmentRequest request)
        {
            var patientExists = await _context.Set<PatientProfile>()
                .AnyAsync(p => p.Id == request.PatientProfileId);

            if (!patientExists)
                throw new ArgumentException("Patient ID was not found.");

            var practitionerExists =
                await _context.Set<PractitionerProfile>()
                    .AnyAsync(p => p.Id == request.PractitionerProfileId);

            if (!practitionerExists)
                throw new ArgumentException("Practitioner ID was not found.");

            var appointment = new PatientAppointment
            {
                PatientProfileId = request.PatientProfileId,
                PractitionerProfileId = request.PractitionerProfileId,
                StartDateTime = request.StartDateTime,
              //  EndDateTime = request.EndDateTime,
                VisitType = request.VisitType,
              //  Description = request.Description,
               // Status = "Scheduled",
              //  CreatedAt = DateTime.UtcNow
            };

            _context.Set<PatientAppointment>().Add(appointment);

            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<List<AppointmentDTO>> GetAppointmentsByPatientAndPractitionerAsync(
            int patientId,
            int practitionerId)
                {
            
            List<PatientAppointment> list = await _context.Set<PatientAppointment>()
                        .AsNoTracking()
                        .Where(a =>
                            a.PatientProfileId == patientId &&
                            a.PractitionerProfileId == practitionerId)
                        .OrderBy(a => a.StartDateTime)
                        .ToListAsync();

            List<AppointmentDTO> returnList = ConvertToAppointmentDTO(list);
            return returnList;
        }

        public List<AppointmentDTO> ConvertToAppointmentDTO(
   List<PatientAppointment> appointments)
        {
            return appointments.Select(a => new AppointmentDTO
            {
                Id = a.Id,
                PatientProfileId = a.PatientProfileId,
                PractitionerProfileId = a.PractitionerProfileId,
                StartDateTime = a.StartDateTime,
                VisitType = a.VisitType
            }).ToList();
        }
    }
}
