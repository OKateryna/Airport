using System.Collections.Generic;
using Airport.BL.Dto.Plane;

namespace Airport.BL.Abstractions
{
    public interface IPlaneService
    {
        PlaneDto GetById(int id);
        IEnumerable<PlaneDto> GetAll();
        int Insert(EditablePlaneFields createPlaneRequest);
        bool Update(int id, EditablePlaneFields updatePlaneRequest);
        bool Delete(int id);
    }
}