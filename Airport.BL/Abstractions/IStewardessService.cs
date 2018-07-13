using System.Collections.Generic;
using Airport.BL.Dto.Stewardess;

namespace Airport.BL.Abstractions
{
    public interface IStewardessService
    {
        StewardessDto GetById(int id);
        IEnumerable<StewardessDto> GetAll();
        int Insert(EditableStewardessFields createStewardessRequest);
        bool Update(int id, EditableStewardessFields updateStewardessRequest);
        bool Delete(int id);
    }
}