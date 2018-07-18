using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.BL.Dto.Plane;

namespace Airport.BL.Abstractions
{
    public interface IPlaneService
    {
        Task<PlaneDto> GetById(int id);
        Task<IEnumerable<PlaneDto>> GetAll();
        Task<int> Insert(EditablePlaneFields createPlaneRequest);
        Task<bool> Update(int id, EditablePlaneFields updatePlaneRequest);
        Task<bool> Delete(int id);
    }
}