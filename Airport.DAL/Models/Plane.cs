using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Plane : Entity
    {
        public string PlaneName { get; set; }
        [ForeignKey("PlaneType")]
        public int PlaneTypeId { get; set; }
        public virtual PlaneType PlaneType { get; set; }
        public DateTime ManufectureDate { get; set; }
        public TimeSpan LifeSpan { get; set; }
    }
}
