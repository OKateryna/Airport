﻿using System;

namespace Airport.DAL.Models
{
    public class Departure : Entity
    {
        public int FlightId { get; set; }
        public DateTime DepartureDate { get; set; }
        public int CrewId { get; set; }
        public int PlaneId { get; set; }
    }
}
