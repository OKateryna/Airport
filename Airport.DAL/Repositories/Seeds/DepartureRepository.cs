using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class DepartureRepository: BaseRepository<Departure>
    {
        public DepartureRepository()
        {
            SeedData.Add(new Departure
            {
                Id = 1,
                FlightId = 1,
                DepartureDate = new DateTime(2017, 12, 9, 12, 00, 00),
                CrewId = 3,
                PlaneId = 3
            });
            SeedData.Add(new Departure
            {
                Id = 2,
                FlightId = 2,
                DepartureDate = new DateTime(2017, 3, 23, 12, 00, 00),
                CrewId = 1,
                PlaneId = 4
            });
            SeedData.Add(new Departure
            {
                Id = 3,
                FlightId = 1,
                DepartureDate = new DateTime(2017, 5, 11, 1, 45, 00),
                CrewId = 2,
                PlaneId = 1
            });
            SeedData.Add(new Departure
            {
                Id = 4,
                FlightId = 4,
                DepartureDate = new DateTime(2017, 8, 5, 16, 45, 00),
                CrewId = 4,
                PlaneId = 2
            });
        }

        public override async Task<bool> Update(Departure departure)
        {
            return await Task.Run(() =>
            {
                var oldDeparture = SeedData.FirstOrDefault(d => d.Id == departure.Id);
                if (oldDeparture == null)
                    return false;

                oldDeparture.CrewId = departure.CrewId;
                oldDeparture.DepartureDate = departure.DepartureDate;
                oldDeparture.FlightId = departure.FlightId;
                oldDeparture.PlaneId = departure.PlaneId;

                return true;
            });
        }
    }
}
