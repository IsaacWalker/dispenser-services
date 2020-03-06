﻿using Microsoft.EntityFrameworkCore;
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
        /// Prescription Times
        /// </summary>
        public DbSet<PrescriptionTime> PrescriptionTimes { get; set; }


        /// <summary>
        /// Print Jobs
        /// </summary>
        public DbSet<PrintJob> PrintJobs { get; set; }


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

            // One Prescription can have many times
            modelBuilder.Entity<PrescriptionTime>()
                .HasOne(PT => PT.Prescription)
                .WithMany(P => P.Times)
                .HasForeignKey(PT => PT.PrescriptionId);

            // One PrescriptionTime can be printed many times as an ODF
            modelBuilder.Entity<ODF>()
                .HasOne(ODF => ODF.PrescriptionTime)
                .WithMany(PT => PT.ODFs)
                .HasForeignKey(ODF => ODF.PrescriptionTimeId);

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

            // An ODFAdminstration is administered by a nurse
            modelBuilder.Entity<ODFAdministration>()
                .HasOne(ODFA => ODFA.Nurse)
                .WithMany(N => N.Administrations)
                .HasForeignKey(ODFA => ODFA.NurseId);
        }
    }
}
