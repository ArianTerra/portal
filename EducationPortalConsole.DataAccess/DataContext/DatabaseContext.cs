using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.Materials;
using Microsoft.EntityFrameworkCore;

namespace EducationPortalConsole.DataAccess.DataContext;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Material> Materials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
            Database=EducationPortalConsole.DataAccess.DataContext.DatabaseContext");
    }

    protected override void OnModelCreating([NotNull] ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>().HasDiscriminator();

        modelBuilder.Entity<ArticleMaterial>();
        modelBuilder.Entity<BookMaterial>();
        modelBuilder.Entity<VideoMaterial>();

        modelBuilder.Entity<Material>()
            .HasOne(x => x.CreatedBy)
            .WithMany(u => u.CreatedMaterials)
            .HasForeignKey(x => x.CreatedById);

        modelBuilder.Entity<Material>()
            .HasOne(x => x.UpdatedBy)
            .WithMany()
            .HasForeignKey(x => x.UpdatedById);

        modelBuilder.Entity<Course>()
            .HasOne(x => x.CreatedBy)
            .WithMany(u => u.CreatedCourses)
            .HasForeignKey(x => x.CreatedById);

        modelBuilder.Entity<Course>()
            .HasOne(x => x.UpdatedBy)
            .WithMany()
            .HasForeignKey(x => x.UpdatedById);
    }
}