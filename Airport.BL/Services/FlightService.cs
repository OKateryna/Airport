using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Flight;
using Airport.BL.Dto.Ticket;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;

namespace Airport.BL.Services
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlightService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FlightDto> GetById(int id)
        {
            var flight = await _unitOfWork.FlightRepository.Get(id);
            return await GetFlightDto(flight);
        }

        public async Task<IEnumerable<FlightDto>> GetAll()
        {
            var flights = await _unitOfWork.FlightRepository.GetAll();
            var flightDtoTasks = await Task.WhenAll(flights.Select(GetFlightDto));

            return flightDtoTasks.Where(f => f != null);
        }

        public async Task<int> Insert(EditableFlightFields createFlightRequest)
        {
            var entityToInsert = _mapper.Map<Flight>(createFlightRequest);
            
            await _unitOfWork.FlightRepository.Insert(entityToInsert);
            await _unitOfWork.SaveChangesAsync();

            await UpdateFlight(entityToInsert, createFlightRequest);

            return entityToInsert.Id;
        }

        private async Task<bool> UpdateFlight(Flight flight, EditableFlightFields editableFlightFields)
        {
            var tickets =
                (await _unitOfWork.TicketRepository.GetAll()).Where(x => editableFlightFields.TicketIds.Contains(x.Id));
            flight.Tickets = tickets.ToList();
            var updateResult = await _unitOfWork.FlightRepository.Update(flight);
            await _unitOfWork.SaveChangesAsync();

            return updateResult;
        }

        public async Task<bool> Update(int id, EditableFlightFields updateFlightRequest)
        {
            var flightToUpdate = _mapper.Map<Flight>(updateFlightRequest);
            flightToUpdate.Id = id;
            var result = await UpdateFlight(flightToUpdate, updateFlightRequest);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.FlightRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        private async Task<FlightDto> GetFlightDto(Flight flight)
        {
            var tickets = (await _unitOfWork.TicketRepository.GetAll()).Where(x => x.FlightId == flight.Id);
            var result = _mapper.Map<FlightDto>(flight);
            result.Tickets = tickets.Select(ticket => _mapper.Map<TicketDto>(ticket)).ToList();
            return result;
        }
    }
}