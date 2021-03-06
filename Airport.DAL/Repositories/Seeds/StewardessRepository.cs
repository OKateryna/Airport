﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class StewardessRepository: BaseRepository<Stewardess>
    {
        public StewardessRepository()
        {
            SeedData.Add(new Stewardess
            {
                Id = 1,
                FirstName = "Olena",
                SecondName = "Petrenko",
                BirthDate = new DateTime(1982, 6, 5)
            });
            SeedData.Add(new Stewardess
            {
                Id = 2,
                FirstName = "Iryna",
                SecondName = "Moroz",
                BirthDate = new DateTime(1998, 11, 11)
            });
            SeedData.Add(new Stewardess
            {
                Id = 3,
                FirstName = "Larysa",
                SecondName = "Kovalchuk",
                BirthDate = new DateTime(1993, 6, 10)
            });
            SeedData.Add(new Stewardess
            {
                Id = 4,
                FirstName = "Karina",
                SecondName = "Voitovych",
                BirthDate = new DateTime(1989, 5, 5)
            });
        }

        public override async Task<bool> Update(Stewardess stewardess)
        {
            return await Task.Run(() =>
            {
                var oldStewardess = SeedData.FirstOrDefault(s => s.Id == stewardess.Id);
                if (oldStewardess == null)
                    return false;

                oldStewardess.FirstName = stewardess.FirstName;
                oldStewardess.SecondName = stewardess.SecondName;
                oldStewardess.BirthDate = stewardess.BirthDate;
                return true;
            });
        }
    }
}
