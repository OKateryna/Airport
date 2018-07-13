using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.DAL.Models
{
    public class Crew : Entity
    {
        public int PilotId { get; set; }
        public int[] StewardessIds { get; set; }
    }
}
