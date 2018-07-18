using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.BL.Dto.PlaneType;

namespace Airport.BL.Abstractions
{
    public interface IPlaneTypeService
    {
        Task<PlaneTypeDto> GetById(int id);
        Task<IEnumerable<PlaneTypeDto>> GetAll();
        Task<int> Insert(EditablePlaneTypeFields createPlaneTypeRequest);
        Task<bool> Update(int id, EditablePlaneTypeFields updatePlaneTypeRequest);
        Task<bool> Delete(int id);
    }
}