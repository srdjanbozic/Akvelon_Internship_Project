﻿// <auto-generated />
using System;
using Internship_Task.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Internship_Task.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220823214913_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Internship_Task.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("project");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompletionDate = new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Priority = 3,
                            ProjectName = "Machine Learning",
                            StartDate = new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 2
                        },
                        new
                        {
                            Id = 2,
                            CompletionDate = new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Priority = 1,
                            ProjectName = "EShop",
                            StartDate = new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 3
                        },
                        new
                        {
                            Id = 3,
                            CompletionDate = new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Priority = 2,
                            ProjectName = "Front-End",
                            StartDate = new DateTime(2022, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 1
                        });
                });

            modelBuilder.Entity("Internship_Task.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TaskId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TaskDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TaskPriority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("task");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProjectId = 1,
                            Status = 2,
                            TaskDescription = "Implement merge sorting algorithm.",
                            TaskName = "Sorting",
                            TaskPriority = 3
                        },
                        new
                        {
                            Id = 2,
                            ProjectId = 2,
                            Status = 3,
                            TaskDescription = "Implement restful.",
                            TaskName = "RestFul Services",
                            TaskPriority = 1
                        },
                        new
                        {
                            Id = 3,
                            ProjectId = 3,
                            Status = 1,
                            TaskDescription = "Implement front-end.",
                            TaskName = "FrontEnd Implementation",
                            TaskPriority = 2
                        });
                });

            modelBuilder.Entity("Internship_Task.Models.Task", b =>
                {
                    b.HasOne("Internship_Task.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Internship_Task.Models.Project", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}