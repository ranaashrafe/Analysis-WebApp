﻿// <auto-generated />
using System;
using AnalysisWebApp.AnalysisDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnalysisWebApp.Migrations
{
    [DbContext(typeof(AnalysisDbContext))]
    [Migration("20200314004646_inhh")]
    partial class inhh
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnalysisWebApp.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MessageBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganDetailID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("AnalysisWebApp.Models.Organ", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Organs");
                });

            modelBuilder.Entity("AnalysisWebApp.Models.OrganDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MessageID")
                        .HasColumnType("int");

                    b.Property<int>("OrganID")
                        .HasColumnType("int");

                    b.Property<string>("Shape")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OrganID");

                    b.ToTable("OrganDetails");
                });

            modelBuilder.Entity("AnalysisWebApp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddedByUserID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AddedByUserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AnalysisWebApp.Models.UserChoice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsFav")
                        .HasColumnType("bit");

                    b.Property<int>("OrganDetailID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrganDetailID");

                    b.HasIndex("UserID");

                    b.ToTable("UserChoices");
                });

            modelBuilder.Entity("AnalysisWebApp.Models.OrganDetail", b =>
                {
                    b.HasOne("AnalysisWebApp.Models.Organ", null)
                        .WithMany("OrganDetails")
                        .HasForeignKey("OrganID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnalysisWebApp.Models.User", b =>
                {
                    b.HasOne("AnalysisWebApp.Models.User", "AddedByUser")
                        .WithMany()
                        .HasForeignKey("AddedByUserID");
                });

            modelBuilder.Entity("AnalysisWebApp.Models.UserChoice", b =>
                {
                    b.HasOne("AnalysisWebApp.Models.OrganDetail", "organDetail")
                        .WithMany("UserChoices")
                        .HasForeignKey("OrganDetailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnalysisWebApp.Models.User", null)
                        .WithMany("UserChoices")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
