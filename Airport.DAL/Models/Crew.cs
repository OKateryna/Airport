﻿namespace Airport.DAL.Models
{
    public class Crew : Entity
    {
        public int PilotId { get; set; }
        public int[] StewardessIds { get; set; }
    }
}
