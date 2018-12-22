﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NTUEvents.Models;

namespace NTUEvents.Migrations
{
    [DbContext(typeof(NtuEventsContext))]
    partial class NtuEventsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NTUEvents.Models.Cca", b =>
                {
                    b.Property<int>("CcaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contact");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description");

                    b.Property<bool?>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("Schedule");

                    b.Property<string>("Type");

                    b.Property<int?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("Venue");

                    b.HasKey("CcaId");

                    b.ToTable("Ccas");
                });

            modelBuilder.Entity("NTUEvents.Models.CcaMembership", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("CcaId");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<int?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("UserId", "CcaId");

                    b.HasIndex("CcaId");

                    b.ToTable("CcaMemberships");
                });

            modelBuilder.Entity("NTUEvents.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CcaId");

                    b.Property<string>("Contact");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int?>("Quota");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Type");

                    b.Property<int?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("Venue");

                    b.HasKey("EventId");

                    b.HasIndex("CcaId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("NTUEvents.Models.EventParticipation", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("EventId");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<int?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("UserId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventParticipations");
                });

            modelBuilder.Entity("NTUEvents.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContactNumber");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Email");

                    b.Property<bool?>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NTUEvents.Models.CcaMembership", b =>
                {
                    b.HasOne("NTUEvents.Models.Cca", "Cca")
                        .WithMany("CcaMemberships")
                        .HasForeignKey("CcaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NTUEvents.Models.User", "User")
                        .WithMany("CcaMemberships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NTUEvents.Models.Event", b =>
                {
                    b.HasOne("NTUEvents.Models.Cca", "Cca")
                        .WithMany("Events")
                        .HasForeignKey("CcaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NTUEvents.Models.EventParticipation", b =>
                {
                    b.HasOne("NTUEvents.Models.Event", "Event")
                        .WithMany("EventParticipations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NTUEvents.Models.User", "User")
                        .WithMany("EventParticipations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
