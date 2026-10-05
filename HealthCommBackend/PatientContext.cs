using HealthCommBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCommBackend
{
    public class PatientContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public string DbPath { get; }

        public PatientContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "healthcomm.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
