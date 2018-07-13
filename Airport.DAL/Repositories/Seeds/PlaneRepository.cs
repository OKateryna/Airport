using System;
using System.Collections.Generic;
using System.Linq;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class PlaneRepository: BaseRepository<Plane>
    {
        public PlaneRepository()
        {
            SeedData.Add(new Plane
            {
                Id = 1,
                PlaneName = "Sukhoi SuperJet",
                ManufectureDate = new DateTime(2009, 12, 5, 23, 15, 00),
                PlaneTypeId = 3,
                LifeSpan = new TimeSpan(3600, 0, 0, 0)
            });
            SeedData.Add(new Plane
            {
                Id = 2,
                PlaneName = "Airbus",
                ManufectureDate = new DateTime(2018, 12, 5, 23, 15, 00),
                PlaneTypeId = 2,
                LifeSpan = new TimeSpan(1800, 0, 0, 0)
            });
            SeedData.Add(new Plane
            {
                Id = 3,
                PlaneName = "Tupolev",
                ManufectureDate = new DateTime(2016, 5, 11, 7, 30, 00),
                PlaneTypeId = 4,
                LifeSpan = new TimeSpan(4200, 0, 0, 0)
            });
            SeedData.Add(new Plane
            {
                Id = 4,
                PlaneName = "Boeing",
                ManufectureDate = new DateTime(2000, 9, 7, 8, 00, 00),
                PlaneTypeId = 1,
                LifeSpan = new TimeSpan(7200, 0, 0, 0)
            });
        }

        public override bool Update(Plane plane)
        {
            var oldPlane = SeedData.FirstOrDefault(p => p.Id == plane.Id);
            if (oldPlane == null)
                return false;

            oldPlane.LifeSpan = plane.LifeSpan;
            oldPlane.ManufectureDate = plane.ManufectureDate;
            oldPlane.PlaneName = plane.PlaneName;
            oldPlane.PlaneTypeId = plane.PlaneTypeId;
            return true;
        }
    }
}
