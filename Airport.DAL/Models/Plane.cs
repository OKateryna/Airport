﻿using System;

namespace Airport.DAL.Models
{
    public class Plane : Entity
    {
        public string PlaneName { get; set; }
        public int PlaneTypeId { get; set; }
        public DateTime ManufectureDate { get; set; }
        public TimeSpan LifeSpan { get; set; }
    }
}
