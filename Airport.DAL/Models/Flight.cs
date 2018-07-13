using System;


namespace Airport.DAL.Models
{
    public class Flight : Entity
    {
        public string Number { get; set; }
        public string PlaceDeparture { get; set; }
        public DateTime TimeDeparture { get; set; }
        public string Destination { get; set; }
        public DateTime TimeDestination { get; set; }
        public int TicketId { get; set; }
    }
}
