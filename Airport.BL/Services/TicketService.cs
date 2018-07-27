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
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TicketDto> GetById(int id)
        {
            var ticket = await _unitOfWork.TicketRepository.Get(id);
            return await GetTicketDto(ticket);
        }

        public async Task<IEnumerable<TicketDto>> GetAll()
        {
            var tickets = await _unitOfWork.TicketRepository.GetAll();
            var ticketsDtos = await Task.WhenAll(tickets.Select(GetTicketDto));

            return ticketsDtos.Where(t => t != null);
        }

        public async Task<int> Insert(EditableTicketFields editableTicketFields)
        {
            var entityToUpdate = _mapper.Map<Ticket>(editableTicketFields);
            await _unitOfWork.TicketRepository.Insert(entityToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return entityToUpdate.Id;
        }

        public async Task<bool> Update(int id, EditableTicketFields editableTicketFields)
        {
            var ticketToUpdate = _mapper.Map<Ticket>(editableTicketFields);
            ticketToUpdate.Id = id;
            var result = await _unitOfWork.TicketRepository.Update(ticketToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.TicketRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        private async Task<TicketDto> GetTicketDto(Ticket ticket)
        {
            var flight = await _unitOfWork.FlightRepository.Get(ticket.FlightId);
            var result = _mapper.Map<TicketDto>(ticket);
            result.Flight = _mapper.Map<FlightDto>(flight);
            return result;
        }
    }
}