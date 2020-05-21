﻿// <auto-generated />
using System;
using Gero.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gero.API.Migrations
{
    [DbContext(typeof(DistributionContext))]
    [Migration("20180717145930_CreateUserInfo")]
    partial class CreateUserInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gero.API.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("MacPrinter")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Model")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(10);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.Property<string>("Version")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Devices","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("ClassType")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.Property<string>("IconName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Order");

                    b.Property<int?>("ParentId");

                    b.Property<string>("Position")
                        .IsRequired();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Modules","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Roles","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.RoleModule", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("ModuleId");

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("RoleId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("RoleModules","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.SalesChannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Route")
                        .IsRequired();

                    b.Property<int?>("SalesRegionId");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("SalesRegionId");

                    b.ToTable("SalesChannels","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.SalesRegion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("SalesRegions","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<DateTimeOffset>("InvitationAcceptedAt");

                    b.Property<DateTimeOffset>("InvitationSentAt");

                    b.Property<string>("InvitationToken")
                        .HasMaxLength(100);

                    b.Property<byte[]>("Password");

                    b.Property<DateTimeOffset>("ResetPasswordExpiredAt");

                    b.Property<DateTimeOffset>("ResetPasswordSentAt");

                    b.Property<string>("ResetPasswordToken")
                        .HasMaxLength(100);

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Users","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTimeOffset>("Birthdate");

                    b.Property<string>("DepartmentCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LicenseNumber")
                        .HasMaxLength(20);

                    b.Property<string>("MunicipalityCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("MunicipalityName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NationalityCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("NationalityName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserInfos","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.UserSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Route")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.UserTelephone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("TelephoneType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int?>("UserInfoId");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("UserTelephones","DISTRIBUCION");
                });

            modelBuilder.Entity("Gero.API.Models.Module", b =>
                {
                    b.HasOne("Gero.API.Models.Module", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Gero.API.Models.RoleModule", b =>
                {
                    b.HasOne("Gero.API.Models.Module", "Module")
                        .WithMany("Roles")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gero.API.Models.Role", "Role")
                        .WithMany("Modules")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gero.API.Models.SalesChannel", b =>
                {
                    b.HasOne("Gero.API.Models.SalesRegion", "SalesRegion")
                        .WithMany("SalesChannels")
                        .HasForeignKey("SalesRegionId");
                });

            modelBuilder.Entity("Gero.API.Models.UserInfo", b =>
                {
                    b.HasOne("Gero.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gero.API.Models.UserRole", b =>
                {
                    b.HasOne("Gero.API.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gero.API.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gero.API.Models.UserSetting", b =>
                {
                    b.HasOne("Gero.API.Models.User", "User")
                        .WithOne("Setting")
                        .HasForeignKey("Gero.API.Models.UserSetting", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gero.API.Models.UserTelephone", b =>
                {
                    b.HasOne("Gero.API.Models.UserInfo", "UserInfo")
                        .WithMany("Phones")
                        .HasForeignKey("UserInfoId");
                });
#pragma warning restore 612, 618
        }
    }
}
