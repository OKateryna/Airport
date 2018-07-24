using System;

namespace Airport.BL.Dto.Pilot
{
    public class PilotExternalDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Exp { get; set; }
    }
}