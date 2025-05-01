using Microsoft.EntityFrameworkCore;
using Polyclinic.Models;

namespace Polyclinic
{
    public class ClinicApiContext : DbContext
    {
        public ClinicApiContext(DbContextOptions<ClinicApiContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalCard> MedicalCards { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Пользователи
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Пациент -> Пользователь
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.IdUser)
                .OnDelete(DeleteBehavior.NoAction);

            // Заявка на приём -> Пациент и Врач
            modelBuilder.Entity<AppointmentRequest>()
                .HasOne(ar => ar.Patient)
                .WithMany()
                .HasForeignKey(ar => ar.IdPatient)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AppointmentRequest>()
                .HasOne(ar => ar.Doctor)
                .WithMany()
                .HasForeignKey(ar => ar.IdDoctor)
                .OnDelete(DeleteBehavior.NoAction);

            // Медицинская карта -> Пациент и Врач
            modelBuilder.Entity<MedicalCard>()
                .HasOne(mc => mc.Patient)
                .WithMany()
                .HasForeignKey(mc => mc.IdPatient)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicalCard>()
                .HasOne(mc => mc.Doctor)
                .WithMany()
                .HasForeignKey(mc => mc.IdDoctor)
                .OnDelete(DeleteBehavior.NoAction);

            // Расписание врача -> Врач
            modelBuilder.Entity<DoctorSchedule>()
                .HasOne(ds => ds.Doctor)
                .WithMany()
                .HasForeignKey(ds => ds.IdDoctor)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}