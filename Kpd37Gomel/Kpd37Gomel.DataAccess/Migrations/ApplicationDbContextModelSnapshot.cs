﻿// <auto-generated />
using System;
using Kpd37Gomel.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kpd37Gomel.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.Apartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("ApartmentNumber")
                        .HasColumnName("ApartmentNumber");

                    b.Property<int?>("FloorNumber")
                        .HasColumnName("FloorNumber");

                    b.Property<double?>("LivingSpace")
                        .HasColumnName("LivingSpace");

                    b.Property<int?>("RoomsCount")
                        .HasColumnName("RoomsCount");

                    b.Property<double>("TotalArea")
                        .HasColumnName("TotalArea");

                    b.Property<double?>("TotalAreaSnb")
                        .HasColumnName("TotalAreaSnb");

                    b.Property<double>("VoteRate")
                        .HasColumnName("VoteRate");

                    b.HasKey("Id");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.ApartmentTenant", b =>
                {
                    b.Property<Guid>("ApartmentId")
                        .HasColumnName("ApartmentId");

                    b.Property<Guid>("TenantId")
                        .HasColumnName("TenantId");

                    b.Property<bool>("IsOwner")
                        .HasColumnName("IsOwner");

                    b.HasKey("ApartmentId", "TenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("ApartmentTenant");
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasMaxLength(100);

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("LastName")
                        .HasMaxLength(100);

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnName("MiddleName")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnName("AuthorId");

                    b.Property<DateTime>("CreateDateUtc")
                        .HasColumnName("CreateDateUtc");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsDeleted")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasMaxLength(150);

                    b.Property<bool>("UseVoteRate")
                        .HasColumnName("UseVoteRate");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.VoteChoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<Guid>("ApartmentId")
                        .HasColumnName("ApartmentId");

                    b.Property<DateTime>("VoteDateUtc")
                        .HasColumnName("VoteDateUtc");

                    b.Property<Guid>("VoteId")
                        .HasColumnName("VoteId");

                    b.Property<double?>("VoteRate")
                        .HasColumnName("VoteRate");

                    b.Property<Guid>("VoteVariantId")
                        .HasColumnName("VoteVariantId");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("VoteId");

                    b.HasIndex("VoteVariantId");

                    b.ToTable("VoteChoice");
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.VoteVariant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("SequenceIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SequenceIndex")
                        .HasDefaultValue(0);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("Text")
                        .HasMaxLength(200);

                    b.Property<Guid>("VoteId")
                        .HasColumnName("VoteId");

                    b.HasKey("Id");

                    b.HasIndex("VoteId");

                    b.ToTable("VoteVariant");
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.ApartmentTenant", b =>
                {
                    b.HasOne("Kpd37Gomel.DataAccess.Models.Apartment", "Apartment")
                        .WithMany("ApartmentTenants")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Kpd37Gomel.DataAccess.Models.Tenant", "Tenant")
                        .WithMany("ApartmentTenants")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.Vote", b =>
                {
                    b.HasOne("Kpd37Gomel.DataAccess.Models.Tenant", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.VoteChoice", b =>
                {
                    b.HasOne("Kpd37Gomel.DataAccess.Models.Apartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Kpd37Gomel.DataAccess.Models.Vote", "Vote")
                        .WithMany("Choices")
                        .HasForeignKey("VoteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Kpd37Gomel.DataAccess.Models.VoteVariant", "VoteVariant")
                        .WithMany("VoteChoices")
                        .HasForeignKey("VoteVariantId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Kpd37Gomel.DataAccess.Models.VoteVariant", b =>
                {
                    b.HasOne("Kpd37Gomel.DataAccess.Models.Vote", "Vote")
                        .WithMany("Variants")
                        .HasForeignKey("VoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
