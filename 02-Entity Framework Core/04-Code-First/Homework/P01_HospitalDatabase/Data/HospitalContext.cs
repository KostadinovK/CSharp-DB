﻿namespace P01_HospitalDatabase.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PatientMedicament> Prescriptions { get; set; }

        public HospitalContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Visitations)
                .WithOne(v => v.Patient)
                .HasForeignKey(v => v.PatientId);

            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Diagnoses)
                .WithOne(d => d.Patient)
                .HasForeignKey(d => d.PatientId);

            modelBuilder
                .Entity<Doctor>()
                .HasMany(d => d.Visitations)
                .WithOne(v => v.Doctor)
                .HasForeignKey(v => v.DoctorId);

            modelBuilder
                .Entity<PatientMedicament>()
                .HasKey(pm => new {pm.PatientId, pm.MedicamentId});

            modelBuilder
                .Entity<PatientMedicament>()
                .HasOne(pm => pm.Patient)
                .WithMany(pm => pm.Prescriptions)
                .HasForeignKey(pm => pm.PatientId);

            modelBuilder
                .Entity<PatientMedicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(pm => pm.Prescriptions)
                .HasForeignKey(pm => pm.MedicamentId);
        }
    }
}
