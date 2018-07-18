using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.BL.Dto.Crew;

namespace Airport.BL.Abstractions
{
    public interface ICrewService
    {
        Task<CrewDto> GetById(int id);
        Task<IEnumerable<CrewDto>> GetAll();
        Task<int> Insert(EditableCrewFields createCrewRequest);
        Task<bool> Update(int id, EditableCrewFields updateCrewRequest);
        Task<bool> Delete(int id);
    }
}