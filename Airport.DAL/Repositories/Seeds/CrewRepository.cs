using System.Linq;
using System.Threading.Tasks;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class CrewRepository : BaseRepository<Crew>
    {
        public CrewRepository()
        {
            SeedData.Add(new Crew { Id = 1, Name = "Alpha" });
            SeedData.Add(new Crew { Id = 2, Name = "Bravo" });
            SeedData.Add(new Crew { Id = 3, Name = "Apolo" });
            SeedData.Add(new Crew { Id = 4, Name = "Delta" });
        }
       
        public override async Task<bool> Update(Crew crew)
        {
            return await Task.Run(() =>
            {
                var oldCrew = SeedData.FirstOrDefault(c => c.Id == crew.Id);
                if (oldCrew == null)
                    return false;
                oldCrew.Name = crew.Name;

                return true;
            });
        }
    }
}