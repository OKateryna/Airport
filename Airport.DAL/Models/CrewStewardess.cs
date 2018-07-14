using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.DAL.Models
{
    public class CrewStewardess
    {
        public int CrewId { get; set; }
        public Crew Crew { get; set; }
        public int StewardessId { get; set; }
        public Stewardess Stewardess { get; set; }
    }
}
