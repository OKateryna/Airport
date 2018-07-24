using System.Collections.Generic;
using Airport.BL.Dto.Pilot;
using Airport.BL.Dto.Stewardess;

namespace Airport.BL.Dto.Crew
{
    public class CrewExternalDto
    {
        public int Id { get; set; }
        public List<PilotExternalDto> Pilot { get; set; }
        public List<StewardessExternalDto> Stewardess { get; set; }
    }
}