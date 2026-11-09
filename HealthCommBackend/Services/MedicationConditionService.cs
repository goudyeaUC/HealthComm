using HealthCommBackend.Data;
using HealthCommBackend.Models;
using HealthCommShared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HealthCommBackend.Services
{
    public class MedicationConditionService
    {
        private readonly BackendDBContext _context;

        public async Task<List<MedicalConditionDTO>> GetAllAsync()
        {
            List<MedicationCondition> list = await _context.Set<MedicationCondition>()
                .AsNoTracking()
                .OrderBy(m => m.ConditionName)
                .ToListAsync();

            List<MedicalConditionDTO> dtoList = convertToMedicationDTO(list);
            return dtoList;
        }

        public MedicationConditionService(BackendDBContext context)
        {
            _context = context;
        }

        public List<MedicalConditionDTO> convertToMedicationDTO(
  List<MedicationCondition> conditions)
        {
            return conditions.Select(a => new MedicalConditionDTO
            {
                ConditionId = a.ConditionId,
                PatientId = a.PatientId,
                ConditionName = a.ConditionName,
                Category = a.Category,
                ClinicalStatus = a.ClinicalStatus
            }).ToList();
        }
    }
}
