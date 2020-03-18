using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// Db context
    /// </summary>
    public sealed class ServiceDbContext : DbContext
    {
        /// <summary>
        /// Beds 
        /// </summary>
        public DbSet<Bed> Beds { get; set; }


        /// <summary>
        /// Schedules for individual Days
        /// </summary>
        public DbSet<DailySchedule> DailySchedules { get; set; }


        /// <summary>
        /// Nurses
        /// </summary>
        public DbSet<Nurse> Nurses { get; set; }


        /// <summary>
        /// The saved ODFs
        /// </summary>
        public DbSet<ODF> ODFs { get; set; }


        /// <summary>
        /// Administrations of ODFs
        /// </summary>
        public DbSet<ODFAdministration> ODFAdministrations { get; set; }

     
        /// <summary>
        /// The patients in the database
        /// </summary>
        public DbSet<Patient> Patients { get; set; }


        /// <summary>
        /// The prescriptions 
        /// </summary>
        public DbSet<Prescription> Prescriptions { get; set; }



        /// <summary>
        /// Print Jobs
        /// </summary>
        public DbSet<PrintJob> PrintJobs { get; set; }


        /// <summary>
        /// Rooms
        /// </summary>
        public DbSet<Room> Rooms { get; set; }


        /// <summary>
        /// Wards
        /// </summary>
        public DbSet<Ward> Wards { get; set; }


        /// <summary>
        /// Weekly Prescription Schedules
        /// </summary>
        public DbSet<WeeklyPrescriptionSchedule> WeeklyPrescriptionSchedules { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One patient can have many prescriptions
            modelBuilder.Entity<Prescription>()
                .HasOne(P => P.Patient)
                .WithMany(P => P.Prescriptions)
                .HasForeignKey(P => P.PatientId);

            // One Week schedule consists of 7 daily schedules
            modelBuilder.Entity<DailySchedule>()
                .HasOne(D => D.WeeklyPrescriptionSchedule)
                .WithMany(W => W.DaySchedules)
                .HasForeignKey(D => D.WeeklyPrescriptionScheduleId);

            // One Daily Schedule has many printjobs
            modelBuilder.Entity<PrintJob>()
                .HasOne(P => P.DailySchedule)
                .WithMany(D => D.PrintJobs)
                .HasForeignKey(P => P.DailyScheduleId);

            // One Prescription can have many ODF
            modelBuilder.Entity<ODF>()
                .HasOne(ODF => ODF.Prescription)
                .WithMany(P => P.ODFs)
                .HasForeignKey(ODF => ODF.PrescriptionId);

            // One print job (batch) can have many ODF's
            modelBuilder.Entity<ODF>()
                .HasOne(ODF => ODF.PrintJob)
                .WithMany(Job => Job.ODFs)
                .HasForeignKey(ODF => ODF.PrintJobId);

            // ODFs can have an associate administration
            modelBuilder.Entity<ODF>()
                .HasOne(ODF => ODF.ODFAdministration)
                .WithOne(A => A.ODF)
                .HasForeignKey<ODF>(ODF => ODF.ODFAdministrationId);

            // ODFs can have an associate administration
            modelBuilder.Entity<ODFAdministration>()
                .HasOne(A => A.ODF)
                .WithOne(ODF => ODF.ODFAdministration)
                .HasForeignKey<ODFAdministration>(A=> A.ODFId);

            // An ODFAdminstration is administered by a nurse
            modelBuilder.Entity<ODFAdministration>()
                .HasOne(ODFA => ODFA.Nurse)
                .WithMany(N => N.Administrations)
                .HasForeignKey(ODFA => ODFA.NurseId);

            // A Ward consist of rooms
            modelBuilder.Entity<Room>()
                .HasOne(R => R.Ward)
                .WithMany(W => W.Rooms)
                .HasForeignKey(R => R.WardId);

            // A Room consists of beds
            modelBuilder.Entity<Bed>()
                .HasOne(B => B.Room)
                .WithMany(R => R.Beds)
                .HasForeignKey(B => B.RoomId);

            // A patient can have a bed
            modelBuilder.Entity<Bed>()
                .HasOne(B => B.Patient)
                .WithOne(P => P.Bed)
                .HasForeignKey<Bed>(B => B.PatientId);
        }
    }
}
