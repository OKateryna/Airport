using System.Linq;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class PlaneTypeRepository: BaseRepository<PlaneType>
    {
        public PlaneTypeRepository()
        {
            SeedData.Add(new PlaneType { Id = 1, PlaneModel = "777", PlacesAmount = 114, CarryingCapacity = 52800 });
            SeedData.Add(new PlaneType { Id = 2, PlaneModel = "A320", PlacesAmount = 40, CarryingCapacity = 15000 });
            SeedData.Add(new PlaneType { Id = 3, PlaneModel = "100", PlacesAmount = 300, CarryingCapacity = 30000 });
            SeedData.Add(new PlaneType {Id = 4, PlaneModel = "Ту-134", PlacesAmount = 80, CarryingCapacity = 47000});
        }

        public override bool Update(PlaneType planeType)
        {
            var oldPlaneType = SeedData.FirstOrDefault(pt => pt.Id == planeType.Id);
            if (oldPlaneType == null)
                return false;

            oldPlaneType.CarryingCapacity = planeType.CarryingCapacity;
            oldPlaneType.PlacesAmount = planeType.PlacesAmount;
            oldPlaneType.PlaneModel = planeType.PlaneModel;

            return true;
        }
    }
}
