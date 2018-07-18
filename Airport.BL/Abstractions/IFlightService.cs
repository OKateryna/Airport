using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.BL.Dto.Flight;

namespace Airport.BL.Abstractions
{
    public interface IFlightService
    {
        Task<FlightDto> GetById(int id);
        Task<IEnumerable<FlightDto>> GetAll();
        Task<int> Insert(EditableFlightFields createFlightRequest);
        Task<bool> Update(int id, EditableFlightFields updateFlightRequest);
        Task<bool> Delete(int id);
    }
}