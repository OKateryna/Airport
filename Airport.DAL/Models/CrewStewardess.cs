using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.DAL.Models
{
    public class CrewStewardess
    {
        public int CrewId { get; set; }
        public virtual Crew Crew { get; set; }
        public int StewardessId { get; set; }
        public virtual Stewardess Stewardess { get; set; }
    }
}
