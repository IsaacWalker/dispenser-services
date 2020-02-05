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
        /// The patients in the database
        /// </summary>
        public DbSet<Patient> Patients { get; set; }


        /// <summary>
        /// The prescriptions 
        /// </summary>
        public DbSet<Prescription> Prescriptions { get; set; }


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
        }
    }
}
