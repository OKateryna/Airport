using System.Collections.Generic;
using Airport.BL.Dto.Crew;

namespace Airport.BL.Abstractions
{
    public interface ICrewService
    {
        CrewDto GetById(int id);
        IEnumerable<CrewDto> GetAll();
        int Insert(EditableCrewFields createCrewRequest);
        bool Update(int id, EditableCrewFields updateCrewRequest);
        bool Delete(int id);
    }
}