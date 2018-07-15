using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Flight : Entity
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public string PlaceDeparture { get; set; }
        [Required]
        public DateTime TimeDeparture { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime TimeDestination { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
