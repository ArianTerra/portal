﻿using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.ManyToManyTables;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Core.Entities.Progress;
using Microsoft.EntityFrameworkCore;

namespace EducationPortalConsole.DataAccess.DataContext;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Material> Materials { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<ArticleMaterial> ArticleMaterials { get; set; }

    public DbSet<BookMaterial> BookMaterials { get; set; }

    public DbSet<VideoMaterial> VideoMaterials { get; set; }

    public DbSet<CourseProgress> CourseProgresses { get; set; }

    public DbSet<MaterialProgress> MaterialProgresses { get; set; }

    public DbSet<SkillProgress> SkillProgresses { get; set; }

    public DbSet<BookMaterialBookAuthor> BookMaterialBookAuthor { get; set; }

    public DbSet<CourseMaterial> CourseMaterial { get; set; }

    public DbSet<CourseSkill> CourseSkill { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
            Database=EducationPortalConsole.DataAccess.DataContext.DatabaseContext");
    }

    protected override void OnModelCreating([NotNull] ModelBuilder modelBuilder)
    {
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
            .HasOne(cp => cp.User)
            .WithMany(u => u.CourseProgresses)
            .HasForeignKey(cp => cp.UserId);

        modelBuilder.Entity<MaterialProgress>()
            .HasKey(cp => new { cp.MaterialId, cp.UserId });
        modelBuilder.Entity<MaterialProgress>()
            .HasOne(cp => cp.Material)
            .WithMany(c => c.MaterialProgresses)
            .HasForeignKey(cp => cp.MaterialId);
        modelBuilder.Entity<MaterialProgress>()
            .HasOne(cp => cp.User)
            .WithMany(u => u.MaterialProgresses)
            .HasForeignKey(cp => cp.UserId);

        modelBuilder.Entity<SkillProgress>()
            .HasKey(cp => new { cp.SkillId, cp.UserId });
        modelBuilder.Entity<SkillProgress>()
            .HasOne(cp => cp.Skill)
            .WithMany(c => c.SkillProgresses)
            .HasForeignKey(cp => cp.SkillId);
        modelBuilder.Entity<SkillProgress>()
            .HasOne(cp => cp.User)
            .WithMany(u => u.SkillProgresses)
            .HasForeignKey(cp => cp.UserId);

        modelBuilder.Entity<BookMaterialBookAuthor>()
            .HasKey(cp => new { cp.BookMaterialId, cp.BookAuthorId });
        modelBuilder.Entity<BookMaterialBookAuthor>()
            .HasOne(cp => cp.BookMaterial)
            .WithMany(c => c.BookMaterialBookAuthors)
            .HasForeignKey(cp => cp.BookMaterialId);
        modelBuilder.Entity<BookMaterialBookAuthor>()
            .HasOne(cp => cp.BookAuthor)
            .WithMany(u => u.BookMaterialBookAuthors)
            .HasForeignKey(cp => cp.BookAuthorId);

        modelBuilder.Entity<CourseMaterial>()
            .HasKey(cp => new { cp.CourseId, cp.MaterialId });
        modelBuilder.Entity<CourseMaterial>()
            .HasOne(cp => cp.Course)
            .WithMany(c => c.CourseMaterials)
            .HasForeignKey(cp => cp.CourseId);
        modelBuilder.Entity<CourseMaterial>()
            .HasOne(cp => cp.Material)
            .WithMany(u => u.CourseMaterials)
            .HasForeignKey(cp => cp.CourseId);

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