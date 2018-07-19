using Airport.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.DAL.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Crew> Crews { get; set; }

        public DbSet<Departure> Departures { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Pilot> Pilots { get; set; }

        public DbSet<Plane> Planes { get; set; }

        public DbSet<PlaneType> PlaneTypes { get; set; }

        public DbSet<Stewardess> Stewardesses { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crew>()
                .HasOne(crew => crew.CrewPilot);


            modelBuilder.Entity<Crew>()
                .HasMany(crew => crew.CrewStewardesses);

            modelBuilder.Entity<CrewPilot>()
                .HasKey(cp => new { cp.CrewId, cp.PilotId });

            modelBuilder.Entity<CrewPilot>()
                .HasOne(cp => cp.Pilot)
                .WithMany(p => p.CrewPilots)
                .HasForeignKey(cp => cp.PilotId);

            modelBuilder.Entity<CrewPilot>()
                .HasOne(cp => cp.Crew)
                .WithOne(c => c.CrewPilot);

            modelBuilder.Entity<CrewStewardess>()
                .HasKey(cs => new { cs.CrewId, cs.StewardessId });

            modelBuilder.Entity<CrewStewardess>()
                .HasOne(cs => cs.Crew)
                .WithMany(c => c.CrewStewardesses)
                .HasForeignKey(cs => cs.CrewId);

            modelBuilder.Entity<CrewStewardess>()
                .HasOne(cs => cs.Stewardess)
                .WithMany(s => s.CrewStewardesses)
                .HasForeignKey(cs => cs.StewardessId);

            modelBuilder.Entity<Departure>()
                .HasOne(d => d.Crew)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Departure>()
                .HasOne(d => d.Flight)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Departure>()
                .HasOne(d => d.Plane)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    Id = 1,
                    Number = "12qwdf",
                    TimeDeparture = new DateTime(2016, 12, 5, 23, 15, 00),
                    PlaceDeparture = "London",
                    Destination = "Paris",
                    TimeDestination = new DateTime(2016, 12, 6, 00, 15, 00)
                },
                new Flight
                {
                    Id = 2,
                    Number = "55abll",
                    TimeDeparture = new DateTime(2017, 11, 23, 13, 10, 00),
                    PlaceDeparture = "Lviv",
                    Destination = "Habana",
                    TimeDestination = new DateTime(2017, 11, 23, 23, 55, 00)
                },
                new Flight
                {
                    Id = 3,
                    Number = "78qsco",
                    TimeDeparture = new DateTime(2018, 5, 11, 7, 30, 00),
                    PlaceDeparture = "Habana",
                    Destination = "London",
                    TimeDestination = new DateTime(2018, 5, 11, 16, 10, 00)
                },
                new Flight
                {
                    Id = 4,
                    Number = "02fthn",
                    TimeDeparture = new DateTime(2017, 9, 7, 8, 00, 00),
                    PlaceDeparture = "Lisbon",
                    Destination = "Paris",
                    TimeDestination = new DateTime(2017, 9, 7, 10, 30, 00)
                }
            );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 1, FlightId = 1, Price = 112 },
                new Ticket { Id = 2, FlightId = 2, Price = 212 },
                new Ticket { Id = 3, FlightId = 4, Price = 222.2 },
                new Ticket { Id = 4, FlightId = 3, Price = 100 }
            );

            modelBuilder.Entity<PlaneType>().HasData(
                new PlaneType { Id = 1, PlaneModel = "777", PlacesAmount = 114, CarryingCapacity = 52800 },
                new PlaneType { Id = 2, PlaneModel = "A320", PlacesAmount = 40, CarryingCapacity = 15000 },
                new PlaneType { Id = 3, PlaneModel = "100", PlacesAmount = 300, CarryingCapacity = 30000 },
                new PlaneType { Id = 4, PlaneModel = "Ту-134", PlacesAmount = 80, CarryingCapacity = 47000 }
            );

            modelBuilder.Entity<Plane>().HasData(
                new Plane
                {
                    Id = 1,
                    PlaneName = "Sukhoi SuperJet",
                    ManufectureDate = new DateTime(2009, 12, 5, 23, 15, 00),
                    PlaneTypeId = 3,
                    LifeSpan = 10
                },
                new Plane
                {
                    Id = 2,
                    PlaneName = "Airbus",
                    ManufectureDate = new DateTime(2018, 12, 5, 23, 15, 00),
                    PlaneTypeId = 2,
                    LifeSpan = 6
                },
                new Plane
                {
                    Id = 3,
                    PlaneName = "Tupolev",
                    ManufectureDate = new DateTime(2016, 5, 11, 7, 30, 00),
                    PlaneTypeId = 4,
                    LifeSpan = 14
                },
                new Plane
                {
                    Id = 4,
                    PlaneName = "Boeing",
                    ManufectureDate = new DateTime(2000, 9, 7, 8, 00, 00),
                    PlaneTypeId = 1,
                    LifeSpan = 20
                }
            );

            modelBuilder.Entity<Crew>().HasData(
                new Crew { Id = 1, Name = "Alpha" },
                new Crew { Id = 2, Name = "Bravo" },
                new Crew { Id = 3, Name = "Apolo" },
                new Crew { Id = 4, Name = "Delta" }
            );

            modelBuilder.Entity<Stewardess>().HasData(
                new Stewardess
                {
                    Id = 1,
                    FirstName = "Olena",
                    SecondName = "Petrenko",
                    BirthDate = new DateTime(1982, 6, 5)
                },
                new Stewardess
                {
                    Id = 2,
                    FirstName = "Iryna",
                    SecondName = "Moroz",
                    BirthDate = new DateTime(1998, 11, 11)
                },
                new Stewardess
                {
                    Id = 3,
                    FirstName = "Larysa",
                    SecondName = "Kovalchuk",
                    BirthDate = new DateTime(1993, 6, 10)
                },
                new Stewardess
                {
                    Id = 4,
                    FirstName = "Karina",
                    SecondName = "Voitovych",
                    BirthDate = new DateTime(1989, 5, 5)
                }
            );

            modelBuilder.Entity<Pilot>().HasData(
                new Pilot
                {
                    Id = 1,
                    FirstName = "Oleg",
                    SecondName = "Petrenko",
                    BirthDate = new DateTime(1980, 6, 5),
                    Experience = 8
                },
                new Pilot
                {
                    Id = 2,
                    FirstName = "Ihor",
                    SecondName = "Vitrenko",
                    BirthDate = new DateTime(1987, 1, 19),
                    Experience = 4
                },
                new Pilot
                {
                    Id = 3,
                    FirstName = "Taras",
                    SecondName = "Boiko",
                    BirthDate = new DateTime(1960, 10, 2),
                    Experience = 31
                },
                new Pilot
                {
                    Id = 4,
                    FirstName = "Viktor",
                    SecondName = "Romaniuk",
                    BirthDate = new DateTime(1992, 11, 27),
                    Experience = 2
                }
            );

            modelBuilder.Entity<CrewPilot>().HasData(
                new CrewPilot
                {
                    CrewId = 1,
                    PilotId = 4
                }, 
                new CrewPilot
                {
                    CrewId = 2,
                    PilotId = 1
                },
                new CrewPilot
                {
                    CrewId = 3,
                    PilotId = 4
                },
                new CrewPilot
                {
                    CrewId = 4,
                    PilotId = 2
                }
            );

            modelBuilder.Entity<CrewStewardess>().HasData(
                new CrewStewardess
                {
                    CrewId = 1,
                    StewardessId = 1
                },
                new CrewStewardess
                {
                    CrewId = 1,
                    StewardessId = 2
                },
                new CrewStewardess
                {
                    CrewId = 2,
                    StewardessId = 1
                },
                new CrewStewardess
                {
                    CrewId = 3,
                    StewardessId = 4
                },
                new CrewStewardess
                {
                    CrewId = 3,
                    StewardessId = 1
                },
                new CrewStewardess
                {
                    CrewId = 4,
                    StewardessId = 2
                },
                new CrewStewardess
                {
                    CrewId = 3,
                    StewardessId = 2
                },
                new CrewStewardess
                {
                    CrewId = 2,
                    StewardessId = 2
                },
                new CrewStewardess
                {
                    CrewId = 2,
                    StewardessId = 4
                },
                new CrewStewardess
                {
                    CrewId = 1,
                    StewardessId = 3
                }
            );

            modelBuilder.Entity<Departure>().HasData(
                new Departure
                {
                    Id = 1,
                    FlightId = 1,
                    DepartureDate = new DateTime(2017, 12, 9, 12, 00, 00),
                    CrewId = 3,
                    PlaneId = 3
                },
                new Departure
                {
                    Id = 2,
                    FlightId = 2,
                    DepartureDate = new DateTime(2017, 3, 23, 12, 00, 00),
                    CrewId = 1,
                    PlaneId = 4
                },
                new Departure
                {
                    Id = 3,
                    FlightId = 1,
                    DepartureDate = new DateTime(2017, 5, 11, 1, 45, 00),
                    CrewId = 2,
                    PlaneId = 1
                },
                new Departure
                {
                    Id = 4,
                    FlightId = 4,
                    DepartureDate = new DateTime(2017, 8, 5, 16, 45, 00),
                    CrewId = 4,
                    PlaneId = 2
                }
            );
        }
    }
}
