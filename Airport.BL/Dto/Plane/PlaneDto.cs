using System;
using Airport.BL.Dto.PlaneType;

namespace Airport.BL.Dto.Plane
{
    public class PlaneDto
    {
        public int Id { get; set; }
        public string PlaneName { get; set; }
        public PlaneTypeDto PlaneType { get; set; }
        public DateTime ManufectureDate { get; set; }
        public TimeSpan LifeSpan { get; set; }
    }
}