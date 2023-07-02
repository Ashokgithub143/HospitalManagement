using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Models
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Email)
                        .IsUnique();
            modelBuilder.Entity<Doctor>().Property(i => i.DoctorId).ValueGeneratedNever();
            modelBuilder.Entity<Patient>().Property(i => i.PatientId).ValueGeneratedNever();
        }
    }
}
