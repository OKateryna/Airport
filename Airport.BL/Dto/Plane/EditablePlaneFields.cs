using System;

namespace Airport.BL.Dto.Plane
{
    public class EditablePlaneFields
    {
        public string PlaneName { get; set; }
        public int PlaneTypeId { get; set; }
        public DateTime ManufectureDate { get; set; }
        public TimeSpan LifeSpan { get; set; }
    }
}