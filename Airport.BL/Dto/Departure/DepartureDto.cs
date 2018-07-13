using System;
using Airport.BL.Dto.Crew;
using Airport.BL.Dto.Flight;
using Airport.BL.Dto.Plane;

namespace Airport.BL.Dto.Departure
{
    public class DepartureDto
    {
        public int Id { get; set; }
        public FlightDto Flight { get; set; }
        public DateTime DepartureDate { get; set; }
        public CrewDto Crew { get; set; }
        public PlaneDto Plane { get; set; }
    }
}