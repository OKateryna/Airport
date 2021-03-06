﻿using System;
using System.Linq;
using System.Threading.Tasks;
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
                Experience = 8
            });
            SeedData.Add(new Pilot
            {
                Id = 2,
                FirstName = "Ihor",
                SecondName = "Vitrenko",
                BirthDate = new DateTime(1987, 1, 19),
                Experience = 4
            });
            SeedData.Add(new Pilot
            {
                Id = 3,
                FirstName = "Taras",
                SecondName = "Boiko",
                BirthDate = new DateTime(1960, 10, 2),
                Experience = 31
            });
            SeedData.Add(new Pilot
            {
                Id = 4,
                FirstName = "Viktor",
                SecondName = "Romaniuk",
                BirthDate = new DateTime(1992, 11, 27),
                Experience = 2
            });
        }

        public override async Task<bool> Update(Pilot pilot)
        {
            return await Task.Run(() =>
            {
                var oldPilot = SeedData.FirstOrDefault(p => p.Id == pilot.Id);
                if (oldPilot == null)
                    return false;

                oldPilot.Experience = pilot.Experience;
                return true;
            });
        }
    }
}
