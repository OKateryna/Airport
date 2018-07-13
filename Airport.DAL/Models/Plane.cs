using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
