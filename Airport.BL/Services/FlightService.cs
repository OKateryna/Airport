using System.Collections.Generic;
using System.Linq;
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

        public FlightDto GetById(int id)
        {
            var flight = _unitOfWork.FlightRepository.Get(id);
            return GetFlightDto(flight);
        }

        public IEnumerable<FlightDto> GetAll()
        {
            var results = _unitOfWork.FlightRepository.GetAll();
            return results.Select(GetFlightDto);
        }

        public int Insert(EditableFlightFields createFlightRequest)
        {
            var entityToUpdate = _mapper.Map<Flight>(createFlightRequest);
            _unitOfWork.FlightRepository.Insert(entityToUpdate);
            _unitOfWork.FlightRepository.Save();

            return entityToUpdate.Id;
        }

        public bool Update(int id, EditableFlightFields updateFlightRequest)
        {
            var flightToUpdate = _mapper.Map<Flight>(updateFlightRequest);
            flightToUpdate.Id = id;
            return _unitOfWork.FlightRepository.Update(flightToUpdate);
        }

        public bool Delete(int id)
        {
            return _unitOfWork.FlightRepository.Delete(id);
        }

        private FlightDto GetFlightDto(Flight flight)
        {
            var ticket = _unitOfWork.TicketRepository.Get(flight.TicketId);
            var result = _mapper.Map<FlightDto>(flight);
            result.Ticket = _mapper.Map<TicketDto>(ticket);
            return result;
        }
    }
}