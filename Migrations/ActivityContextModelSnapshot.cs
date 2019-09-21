﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using activities.Models;

namespace activities.Migrations
{
    [DbContext(typeof(ActivityContext))]
    partial class ActivityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("activities.Models.Attending", b =>
                {
                    b.Property<int>("AttendingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<int>("UserId");

                    b.HasKey("AttendingId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Attendings");
                });

            modelBuilder.Entity("activities.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Coordinator");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("Time");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("numDuration");

                    b.Property<string>("strDuration")
                        .IsRequired();

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("activities.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("activities.Models.Attending", b =>
                {
                    b.HasOne("activities.Models.Event", "Event")
                        .WithMany("Attending")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("activities.Models.User", "User")
                        .WithMany("Attending")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
