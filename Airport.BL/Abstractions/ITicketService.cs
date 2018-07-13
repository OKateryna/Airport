using System.Collections.Generic;
using Airport.BL.Dto.Ticket;

namespace Airport.BL.Abstractions
{
    public interface ITicketService
    {
        TicketDto GetById(int id);
        IEnumerable<TicketDto> GetAll();
        int Insert(EditableTicketFields createTicketRequest);
        bool Update(int id, EditableTicketFields updateTicketRequest);
        bool Delete(int id);
    }
}