using System.Collections.Generic;
using System.Linq;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class CrewRepository : BaseRepository<Crew>
    {
        public CrewRepository()
        {
            SeedData.Add(new Crew { Id = 1, PilotId = 1, StewardessIds = new int[] { 1, 2 } });
            SeedData.Add(new Crew { Id = 2, PilotId = 2, StewardessIds = new int[] { 1, 2 } });
            SeedData.Add(new Crew { Id = 3, PilotId = 1, StewardessIds = new int[] { 1, 3 } });
            SeedData.Add(new Crew { Id = 4, PilotId = 4, StewardessIds = new int[] { 3, 4 } });
        }
       
        public override bool Update(Crew crew)
        {
            var oldCrew = SeedData.FirstOrDefault(c => c.Id == crew.Id);
            if (oldCrew == null)
                return false;

            oldCrew.PilotId = crew.PilotId;
            oldCrew.StewardessIds = crew.StewardessIds;

            return true;
        }
    }
}