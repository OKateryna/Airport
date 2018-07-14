﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.DAL.Models
{
    public class CrewPilot
    {
        public int CrewId { get; set; }
        public Crew Crew { get; set; }
        public int PilotId { get; set; }
        public Pilot Pilot { get; set; }
    }
}