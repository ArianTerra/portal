using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core;
using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.DataAccess.DataContext;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Material> Materials { get; set; }

    protected override void OnModelCreating([NotNull] DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasOptional(e => e.CreatedBy)
            .WithMany(u => u.CreatedCourses)
            .HasForeignKey(e => e.CreatedById)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<Course>()
            .HasOptional(e => e.UpdatedBy)
            .WithMany()
            .HasForeignKey(e => e.UpdatedById)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<Material>()
            .HasOptional(m => m.CreatedBy)
            .WithMany(u => u.CreatedMaterials)
            .HasForeignKey(m => m.CreatedById)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<Material>()
            .HasOptional(m => m.UpdatedBy)
            .WithMany()
            .HasForeignKey(m => m.UpdatedById)
            .WillCascadeOnDelete(false);
    }
}