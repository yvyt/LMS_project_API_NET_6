﻿// <auto-generated />
using System;
using CourseService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourseService.Migrations
{
    [DbContext(typeof(CourseContext))]
    partial class CourseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CourseService.Data.Classes", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Teacher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("CourseService.Data.Course", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("ApprovalAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("CourseService.Data.Documents", b =>
                {
                    b.Property<string>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("CourseService.Data.Lesson", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TopicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updateBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique();

                    b.HasIndex("TopicId");

                    b.HasIndex("TypeId");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("CourseService.Data.LessonAnswer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ContentAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonQuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updateBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LessonQuestionId");

                    b.ToTable("LessonAnswer");
                });

            modelBuilder.Entity("CourseService.Data.LessonQuestion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ContentQuestion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAnswer")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLike")
                        .HasColumnType("bit");

                    b.Property<string>("LessonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isFromTeacher")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("LessonQuestion");
                });

            modelBuilder.Entity("CourseService.Data.Resources", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LessonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updateBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique();

                    b.HasIndex("LessonId");

                    b.HasIndex("TypeId");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("CourseService.Data.StudentCourse", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("CourseService.Data.Topic", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("CourseService.Data.TypeFile", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeFile");
                });

            modelBuilder.Entity("CourseService.Data.Classes", b =>
                {
                    b.HasOne("CourseService.Data.Course", "Course")
                        .WithMany("Classes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("CourseService.Data.Lesson", b =>
                {
                    b.HasOne("CourseService.Data.Documents", "Document")
                        .WithOne("Lesson")
                        .HasForeignKey("CourseService.Data.Lesson", "DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseService.Data.Topic", "Topics")
                        .WithMany("Lessons")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseService.Data.TypeFile", "TypeFile")
                        .WithMany("Lessons")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Topics");

                    b.Navigation("TypeFile");
                });

            modelBuilder.Entity("CourseService.Data.LessonAnswer", b =>
                {
                    b.HasOne("CourseService.Data.LessonQuestion", "LessonQuestion")
                        .WithMany("LessonAnswers")
                        .HasForeignKey("LessonQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LessonQuestion");
                });

            modelBuilder.Entity("CourseService.Data.LessonQuestion", b =>
                {
                    b.HasOne("CourseService.Data.Lesson", "Lesson")
                        .WithMany("LessonQuestion")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("CourseService.Data.Resources", b =>
                {
                    b.HasOne("CourseService.Data.Documents", "Document")
                        .WithOne("Resources")
                        .HasForeignKey("CourseService.Data.Resources", "DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseService.Data.Lesson", "Lessons")
                        .WithMany("Resources")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseService.Data.TypeFile", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Lessons");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("CourseService.Data.StudentCourse", b =>
                {
                    b.HasOne("CourseService.Data.Classes", "Class")
                        .WithMany("StudentCourses")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("CourseService.Data.Topic", b =>
                {
                    b.HasOne("CourseService.Data.Classes", "Classes")
                        .WithMany("Topics")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classes");
                });

            modelBuilder.Entity("CourseService.Data.Classes", b =>
                {
                    b.Navigation("StudentCourses");

                    b.Navigation("Topics");
                });

            modelBuilder.Entity("CourseService.Data.Course", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("CourseService.Data.Documents", b =>
                {
                    b.Navigation("Lesson")
                        .IsRequired();

                    b.Navigation("Resources")
                        .IsRequired();
                });

            modelBuilder.Entity("CourseService.Data.Lesson", b =>
                {
                    b.Navigation("LessonQuestion");

                    b.Navigation("Resources");
                });

            modelBuilder.Entity("CourseService.Data.LessonQuestion", b =>
                {
                    b.Navigation("LessonAnswers");
                });

            modelBuilder.Entity("CourseService.Data.Topic", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("CourseService.Data.TypeFile", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
