using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    public sealed class ServiceDbContext : DbContext
    {
     

        public DbSet<Patient> Patients { get; set; }


        public DbSet<Prescription> Prescriptions { get; set; }


        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription>()
                .HasOne(P => P.Patient)
                .WithMany(P => P.Prescriptions)
                .HasForeignKey(P => P.PatientId);
        }
    }
}
