using System;
using System.Linq;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class PilotRepository: BaseRepository<Pilot>
    {
        public PilotRepository()
        {
            SeedData.Add(new Pilot
            {
                Id = 1,
                FirstName = "Oleg",
                SecondName = "Petrenko",
                BirthDate = new DateTime(1980, 6, 5),
                Expierence = 8
            });
            SeedData.Add(new Pilot
            {
                Id = 2,
                FirstName = "Ihor",
                SecondName = "Vitrenko",
                BirthDate = new DateTime(1987, 1, 19),
                Expierence = 4
            });
            SeedData.Add(new Pilot
            {
                Id = 3,
                FirstName = "Taras",
                SecondName = "Boiko",
                BirthDate = new DateTime(1960, 10, 2),
                Expierence = 31
            });
            SeedData.Add(new Pilot
            {
                Id = 4,
                FirstName = "Viktor",
                SecondName = "Romaniuk",
                BirthDate = new DateTime(1992, 11, 27),
                Expierence = 2
            });
        }

        public override bool Update(Pilot pilot)
        {
            var oldPilot = SeedData.FirstOrDefault(p => p.Id == pilot.Id);
            if (oldPilot == null)
                return false;

            oldPilot.Expierence = pilot.Expierence;
            return true;
        }
    }
}
