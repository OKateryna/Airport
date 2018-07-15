using System;
using System.Linq;
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
                LifeSpan = 10
            });
            SeedData.Add(new Plane
            {
                Id = 2,
                PlaneName = "Airbus",
                ManufectureDate = new DateTime(2018, 12, 5, 23, 15, 00),
                PlaneTypeId = 2,
                LifeSpan = 6
            });
            SeedData.Add(new Plane
            {
                Id = 3,
                PlaneName = "Tupolev",
                ManufectureDate = new DateTime(2016, 5, 11, 7, 30, 00),
                PlaneTypeId = 4,
                LifeSpan = 12
            });
            SeedData.Add(new Plane
            {
                Id = 4,
                PlaneName = "Boeing",
                ManufectureDate = new DateTime(2000, 9, 7, 8, 00, 00),
                PlaneTypeId = 1,
                LifeSpan = 20
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
