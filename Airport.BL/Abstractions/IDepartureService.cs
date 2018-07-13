using System.Collections.Generic;
using Airport.BL.Dto.Departure;

namespace Airport.BL.Abstractions
{
    public interface IDepartureService
    {
        DepartureDto GetById(int id);
        IEnumerable<DepartureDto> GetAll();
        int Insert(EditableDepartureFields createDepartureRequest);
        bool Update(int id, EditableDepartureFields updateDepartureRequest);
        bool Delete(int id);
    }
}