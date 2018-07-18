using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.BL.Dto.Pilot;

namespace Airport.BL.Abstractions
{
    public interface IPilotService
    {
        Task<PilotDto> GetById(int id);
        Task<IEnumerable<PilotDto>> GetAll();
        Task<int> Insert(EditablePilotFields createPilotRequest);
        Task<bool> Update(int id, EditablePilotFields updatePilotRequest);
        Task<bool> Delete(int id);
    }
}