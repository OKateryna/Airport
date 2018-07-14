using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Departure : Entity
    {
        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
        public DateTime DepartureDate { get; set; }
        [ForeignKey("Crew")]
        public int CrewId { get; set; }
        public virtual Crew Crew { get; set; }
        [ForeignKey("Plane")]
        public int PlaneId { get; set; }
        public virtual Plane Plane { get; set; }
    }
}
