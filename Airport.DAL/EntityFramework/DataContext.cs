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
    }
}
