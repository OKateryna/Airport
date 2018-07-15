using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.DAL.Models
{
    public class CrewPilot
    {
        public int CrewId { get; set; }
        public virtual Crew Crew { get; set; }
        public int PilotId { get; set; }
        public virtual Pilot Pilot { get; set; }
    }
}
