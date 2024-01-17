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
    [Migration("20240117041024_AddAndSeedRolePermission")]
    partial class AddAndSeedRolePermission
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
                });

            modelBuilder.Entity("UserService.Data.RolePermission", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PermissionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            Id = "e7eb1479-d609-4db0-90cd-661dcbcc6296",
                            PermissionId = "0bda25ae-f034-43f3-b6fb-2f06d32870a4",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "b50419e4-3a4e-49b5-a749-8eefefa5aac3",
                            PermissionId = "80e94b56-160d-4450-a1d1-0f17f6fe1456",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "5572d744-c870-47a0-b04b-858b86454cf1",
                            PermissionId = "88dbd1f9-d943-41ed-a607-866def46d6a6",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "9a587e38-e203-4fbd-b2bf-e937f9506a06",
                            PermissionId = "95aeb353-24d8-4a8d-8b43-ae4ef3860a03",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "b8028e6b-9356-4fc1-8554-b59d486d14bf",
                            PermissionId = "2a6f25ea-83aa-4c94-a1eb-0434671d5230",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "dbc0af5f-fb02-40b4-8ed6-a27f1b74f13b",
                            PermissionId = "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "6a3261d2-dad6-4abe-8998-17859ca00c65",
                            PermissionId = "90065181-d7c7-4258-8c5b-237871454c5a",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "aa015b02-7bcf-4b27-9cf8-6d80056a072a",
                            PermissionId = "c5feb7be-eeea-46e3-b44c-588d37eff052",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "d5b87180-1a10-407f-8f41-41b83322acf0",
                            PermissionId = "75530804-26db-41f3-9d1b-e58ed9e8fee4",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "67947f2f-6fea-4003-8921-eb8d53df97b3",
                            PermissionId = "7b15cfff-a2ed-4126-8860-e3b99df91dd9",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "fe88792b-5498-44a8-98a6-35afc9209c9f",
                            PermissionId = "c0030dfa-5bae-4dfa-8e7c-b367ed42a3db",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "5666bcb2-d5a3-4600-b514-e84ac5abf416",
                            PermissionId = "c57dadf9-5da7-4166-b0d0-ef64667f2572",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "f0884738-420d-46b0-a2a4-9faf9e987a55",
                            PermissionId = "27a0b056-02b8-4cb7-a81c-26757569d9cf",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "1d70ba56-f01c-4928-abee-03fa291c05a7",
                            PermissionId = "2e8e852d-9e69-4f43-a9bf-deaead6485d1",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "c2c44050-e317-4157-afaf-69d1b6d7fc32",
                            PermissionId = "a995e571-59cb-4f70-8aa9-2f637bb67e13",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "74edb382-15d0-4fb2-86b8-d1dbde619c80",
                            PermissionId = "ead1e55a-8213-442d-b8a4-a49fbeaad9a2",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "f11d6812-71ab-4f25-9e4c-3eb603250fb3",
                            PermissionId = "13adaeba-2ce2-4f7d-a16d-9af743399612",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "5b3a9623-79e4-4544-af7c-6268d6e6f7bd",
                            PermissionId = "28147748-0067-48d7-95d7-4aca515a58c7",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "ce63f979-24e1-4c47-98c6-647c89c2a1a0",
                            PermissionId = "a9ea9725-b0d3-4d31-b6c3-1ac263ac463a",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "e2d68255-f58b-4c1a-bf1c-72736c2173b3",
                            PermissionId = "f702360a-f73b-46e7-adc0-ccaaf9c75db8",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "fabd91ed-ad71-43be-8fba-b235e563edd6",
                            PermissionId = "ee255870-c765-4451-a019-a408d9e6ee27",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "5a9fbf03-421d-46a6-9a90-41d953cf5e2c",
                            PermissionId = "0db91594-128b-4aa5-bf04-77aa417143f4",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "ed5d0a3a-0dea-4e09-8e79-976124a54ca9",
                            PermissionId = "c986d59c-57fb-4669-86e0-7b51fd57b3ae",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "ab6ae13a-24f0-47e4-9cec-c33d922e6b76",
                            PermissionId = "a184a406-b527-4c2f-a123-f0a99d1737cd",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "ae34f41c-398f-4b13-9496-83a8ef816546",
                            PermissionId = "e1fb5d6a-f234-4b7b-8f4c-9c02a80e7396",
                            RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15"
                        },
                        new
                        {
                            Id = "352e5714-80d6-4384-bb30-bc398d2cc2d9",
                            PermissionId = "1b183555-3067-4485-b563-65b34b29475f",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "15fa835d-0ef5-47e8-aeae-935db0db40e5",
                            PermissionId = "6d6ad73b-9a19-449f-a809-57794239e882",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "36a34f90-ca02-418c-9a5b-b17bc4b2fcbf",
                            PermissionId = "940748d7-c97d-4157-8104-fde614869fd9",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "71429366-7110-41ba-94bb-0e5573f57336",
                            PermissionId = "ee255870-c765-4451-a019-a408d9e6ee27",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "ee90a2a6-3e9b-4223-ae10-b34520bc9af4",
                            PermissionId = "0db91594-128b-4aa5-bf04-77aa417143f4",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "9df6c415-ad42-46d0-9fb5-75b84a1b3ed9",
                            PermissionId = "6a886d29-1f69-459e-80ca-5b37ef7f2fe6",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "60464c39-3b39-4777-bc3d-d94f9ab688bc",
                            PermissionId = "9aafbadc-9401-4ebb-95b1-1adf64ecef00",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "ca5df13e-762b-4db3-aeb2-22b2ffcc11bb",
                            PermissionId = "e368179b-9797-4018-8867-72723cecc92e",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "2da17aab-cd56-4bec-8681-75f8e55068a4",
                            PermissionId = "75530804-26db-41f3-9d1b-e58ed9e8fee4",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "5a673e3f-73cc-4dc6-ba08-893b5ee54741",
                            PermissionId = "7b15cfff-a2ed-4126-8860-e3b99df91dd9",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "998f5787-4548-42a4-b716-c77e7dfc1bc3",
                            PermissionId = "c0030dfa-5bae-4dfa-8e7c-b367ed42a3db",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "67074e00-ed48-451a-bdf9-2c2ded591450",
                            PermissionId = "c57dadf9-5da7-4166-b0d0-ef64667f2572",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "8911ae15-664b-4762-a253-5fdcfeb03a0d",
                            PermissionId = "34ffbd7d-0ec7-4a56-8d83-179568ba07c5",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "b4da1aff-4401-4a6d-b1de-78c0bbbd662b",
                            PermissionId = "3c858bdd-3ef3-4ba6-b4f5-12b45f14ea02",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "f80a8fe9-20ca-4b6a-9435-3c3647eb9f69",
                            PermissionId = "c5196f8c-95a8-4512-b5f2-c576958ff5cd",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "4ca8a39d-7383-4c5d-8878-d8b92f77211d",
                            PermissionId = "de75d96d-fdb4-439b-8cf8-43d587974865",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "cd5bdff6-c2ed-40e6-8895-27faef4d6182",
                            PermissionId = "1793ca7e-61e6-4961-bdfb-766a3555fd20",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "d4222f06-d189-4876-875b-5a1b77e6fdbd",
                            PermissionId = "c986d59c-57fb-4669-86e0-7b51fd57b3ae",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "6f6a0649-9f15-4135-9f45-a838ddcf6ad8",
                            PermissionId = "ce808469-ac15-40cc-a317-cbc2d7b1c7d2",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "9ca2f642-b042-4f46-b610-ca47794fae25",
                            PermissionId = "d8f4aaed-cd0b-46e8-8b9e-4ffefe9b2192",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "ac89235c-d330-4a48-8d6c-1ad088739063",
                            PermissionId = "46f87f72-afaa-4df4-85d9-87dcd4c9b076",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "8e9883c0-74db-44d1-bab6-0947f1d221a9",
                            PermissionId = "9c840343-8004-4d7f-b670-9a63c0237bce",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "0c016ea4-4c99-42e8-a947-964afb693a6d",
                            PermissionId = "a184a406-b527-4c2f-a123-f0a99d1737cd",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "1e0d46bc-ec00-46dc-9849-d51a6071200c",
                            PermissionId = "a92d095a-50bf-4ff6-b774-bdd81df6313d",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "6024a48c-cab5-4b6b-8b6c-f151de5127d1",
                            PermissionId = "1e77c1c6-478e-4111-8d76-c03c835c1151",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "2dfd8c55-4ca9-4231-98a3-05f513a8a2f2",
                            PermissionId = "a3ebb2ea-fc78-4d7a-bc34-b620600a59c4",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "a5d514d9-a22d-4b5f-a582-ce41262b4366",
                            PermissionId = "b0c9c2e8-aa9e-4a02-a5ac-732152e72261",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "0d4a6a51-9246-45c7-b9b0-9758173551ef",
                            PermissionId = "e1fb5d6a-f234-4b7b-8f4c-9c02a80e7396",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "8b5e9da9-7c0b-4ead-bd10-432356449e7a",
                            PermissionId = "28147748-0067-48d7-95d7-4aca515a58c7",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "a1b14a91-2e4f-4b50-8faf-edeeb04edec6",
                            PermissionId = "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "95cf3fa0-65b1-4969-8d28-b8a6befe73da",
                            PermissionId = "80e94b56-160d-4450-a1d1-0f17f6fe1456",
                            RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51"
                        },
                        new
                        {
                            Id = "3bc87d2d-e445-498c-98f3-bcc3a0a286b5",
                            PermissionId = "80e94b56-160d-4450-a1d1-0f17f6fe1456",
                            RoleId = "fd455dd4-097a-457e-95a3-21c40374fb01"
                        },
                        new
                        {
                            Id = "81bd9ecd-5709-46ae-b409-76e3b268c556",
                            PermissionId = "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d",
                            RoleId = "fd455dd4-097a-457e-95a3-21c40374fb01"
                        },
                        new
                        {
                            Id = "517422df-0c7d-45fb-9895-ce2d32052862",
                            PermissionId = "28147748-0067-48d7-95d7-4aca515a58c7",
                            RoleId = "fd455dd4-097a-457e-95a3-21c40374fb01"
                        },
                        new
                        {
                            Id = "d97040c8-d896-4570-b3f2-37c77f3bda1b",
                            PermissionId = "3c858bdd-3ef3-4ba6-b4f5-12b45f14ea02",
                            RoleId = "fd455dd4-097a-457e-95a3-21c40374fb01"
                        },
                        new
                        {
                            Id = "ad60ed98-9f3f-4809-94ae-8f85602b3b16",
                            PermissionId = "ee255870-c765-4451-a019-a408d9e6ee27",
                            RoleId = "fd455dd4-097a-457e-95a3-21c40374fb01"
                        },
                        new
                        {
                            Id = "b9521dd8-53a8-4a32-bf2c-d861a1b779fd",
                            PermissionId = "0db91594-128b-4aa5-bf04-77aa417143f4",
                            RoleId = "fd455dd4-097a-457e-95a3-21c40374fb01"
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

            modelBuilder.Entity("UserService.Data.RolePermission", b =>
                {
                    b.HasOne("UserService.Data.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("UserService.Data.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
