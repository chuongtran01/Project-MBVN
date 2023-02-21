using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HospitalManagementSystem.Models;
public partial class MBVNContext : IdentityDbContext<Patient>

{

    public MBVNContext(DbContextOptions<MBVNContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; } = null!;
    public virtual DbSet<Appointment> Appointments { get; set; } = null!;
    public virtual DbSet<ContactU> ContactUs { get; set; } = null!;
    public virtual DbSet<Department> Departments { get; set; } = null!;
    public virtual DbSet<Doctor> Doctors { get; set; } = null!;
    public virtual DbSet<DoctorsLog> DoctorsLogs { get; set; } = null!;
    public virtual DbSet<Hospital> Hospitals { get; set; } = null!;
    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; } = null!;
    public virtual DbSet<Medicine> Medicines { get; set; } = null!;
    public virtual DbSet<Nurse> Nurses { get; set; } = null!;
    public virtual DbSet<Page> Pages { get; set; } = null!;
    public virtual DbSet<Patient> Patients { get; set; } = null!;
    public virtual DbSet<Prescription> Prescriptions { get; set; } = null!;
    public virtual DbSet<UserLog> UserLogs { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AspNetUserLogin>().HasKey(c => c.LoginProvider);
        modelBuilder.Entity<AspNetUserToken>().HasKey(c => new {c.LoginProvider, c.UserId });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Server=tcp:hospitalserver.database.windows.net,1433;Initial Catalog=hospitaldb;Persist Security Info=False;User ID=hospitaladmin;Password=admin@Admin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
