using Kolokwium.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Data;

public class AppDbContext : DbContext
{
    
    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<TreeSpecies> TreeSpecies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SeedlingBatch> SeedlingBatches { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }

    
    public AppDbContext(DbContextOptions options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nursery>(e =>
        {
            e.ToTable("Nursery");
            e.HasKey(n => n.NurseryId);
            e.Property(n => n.Name).IsRequired().HasMaxLength(100);
            e.Property(n => n.EstablishedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TreeSpecies>(e =>
        {
            e.ToTable("Tree_Species");
            e.HasKey(t => t.SpeciesId);
            e.Property(t => t.LatinName).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(e =>
        {
            e.ToTable("Employee");
            e.HasKey(em => em.EmployeeId);
            e.Property(em => em.FirstName).IsRequired().HasMaxLength(100);
            e.Property(em => em.LastName).IsRequired().HasMaxLength(100);
            e.Property(em => em.HireDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SeedlingBatch>(e =>
        {
            e.ToTable("Seedling_Batch");
            e.HasKey(sb => sb.BatchId);
            e.Property(sb => sb.SownDate).HasColumnType("datetime");
            e.Property(sb => sb.ReadyDate).HasColumnType("datetime");

            e.HasOne(sb => sb.Nursery)
                .WithMany(n => n.SeedlingBatches)
                .HasForeignKey(sb => sb.NurseryId);

            e.HasOne(sb => sb.Species)
                .WithMany(t => t.SeedlingBatches)
                .HasForeignKey(sb => sb.SpeciesId);
        });

        modelBuilder.Entity<Responsible>(e =>
        {
            e.ToTable("Responsible");
            e.HasKey(r => new { r.BatchId, r.EmployeeId });
            e.Property(r => r.Role).IsRequired().HasMaxLength(100);

            e.HasOne(r => r.Batch)
                .WithMany(sb => sb.Responsibilities)
                .HasForeignKey(r => r.BatchId);

            e.HasOne(r => r.Employee)
                .WithMany(em => em.Responsibilities)
                .HasForeignKey(r => r.EmployeeId);
        });
    }
    
    
}