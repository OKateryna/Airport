using System;
using System.Linq;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class FlightRepository : BaseRepository<Flight>
    {
        public FlightRepository()
        {
            SeedData.Add(new Flight
            {
                Id = 1,
                Number = "12qwdf",
                TimeDeparture = new DateTime(2016, 12, 5, 23, 15, 00),
                PlaceDeparture = "London",
                Destination = "Paris",
                TimeDestination = new DateTime(2016, 12, 6, 00, 15, 00),
                TicketId = 3
            });
            SeedData.Add(new Flight
            {
                Id = 2,
                Number = "55abll",
                TimeDeparture = new DateTime(2017, 11, 23, 13, 10, 00),
                PlaceDeparture = "Lviv",
                Destination = "Habana",
                TimeDestination = new DateTime(2017, 11, 23, 23, 55, 00),
                TicketId = 2
            });
            SeedData.Add(new Flight
            {
                Id = 3,
                Number = "78qsco",
                TimeDeparture = new DateTime(2018, 5, 11, 7, 30, 00),
                PlaceDeparture = "Habana",
                Destination = "London",
                TimeDestination = new DateTime(2018, 5, 11, 16, 10, 00),
                TicketId = 1
            });
            SeedData.Add(new Flight
            {
                Id = 4,
                Number = "02fthn",
                TimeDeparture = new DateTime(2017, 9, 7, 8, 00, 00),
                PlaceDeparture = "Lisbon",
                Destination = "Paris",
                TimeDestination = new DateTime(2017, 9, 7, 10, 30, 00),
                TicketId = 4
            });
        }


        public override bool Update(Flight flight)
        {
            var oldFlight = SeedData.FirstOrDefault(f => f.Id == flight.Id);
            if (oldFlight == null)
                return false;

            oldFlight.Destination = flight.Destination;
            oldFlight.Number = flight.Number;
            oldFlight.PlaceDeparture = flight.PlaceDeparture;
            oldFlight.TicketId = flight.TicketId;
            oldFlight.TimeDeparture = flight.TimeDeparture;
            oldFlight.TimeDestination = flight.TimeDestination;

            return true;
        }
    }
}