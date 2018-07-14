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
                .HasOne(cp => cp.Pilot);

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
                .HasOne(cs => cs.Stewardess);

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

            modelBuilder.Entity<Plane>().HasData(
                new Plane
                {
                    Id = 1,
                    PlaneName = "Sukhoi SuperJet",
                    ManufectureDate = new DateTime(2009, 12, 5, 23, 15, 00),
                    PlaneTypeId = 3,
                    LifeSpan = new TimeSpan(3600, 0, 0, 0)
                },
                new Plane
                {
                    Id = 2,
                    PlaneName = "Airbus",
                    ManufectureDate = new DateTime(2018, 12, 5, 23, 15, 00),
                    PlaneTypeId = 2,
                    LifeSpan = new TimeSpan(1800, 0, 0, 0)
                },
                new Plane
                {
                    Id = 3,
                    PlaneName = "Tupolev",
                    ManufectureDate = new DateTime(2016, 5, 11, 7, 30, 00),
                    PlaneTypeId = 4,
                    LifeSpan = new TimeSpan(4200, 0, 0, 0)
                },
                new Plane
                {
                    Id = 4,
                    PlaneName = "Boeing",
                    ManufectureDate = new DateTime(2000, 9, 7, 8, 00, 00),
                    PlaneTypeId = 1,
                    LifeSpan = new TimeSpan(7200, 0, 0, 0)
                }
            );

            modelBuilder.Entity<PlaneType>().HasData(
                new PlaneType { Id = 1, PlaneModel = "777", PlacesAmount = 114, CarryingCapacity = 52800 },
                new PlaneType { Id = 2, PlaneModel = "A320", PlacesAmount = 40, CarryingCapacity = 15000 },
                new PlaneType { Id = 3, PlaneModel = "100", PlacesAmount = 300, CarryingCapacity = 30000 },
                new PlaneType { Id = 4, PlaneModel = "Ту-134", PlacesAmount = 80, CarryingCapacity = 47000 }
            );


        }
    }
}
