using System;

namespace Airport.BL.Dto.Pilot
{
    public class EditablePilotFields
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Expierence { get; set; }
    }
}