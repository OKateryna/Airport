using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.DAL.Models
{
    public class PlaneType : Entity
    {
        public string PlaneModel { get; set; }
        public int PlacesAmount { get; set; }
        public int CarryingCapacity { get; set; }
    }
}
