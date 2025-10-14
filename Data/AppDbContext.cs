using Microsoft.EntityFrameworkCore;
using Prueba_desempeno_csharp.Models;

namespace Prueba_desempeno_csharp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Medic> Medics { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Medic)
                .WithMany(m => m.Appointments)
                .HasForeignKey(a => a.MedicId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);
        }
    }
}