﻿// <auto-generated />
using System;
using EducationPortal.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EducationPortal.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.AdditionalModels.BookAuthor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("BookAuthor");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.AdditionalModels.BookFormat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookFormats");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.AdditionalModels.VideoQuality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VideoQualities");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.JoinEntities.BookAuthorBookMaterial", b =>
                {
                    b.Property<Guid>("BookAuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookMaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookAuthorId", "BookMaterialId");

                    b.HasIndex("BookMaterialId");

                    b.ToTable("BookAuthorBookMaterial");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.JoinEntities.CourseMaterial", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseId", "MaterialId");

                    b.HasIndex("MaterialId");

                    b.ToTable("CourseMaterial");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.JoinEntities.CourseSkill", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("CourseSkill");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Materials");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Material");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Progress.CourseProgress", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CourseProgresses");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Progress.MaterialProgress", b =>
                {
                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.HasKey("MaterialId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MaterialProgresses");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Progress.SkillProgress", b =>
                {
                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasKey("SkillId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("SkillProgresses");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHashSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Materials.ArticleMaterial", b =>
                {
                    b.HasBaseType("EducationPortal.DataAccess.DomainModels.Material");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ArticleMaterial");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Materials.BookMaterial", b =>
                {
                    b.HasBaseType("EducationPortal.DataAccess.DomainModels.Material");

                    b.Property<Guid>("BookFormatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Pages")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasIndex("BookFormatId");

                    b.HasDiscriminator().HasValue("BookMaterial");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Materials.VideoMaterial", b =>
                {
                    b.HasBaseType("EducationPortal.DataAccess.DomainModels.Material");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<Guid>("QualityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("QualityId");

                    b.HasDiscriminator().HasValue("VideoMaterial");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.AdditionalModels.BookAuthor", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Course", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "CreatedBy")
                        .WithMany("CreatedCourses")
                        .HasForeignKey("CreatedById");

                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.JoinEntities.BookAuthorBookMaterial", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.AdditionalModels.BookAuthor", "BookAuthor")
                        .WithMany("BookAuthorBookMaterial")
                        .HasForeignKey("BookAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationPortal.DataAccess.DomainModels.Materials.BookMaterial", "BookMaterial")
                        .WithMany("BookAuthorBookMaterial")
                        .HasForeignKey("BookMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookAuthor");

                    b.Navigation("BookMaterial");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.JoinEntities.CourseMaterial", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.Course", "Course")
                        .WithMany("CourseMaterials")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationPortal.DataAccess.DomainModels.Material", "Material")
                        .WithMany("CourseMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.JoinEntities.CourseSkill", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.Course", "Course")
                        .WithMany("CourseSkills")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationPortal.DataAccess.DomainModels.Skill", "Skill")
                        .WithMany("CourseSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Material", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "CreatedBy")
                        .WithMany("CreatedMaterials")
                        .HasForeignKey("CreatedById");

                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Progress.CourseProgress", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.Course", "Course")
                        .WithMany("CourseProgresses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "User")
                        .WithMany("CourseProgresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Progress.MaterialProgress", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.Material", "Material")
                        .WithMany("MaterialProgresses")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "User")
                        .WithMany("MaterialProgresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Progress.SkillProgress", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.Skill", "Skill")
                        .WithMany("SkillProgresses")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "User")
                        .WithMany("SkillProgresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Skill", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("EducationPortal.DataAccess.DomainModels.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Materials.BookMaterial", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.AdditionalModels.BookFormat", "BookFormat")
                        .WithMany("BookMaterials")
                        .HasForeignKey("BookFormatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookFormat");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Materials.VideoMaterial", b =>
                {
                    b.HasOne("EducationPortal.DataAccess.DomainModels.AdditionalModels.VideoQuality", "Quality")
                        .WithMany("VideoMaterials")
                        .HasForeignKey("QualityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quality");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.AdditionalModels.BookAuthor", b =>
                {
                    b.Navigation("BookAuthorBookMaterial");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.AdditionalModels.BookFormat", b =>
                {
                    b.Navigation("BookMaterials");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.AdditionalModels.VideoQuality", b =>
                {
                    b.Navigation("VideoMaterials");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Course", b =>
                {
                    b.Navigation("CourseMaterials");

                    b.Navigation("CourseProgresses");

                    b.Navigation("CourseSkills");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Material", b =>
                {
                    b.Navigation("CourseMaterials");

                    b.Navigation("MaterialProgresses");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Skill", b =>
                {
                    b.Navigation("CourseSkills");

                    b.Navigation("SkillProgresses");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.User", b =>
                {
                    b.Navigation("CourseProgresses");

                    b.Navigation("CreatedCourses");

                    b.Navigation("CreatedMaterials");

                    b.Navigation("MaterialProgresses");

                    b.Navigation("SkillProgresses");
                });

            modelBuilder.Entity("EducationPortal.DataAccess.DomainModels.Materials.BookMaterial", b =>
                {
                    b.Navigation("BookAuthorBookMaterial");
                });
#pragma warning restore 612, 618
        }
    }
}
