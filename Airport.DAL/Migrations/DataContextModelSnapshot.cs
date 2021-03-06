﻿// <auto-generated />
using System;
using Airport.DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Airport.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Airport.DAL.Models.Crew", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Crews");

                    b.HasData(
                        new { Id = 1, Name = "Alpha" },
                        new { Id = 2, Name = "Bravo" },
                        new { Id = 3, Name = "Apolo" },
                        new { Id = 4, Name = "Delta" }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.CrewPilot", b =>
                {
                    b.Property<int>("CrewId");

                    b.Property<int>("PilotId");

                    b.HasKey("CrewId", "PilotId");

                    b.HasIndex("CrewId")
                        .IsUnique();

                    b.HasIndex("PilotId");

                    b.ToTable("CrewPilot");

                    b.HasData(
                        new { CrewId = 1, PilotId = 4 },
                        new { CrewId = 2, PilotId = 1 },
                        new { CrewId = 3, PilotId = 4 },
                        new { CrewId = 4, PilotId = 2 }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.CrewStewardess", b =>
                {
                    b.Property<int>("CrewId");

                    b.Property<int>("StewardessId");

                    b.HasKey("CrewId", "StewardessId");

                    b.HasIndex("StewardessId");

                    b.ToTable("CrewStewardess");

                    b.HasData(
                        new { CrewId = 1, StewardessId = 1 },
                        new { CrewId = 1, StewardessId = 2 },
                        new { CrewId = 2, StewardessId = 1 },
                        new { CrewId = 3, StewardessId = 4 },
                        new { CrewId = 3, StewardessId = 1 },
                        new { CrewId = 4, StewardessId = 2 },
                        new { CrewId = 3, StewardessId = 2 },
                        new { CrewId = 2, StewardessId = 2 },
                        new { CrewId = 2, StewardessId = 4 },
                        new { CrewId = 1, StewardessId = 3 }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.Departure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CrewId");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<int>("FlightId");

                    b.Property<int>("PlaneId");

                    b.HasKey("Id");

                    b.HasIndex("CrewId");

                    b.HasIndex("FlightId");

                    b.HasIndex("PlaneId");

                    b.ToTable("Departures");

                    b.HasData(
                        new { Id = 1, CrewId = 3, DepartureDate = new DateTime(2017, 12, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), FlightId = 1, PlaneId = 3 },
                        new { Id = 2, CrewId = 1, DepartureDate = new DateTime(2017, 3, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), FlightId = 2, PlaneId = 4 },
                        new { Id = 3, CrewId = 2, DepartureDate = new DateTime(2017, 5, 11, 1, 45, 0, 0, DateTimeKind.Unspecified), FlightId = 1, PlaneId = 1 },
                        new { Id = 4, CrewId = 4, DepartureDate = new DateTime(2017, 8, 5, 16, 45, 0, 0, DateTimeKind.Unspecified), FlightId = 4, PlaneId = 2 }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Destination")
                        .IsRequired();

                    b.Property<string>("Number")
                        .IsRequired();

                    b.Property<string>("PlaceDeparture")
                        .IsRequired();

                    b.Property<DateTime>("TimeDeparture");

                    b.Property<DateTime>("TimeDestination");

                    b.HasKey("Id");

                    b.ToTable("Flights");

                    b.HasData(
                        new { Id = 1, Destination = "Paris", Number = "12qwdf", PlaceDeparture = "London", TimeDeparture = new DateTime(2016, 12, 5, 23, 15, 0, 0, DateTimeKind.Unspecified), TimeDestination = new DateTime(2016, 12, 6, 0, 15, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 2, Destination = "Habana", Number = "55abll", PlaceDeparture = "Lviv", TimeDeparture = new DateTime(2017, 11, 23, 13, 10, 0, 0, DateTimeKind.Unspecified), TimeDestination = new DateTime(2017, 11, 23, 23, 55, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 3, Destination = "London", Number = "78qsco", PlaceDeparture = "Habana", TimeDeparture = new DateTime(2018, 5, 11, 7, 30, 0, 0, DateTimeKind.Unspecified), TimeDestination = new DateTime(2018, 5, 11, 16, 10, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 4, Destination = "Paris", Number = "02fthn", PlaceDeparture = "Lisbon", TimeDeparture = new DateTime(2017, 9, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), TimeDestination = new DateTime(2017, 9, 7, 10, 30, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.Pilot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<int>("Experience");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("SecondName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Pilots");

                    b.HasData(
                        new { Id = 1, BirthDate = new DateTime(1980, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), Experience = 8, FirstName = "Oleg", SecondName = "Petrenko" },
                        new { Id = 2, BirthDate = new DateTime(1987, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), Experience = 4, FirstName = "Ihor", SecondName = "Vitrenko" },
                        new { Id = 3, BirthDate = new DateTime(1960, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), Experience = 31, FirstName = "Taras", SecondName = "Boiko" },
                        new { Id = 4, BirthDate = new DateTime(1992, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), Experience = 2, FirstName = "Viktor", SecondName = "Romaniuk" }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LifeSpan");

                    b.Property<DateTime>("ManufectureDate");

                    b.Property<string>("PlaneName")
                        .IsRequired();

                    b.Property<int>("PlaneTypeId");

                    b.HasKey("Id");

                    b.HasIndex("PlaneTypeId");

                    b.ToTable("Planes");

                    b.HasData(
                        new { Id = 1, LifeSpan = 10, ManufectureDate = new DateTime(2009, 12, 5, 23, 15, 0, 0, DateTimeKind.Unspecified), PlaneName = "Sukhoi SuperJet", PlaneTypeId = 3 },
                        new { Id = 2, LifeSpan = 6, ManufectureDate = new DateTime(2018, 12, 5, 23, 15, 0, 0, DateTimeKind.Unspecified), PlaneName = "Airbus", PlaneTypeId = 2 },
                        new { Id = 3, LifeSpan = 14, ManufectureDate = new DateTime(2016, 5, 11, 7, 30, 0, 0, DateTimeKind.Unspecified), PlaneName = "Tupolev", PlaneTypeId = 4 },
                        new { Id = 4, LifeSpan = 20, ManufectureDate = new DateTime(2000, 9, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), PlaneName = "Boeing", PlaneTypeId = 1 }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.PlaneType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarryingCapacity");

                    b.Property<int>("PlacesAmount");

                    b.Property<string>("PlaneModel")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PlaneTypes");

                    b.HasData(
                        new { Id = 1, CarryingCapacity = 52800, PlacesAmount = 114, PlaneModel = "777" },
                        new { Id = 2, CarryingCapacity = 15000, PlacesAmount = 40, PlaneModel = "A320" },
                        new { Id = 3, CarryingCapacity = 30000, PlacesAmount = 300, PlaneModel = "100" },
                        new { Id = 4, CarryingCapacity = 47000, PlacesAmount = 80, PlaneModel = "Ту-134" }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.Stewardess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("SecondName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Stewardesses");

                    b.HasData(
                        new { Id = 1, BirthDate = new DateTime(1982, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstName = "Olena", SecondName = "Petrenko" },
                        new { Id = 2, BirthDate = new DateTime(1998, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstName = "Iryna", SecondName = "Moroz" },
                        new { Id = 3, BirthDate = new DateTime(1993, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstName = "Larysa", SecondName = "Kovalchuk" },
                        new { Id = 4, BirthDate = new DateTime(1989, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstName = "Karina", SecondName = "Voitovych" }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlightId");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new { Id = 1, FlightId = 1, Price = 112.0 },
                        new { Id = 2, FlightId = 2, Price = 212.0 },
                        new { Id = 3, FlightId = 4, Price = 222.2 },
                        new { Id = 4, FlightId = 3, Price = 100.0 }
                    );
                });

            modelBuilder.Entity("Airport.DAL.Models.CrewPilot", b =>
                {
                    b.HasOne("Airport.DAL.Models.Crew", "Crew")
                        .WithOne("CrewPilot")
                        .HasForeignKey("Airport.DAL.Models.CrewPilot", "CrewId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Airport.DAL.Models.Pilot", "Pilot")
                        .WithMany("CrewPilots")
                        .HasForeignKey("PilotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Airport.DAL.Models.CrewStewardess", b =>
                {
                    b.HasOne("Airport.DAL.Models.Crew", "Crew")
                        .WithMany("CrewStewardesses")
                        .HasForeignKey("CrewId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Airport.DAL.Models.Stewardess", "Stewardess")
                        .WithMany("CrewStewardesses")
                        .HasForeignKey("StewardessId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Airport.DAL.Models.Departure", b =>
                {
                    b.HasOne("Airport.DAL.Models.Crew", "Crew")
                        .WithMany()
                        .HasForeignKey("CrewId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Airport.DAL.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Airport.DAL.Models.Plane", "Plane")
                        .WithMany()
                        .HasForeignKey("PlaneId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Airport.DAL.Models.Plane", b =>
                {
                    b.HasOne("Airport.DAL.Models.PlaneType", "PlaneType")
                        .WithMany()
                        .HasForeignKey("PlaneTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Airport.DAL.Models.Ticket", b =>
                {
                    b.HasOne("Airport.DAL.Models.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
