using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmployeeManager.WebAPI.Models
{
    public partial class EmployeeManagerStoreContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public EmployeeManagerStoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EmployeeManagerStoreContext(DbContextOptions<EmployeeManagerStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeSalaries> EmployeeSalaries { get; set; }
        public virtual DbSet<EmployeesJobCategories> EmployeesJobCategories { get; set; }
        public virtual DbSet<JobCategory> JobCategory { get; set; }
        public virtual DbSet<Salary> Salary { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=.;Database=EmployeeManager;Integrated Security=True;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("EmployeeWebAPIConn"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Street1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Street2).HasMaxLength(50);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_Country");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_1_Country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ExitedDate).HasColumnType("date");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasComment("");

                entity.Property(e => e.JoinedDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Employee_Address");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Employee_Country");

                entity.HasOne(d => d.Superior)
                    .WithMany(p => p.InverseSuperior)
                    .HasForeignKey(d => d.SuperiorId)
                    .HasConstraintName("FK_Employee_Employee");
            });

            modelBuilder.Entity<EmployeeSalaries>(entity =>
            {
                entity.HasIndex(e => new { e.EmployeeId, e.To })
                    .HasName("IX_EmployeeSalaries")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.From).HasColumnType("date");

                entity.Property(e => e.To).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSalaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeSalaries_Employee");
            });

            modelBuilder.Entity<EmployeesJobCategories>(entity =>
            {
                entity.HasIndex(e => new { e.EmployeeId, e.JobCategoryId })
                    .HasName("IX_EmployeesJobCategories")
                    .IsUnique();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeesJobCategories)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeesJobCategories_Employee");

                entity.HasOne(d => d.JobCategory)
                    .WithMany(p => p.EmployeesJobCategories)
                    .HasForeignKey(d => d.JobCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeesJobCategories_JobCategory");
            });

            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.From).HasColumnType("date");

                entity.Property(e => e.To).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
