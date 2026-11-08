using HealthCommBackend.Data;
using HealthCommBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCommBackend.Services
{
    public class MedicationRecordService
    {
        private readonly BackendDBContext _context;

        public async Task<List<MedicationRecord>> GetAllAsync()
        {
            return await _context.Set<MedicationRecord>()
                .AsNoTracking()
                .OrderBy(m => m.MedicationName)
                .ToListAsync();
        }

        public MedicationRecordService(BackendDBContext context)
        {
            _context = context;
        }
    }
}
