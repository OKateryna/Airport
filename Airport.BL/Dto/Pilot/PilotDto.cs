using System;

namespace Airport.BL.Dto.Pilot
{
    public class PilotDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Expierence { get; set; }
    }
}