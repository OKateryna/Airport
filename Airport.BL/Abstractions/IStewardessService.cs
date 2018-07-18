using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.BL.Dto.Stewardess;

namespace Airport.BL.Abstractions
{
    public interface IStewardessService
    {
        Task<StewardessDto> GetById(int id);
        Task<IEnumerable<StewardessDto>> GetAll();
        Task<int> Insert(EditableStewardessFields createStewardessRequest);
        Task<bool> Update(int id, EditableStewardessFields updateStewardessRequest);
        Task<bool> Delete(int id);
    }
}