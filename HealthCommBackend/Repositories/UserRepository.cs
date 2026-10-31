using HealthCommBackend.Data;

namespace HealthCommBackend.Repositories
{
    public class UserRepository 
    {
        private BackendDBContext _context;

        public UserRepository(BackendDBContext context)
        {
            _context = context;
        }


    }
}
