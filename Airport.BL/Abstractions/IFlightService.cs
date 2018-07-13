using System.Collections.Generic;
using Airport.BL.Dto.Flight;

namespace Airport.BL.Abstractions
{
    public interface IFlightService
    {
        FlightDto GetById(int id);
        IEnumerable<FlightDto> GetAll();
        int Insert(EditableFlightFields createFlightRequest);
        bool Update(int id, EditableFlightFields updateFlightRequest);
        bool Delete(int id);
    }
}