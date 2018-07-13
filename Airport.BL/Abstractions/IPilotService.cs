using System.Collections.Generic;
using Airport.BL.Dto.Pilot;

namespace Airport.BL.Abstractions
{
    public interface IPilotService
    {
        PilotDto GetById(int id);
        IEnumerable<PilotDto> GetAll();
        int Insert(EditablePilotFields createPilotRequest);
        bool Update(int id, EditablePilotFields updatePilotRequest);
        bool Delete(int id);
    }
}