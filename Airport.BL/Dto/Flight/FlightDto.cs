using System;
using System.Collections.Generic;
using Airport.BL.Dto.Ticket;

namespace Airport.BL.Dto.Flight
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string PlaceDeparture { get; set; }
        public DateTime TimeDeparture { get; set; }
        public string Destination { get; set; }
        public DateTime TimeDestination { get; set; }
        public virtual ICollection<TicketDto> Tickets { get; set; }
    }
}