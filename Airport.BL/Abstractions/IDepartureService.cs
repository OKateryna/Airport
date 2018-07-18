using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.BL.Dto.Departure;

namespace Airport.BL.Abstractions
{
    public interface IDepartureService
    {
        Task<DepartureDto> GetById(int id);
        Task<IEnumerable<DepartureDto>> GetAll();
        Task<int> Insert(EditableDepartureFields createDepartureRequest);
        Task<bool> Update(int id, EditableDepartureFields updateDepartureRequest);
        Task<bool> Delete(int id);
    }
}