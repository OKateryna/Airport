using System.Collections.Generic;
using Airport.BL.Dto.Pilot;
using Airport.BL.Dto.Stewardess;

namespace Airport.BL.Dto.Crew
{
    public class CrewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PilotDto Pilot { get; set; }
        public IEnumerable<StewardessDto> Stewardesses { get; set; }
    }
}