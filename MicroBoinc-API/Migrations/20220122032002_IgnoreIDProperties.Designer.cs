﻿// <auto-generated />
using System;
using MicroBoincAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MicroBoincAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220122032002_IgnoreIDProperties")]
    partial class IgnoreIDProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MicroBoincAPI.Models.Accounts.Account", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("DiscordID")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<long>("Offset")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Accounts.AccountKey", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("AccountID")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Identifier")
                        .HasColumnType("text");

                    b.Property<bool>("IsRoot")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWeak")
                        .HasColumnType("boolean");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("AccountsKeys");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Assignments.Assignment", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AssignedToID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<long>("TaskID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("AssignedToID");

                    b.HasIndex("TaskID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Binaries.Binary", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Checksum")
                        .HasColumnType("text");

                    b.Property<string>("DownloadURL")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Binaries");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Binaries.ProjectBinary", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("BinaryID")
                        .HasColumnType("bigint");

                    b.Property<long?>("PlatformID")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProjectID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("BinaryID");

                    b.HasIndex("PlatformID");

                    b.HasIndex("ProjectID");

                    b.ToTable("ProjectsBinaries");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Groups.Group", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("OwnedByID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("OwnedByID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Permissions.GroupPermission", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("AccountID")
                        .HasColumnType("bigint");

                    b.Property<long?>("GroupID")
                        .HasColumnType("bigint");

                    b.Property<int>("Permissions")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.HasIndex("GroupID");

                    b.ToTable("GroupsPermissions");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Platforms.Platform", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("DetectorBinaryID")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsSpecialized")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DetectorBinaryID");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Projects.Project", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("AddedByID")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<long>("GroupID")
                        .HasColumnType("bigint");

                    b.Property<string>("InputServerURL")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("UseExternalInputServer")
                        .HasColumnType("boolean");

                    b.HasKey("ID");

                    b.HasIndex("AddedByID");

                    b.HasIndex("GroupID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Results.Result", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("AssignmentID")
                        .HasColumnType("bigint");

                    b.Property<long>("ExecutionTime")
                        .HasColumnType("bigint");

                    b.Property<short>("ExitCode")
                        .HasColumnType("smallint");

                    b.Property<long>("ProjectID")
                        .HasColumnType("bigint");

                    b.Property<string>("StdErr")
                        .HasColumnType("text");

                    b.Property<string>("StdOut")
                        .HasColumnType("text");

                    b.Property<long>("TaskID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("AssignmentID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("TaskID");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Tasks.Task", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("GroupID")
                        .HasColumnType("bigint");

                    b.Property<string>("InputData")
                        .HasColumnType("text");

                    b.Property<string>("InputID")
                        .HasColumnType("text");

                    b.Property<long>("ProjectID")
                        .HasColumnType("bigint");

                    b.Property<int>("ResultsLeft")
                        .HasColumnType("integer");

                    b.Property<int>("SlotsLeft")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("ID", "SlotsLeft");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Accounts.AccountKey", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Accounts.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Assignments.Assignment", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Accounts.AccountKey", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MicroBoincAPI.Models.Tasks.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Binaries.ProjectBinary", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Binaries.Binary", "Binary")
                        .WithMany()
                        .HasForeignKey("BinaryID");

                    b.HasOne("MicroBoincAPI.Models.Platforms.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformID");

                    b.HasOne("MicroBoincAPI.Models.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");

                    b.Navigation("Binary");

                    b.Navigation("Platform");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Groups.Group", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Accounts.Account", "OwnedBy")
                        .WithMany()
                        .HasForeignKey("OwnedByID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnedBy");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Permissions.GroupPermission", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Accounts.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID");

                    b.HasOne("MicroBoincAPI.Models.Groups.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupID");

                    b.Navigation("Account");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Platforms.Platform", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Binaries.Binary", "DetectorBinary")
                        .WithMany()
                        .HasForeignKey("DetectorBinaryID");

                    b.Navigation("DetectorBinary");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Projects.Project", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Accounts.Account", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedByID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MicroBoincAPI.Models.Groups.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddedBy");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Results.Result", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Assignments.Assignment", "Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentID");

                    b.HasOne("MicroBoincAPI.Models.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MicroBoincAPI.Models.Tasks.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Project");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("MicroBoincAPI.Models.Tasks.Task", b =>
                {
                    b.HasOne("MicroBoincAPI.Models.Groups.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MicroBoincAPI.Models.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Project");
                });
#pragma warning restore 612, 618
        }
    }
}
