using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Flight : Entity
    {
        public string Number { get; set; }
        public string PlaceDeparture { get; set; }
        public DateTime TimeDeparture { get; set; }
        public string Destination { get; set; }
        public DateTime TimeDestination { get; set; }
        public virtual IEnumerable<Ticket> Tickets { get; set; }
    }
}
