using System.Collections.Generic;
using Airport.BL.Dto.PlaneType;

namespace Airport.BL.Abstractions
{
    public interface IPlaneTypeService
    {
        PlaneTypeDto GetById(int id);
        IEnumerable<PlaneTypeDto> GetAll();
        int Insert(EditablePlaneTypeFields createPlaneTypeRequest);
        bool Update(int id, EditablePlaneTypeFields updatePlaneTypeRequest);
        bool Delete(int id);
    }
}