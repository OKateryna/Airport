using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Plane : Entity
    {
        [Required]
        public string PlaneName { get; set; }
        [ForeignKey("PlaneType")]
        public int PlaneTypeId { get; set; }
        public virtual PlaneType PlaneType { get; set; }
        [Required]
        public DateTime ManufectureDate { get; set; }
        [Required]
        public int LifeSpan { get; set; }
    }
}
