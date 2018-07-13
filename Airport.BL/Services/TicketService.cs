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
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public TicketDto GetById(int id)
        {
            var ticket = _unitOfWork.TicketRepository.Get(id);
            return GetTicketDto(ticket);
        }

        public IEnumerable<TicketDto> GetAll()
        {
            var results = _unitOfWork.TicketRepository.GetAll();
            return results.Select(GetTicketDto);
        }

        public int Insert(EditableTicketFields editableTicketFields)
        {
            return _unitOfWork.TicketRepository.Insert(_mapper.Map<Ticket>(editableTicketFields));
        }

        public bool Update(int id, EditableTicketFields editableTicketFields)
        {
            var ticketToUpdate = _mapper.Map<Ticket>(editableTicketFields);
            ticketToUpdate.Id = id;
            return _unitOfWork.TicketRepository.Update(ticketToUpdate);
        }

        public bool Delete(int id)
        {
            return _unitOfWork.TicketRepository.Delete(id);
        }

        private TicketDto GetTicketDto(Ticket ticket)
        {
            var flight = _unitOfWork.FlightRepository.Get(ticket.FlightId);
            var result = _mapper.Map<TicketDto>(ticket);
            result.Flight = _mapper.Map<FlightDto>(flight);
            return result;
        }
    }
}