using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using EducationPortal.DataAccess.DomainModels.JoinEntities;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.DomainModels.Progress;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.DataAccess;

public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public DbSet<Course> Courses { get; set; }

    public DbSet<Material> Materials { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<ArticleMaterial> ArticleMaterials { get; set; }

    public DbSet<BookMaterial> BookMaterials { get; set; }

    public DbSet<VideoMaterial> VideoMaterials { get; set; }

    public DbSet<CourseProgress> CourseProgresses { get; set; }

    public DbSet<MaterialProgress> MaterialProgresses { get; set; }

    public DbSet<SkillProgress> SkillProgresses { get; set; }

    public DbSet<BookAuthorBookMaterial> BookAuthorBookMaterial { get; set; }

    public DbSet<CourseMaterial> CourseMaterial { get; set; }

    public DbSet<CourseSkill> CourseSkill { get; set; }

    public DbSet<VideoQuality> VideoQualities { get; set; }

    public DbSet<BookFormat> BookFormats { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Material>()
            .HasDiscriminator<string>(x => x.Discriminator);

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

        modelBuilder.Entity<CourseProgress>()
            .HasKey(cp => new { cp.CourseId, cp.UserId });
        modelBuilder.Entity<CourseProgress>()
            .HasOne(cp => cp.Course)
            .WithMany(c => c.CourseProgresses)
            .HasForeignKey(cp => cp.CourseId);
        modelBuilder.Entity<CourseProgress>()
            .HasOne(cp => cp.ApplicationUser)
            .WithMany(u => u.CourseProgresses)
            .HasForeignKey(cp => cp.UserId);

        modelBuilder.Entity<MaterialProgress>()
            .HasKey(cp => new { cp.MaterialId, cp.UserId });
        modelBuilder.Entity<MaterialProgress>()
            .HasOne(cp => cp.Material)
            .WithMany(c => c.MaterialProgresses)
            .HasForeignKey(cp => cp.MaterialId);
        modelBuilder.Entity<MaterialProgress>()
            .HasOne(cp => cp.ApplicationUser)
            .WithMany(u => u.MaterialProgresses)
            .HasForeignKey(cp => cp.UserId);

        modelBuilder.Entity<SkillProgress>()
            .HasKey(cp => new { cp.SkillId, cp.UserId });
        modelBuilder.Entity<SkillProgress>()
            .HasOne(cp => cp.Skill)
            .WithMany(c => c.SkillProgresses)
            .HasForeignKey(cp => cp.SkillId);
        modelBuilder.Entity<SkillProgress>()
            .HasOne(cp => cp.ApplicationUser)
            .WithMany(u => u.SkillProgresses)
            .HasForeignKey(cp => cp.UserId);

        modelBuilder.Entity<BookAuthorBookMaterial>()
            .HasKey(cp => new { cp.BookAuthorId, cp.BookMaterialId});
        modelBuilder.Entity<BookAuthorBookMaterial>()
            .HasOne(cp => cp.BookAuthor)
            .WithMany(u => u.BookAuthorBookMaterial)
            .HasForeignKey(cp => cp.BookAuthorId);
        modelBuilder.Entity<BookAuthorBookMaterial>()
            .HasOne(cp => cp.BookMaterial)
            .WithMany(c => c.BookAuthorBookMaterial)
            .HasForeignKey(cp => cp.BookMaterialId);

        modelBuilder.Entity<CourseMaterial>()
            .HasKey(cm => new { cm.CourseId, cm.MaterialId });
        modelBuilder.Entity<CourseMaterial>()
            .HasOne(cm => cm.Course)
            .WithMany(c => c.CourseMaterials)
            .HasForeignKey(cm => cm.CourseId);
        modelBuilder.Entity<CourseMaterial>()
            .HasOne(cm => cm.Material)
            .WithMany(m => m.CourseMaterials)
            .HasForeignKey(cm => cm.MaterialId);

        modelBuilder.Entity<CourseSkill>()
            .HasKey(cp => new { cp.CourseId, cp.SkillId });
        modelBuilder.Entity<CourseSkill>()
            .HasOne(cp => cp.Course)
            .WithMany(c => c.CourseSkills)
            .HasForeignKey(cp => cp.CourseId);
        modelBuilder.Entity<CourseSkill>()
            .HasOne(cp => cp.Skill)
            .WithMany(u => u.CourseSkills)
            .HasForeignKey(cp => cp.SkillId);
    }
}