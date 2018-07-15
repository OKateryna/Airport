using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Departure : Entity
    {
        [Required, ForeignKey("Flight")]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required, ForeignKey("Crew")]
        public int CrewId { get; set; }
        public virtual Crew Crew { get; set; }
        [Required, ForeignKey("Plane")]
        public int PlaneId { get; set; }
        public virtual Plane Plane { get; set; }
    }
}
