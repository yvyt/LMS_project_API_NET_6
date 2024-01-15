﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Data;

#nullable disable

namespace UserService.Migrations
{
    [DbContext(typeof(AppicationDbContext))]
    [Migration("20240112135030_seedData1")]
    partial class seedData1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UserService.Data.Permission", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Permission");

                    b.HasData(
                        new
                        {
                            Id = "0bda25ae-f034-43f3-b6fb-2f06d32870a4",
                            PermissionName = "Edit:Course"
                        },
                        new
                        {
                            Id = "95aeb353-24d8-4a8d-8b43-ae4ef3860a03",
                            PermissionName = "Delete:Course"
                        },
                        new
                        {
                            Id = "88dbd1f9-d943-41ed-a607-866def46d6a6",
                            PermissionName = "Create:Course"
                        },
                        new
                        {
                            Id = "80e94b56-160d-4450-a1d1-0f17f6fe1456",
                            PermissionName = "View:Course"
                        },
                        new
                        {
                            Id = "2a6f25ea-83aa-4c94-a1eb-0434671d5230",
                            PermissionName = "Edit:Class"
                        },
                        new
                        {
                            Id = "c5feb7be-eeea-46e3-b44c-588d37eff052",
                            PermissionName = "Delete:Class"
                        },
                        new
                        {
                            Id = "90065181-d7c7-4258-8c5b-237871454c5a",
                            PermissionName = "Create:Class"
                        },
                        new
                        {
                            Id = "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d",
                            PermissionName = "View:Class"
                        },
                        new
                        {
                            Id = "27a0b056-02b8-4cb7-a81c-26757569d9cf",
                            PermissionName = "Edit:PrivateFile"
                        },
                        new
                        {
                            Id = "2e8e852d-9e69-4f43-a9bf-deaead6485d1",
                            PermissionName = "Delete:PrivateFile"
                        },
                        new
                        {
                            Id = "a995e571-59cb-4f70-8aa9-2f637bb67e13",
                            PermissionName = "Create:PrivateFile"
                        },
                        new
                        {
                            Id = "ead1e55a-8213-442d-b8a4-a49fbeaad9a2",
                            PermissionName = "View:PrivateFile"
                        },
                        new
                        {
                            Id = "6d6ad73b-9a19-449f-a809-57794239e882",
                            PermissionName = "Edit:Topic"
                        },
                        new
                        {
                            Id = "1b183555-3067-4485-b563-65b34b29475f",
                            PermissionName = "Delete:Topic"
                        },
                        new
                        {
                            Id = "940748d7-c97d-4157-8104-fde614869fd9",
                            PermissionName = "Create:Topic"
                        },
                        new
                        {
                            Id = "ee255870-c765-4451-a019-a408d9e6ee27",
                            PermissionName = "View:Topic"
                        },
                        new
                        {
                            Id = "6a886d29-1f69-459e-80ca-5b37ef7f2fe6",
                            PermissionName = "Edit:Lesson"
                        },
                        new
                        {
                            Id = "9aafbadc-9401-4ebb-95b1-1adf64ecef00",
                            PermissionName = "Delete:Lesson"
                        },
                        new
                        {
                            Id = "e368179b-9797-4018-8867-72723cecc92e",
                            PermissionName = "Create:Lesson"
                        },
                        new
                        {
                            Id = "0db91594-128b-4aa5-bf04-77aa417143f4",
                            PermissionName = "View:Lesson"
                        },
                        new
                        {
                            Id = "c0030dfa-5bae-4dfa-8e7c-b367ed42a3db",
                            PermissionName = "Edit:Resource"
                        },
                        new
                        {
                            Id = "7b15cfff-a2ed-4126-8860-e3b99df91dd9",
                            PermissionName = "Delete:Resource"
                        },
                        new
                        {
                            Id = "75530804-26db-41f3-9d1b-e58ed9e8fee4",
                            PermissionName = "Create:Resource"
                        },
                        new
                        {
                            Id = "c57dadf9-5da7-4166-b0d0-ef64667f2572",
                            PermissionName = "View:Resource"
                        },
                        new
                        {
                            Id = "c5196f8c-95a8-4512-b5f2-c576958ff5cd",
                            PermissionName = "Edit:Exam"
                        },
                        new
                        {
                            Id = "34ffbd7d-0ec7-4a56-8d83-179568ba07c5",
                            PermissionName = "Delete:Exam"
                        },
                        new
                        {
                            Id = "de75d96d-fdb4-439b-8cf8-43d587974865",
                            PermissionName = "Create:Exam"
                        },
                        new
                        {
                            Id = "3c858bdd-3ef3-4ba6-b4f5-12b45f14ea02",
                            PermissionName = "View:Exam"
                        },
                        new
                        {
                            Id = "46f87f72-afaa-4df4-85d9-87dcd4c9b076",
                            PermissionName = "Edit:Question"
                        },
                        new
                        {
                            Id = "9c840343-8004-4d7f-b670-9a63c0237bce",
                            PermissionName = "Delete:Question"
                        },
                        new
                        {
                            Id = "a92d095a-50bf-4ff6-b774-bdd81df6313d",
                            PermissionName = "Create:Question"
                        },
                        new
                        {
                            Id = "a184a406-b527-4c2f-a123-f0a99d1737cd",
                            PermissionName = "View:Question"
                        },
                        new
                        {
                            Id = "b0c9c2e8-aa9e-4a02-a5ac-732152e72261",
                            PermissionName = "Edit:Answer"
                        },
                        new
                        {
                            Id = "1e77c1c6-478e-4111-8d76-c03c835c1151",
                            PermissionName = "Delete:Answer"
                        },
                        new
                        {
                            Id = "a3ebb2ea-fc78-4d7a-bc34-b620600a59c4",
                            PermissionName = "Create:Answer"
                        },
                        new
                        {
                            Id = "e1fb5d6a-f234-4b7b-8f4c-9c02a80e7396",
                            PermissionName = "View:Answer"
                        },
                        new
                        {
                            Id = "1793ca7e-61e6-4961-bdfb-766a3555fd20",
                            PermissionName = "Edit:ExamQuestion"
                        },
                        new
                        {
                            Id = "ce808469-ac15-40cc-a317-cbc2d7b1c7d2",
                            PermissionName = "Delete:ExamQuestion"
                        },
                        new
                        {
                            Id = "d8f4aaed-cd0b-46e8-8b9e-4ffefe9b2192",
                            PermissionName = "Create:ExamQuestion"
                        },
                        new
                        {
                            Id = "c986d59c-57fb-4669-86e0-7b51fd57b3ae",
                            PermissionName = "View:ExamQuestion"
                        },
                        new
                        {
                            Id = "52d410d4-96a6-4b6c-af97-72b921a5e532",
                            PermissionName = "Edit:Notification"
                        },
                        new
                        {
                            Id = "f89cfb34-8f2a-4d34-b431-97faac3ed02b",
                            PermissionName = "Delete:Notification"
                        },
                        new
                        {
                            Id = "cdea1f2c-0fcf-4615-ae3d-1b90e6801eb5",
                            PermissionName = "Create:Notification"
                        },
                        new
                        {
                            Id = "9e499c8e-ac5d-4f7c-98e0-d599b7f24247",
                            PermissionName = "View:Notification"
                        },
                        new
                        {
                            Id = "5044af9b-28ab-45f5-a757-719a286d0857",
                            PermissionName = "Edit:System"
                        },
                        new
                        {
                            Id = "6a5af63a-32c5-4424-90c6-c81b5f8a545a",
                            PermissionName = "Delete:System"
                        },
                        new
                        {
                            Id = "6bab1239-8a2c-4864-9927-2dccb8f894da",
                            PermissionName = "Create:System"
                        },
                        new
                        {
                            Id = "e81da78e-443e-45a9-a8d8-1d5dfdf0e2a6",
                            PermissionName = "View:System"
                        },
                        new
                        {
                            Id = "f702360a-f73b-46e7-adc0-ccaaf9c75db8",
                            PermissionName = "Edit:User"
                        },
                        new
                        {
                            Id = "13adaeba-2ce2-4f7d-a16d-9af743399612",
                            PermissionName = "Delete:User"
                        },
                        new
                        {
                            Id = "a9ea9725-b0d3-4d31-b6c3-1ac263ac463a",
                            PermissionName = "Create:User"
                        },
                        new
                        {
                            Id = "28147748-0067-48d7-95d7-4aca515a58c7",
                            PermissionName = "View:User"
                        });
                });

            modelBuilder.Entity("UserService.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}