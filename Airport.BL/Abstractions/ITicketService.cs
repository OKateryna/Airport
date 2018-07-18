using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.BL.Dto.Ticket;

namespace Airport.BL.Abstractions
{
    public interface ITicketService
    {
        Task<TicketDto> GetById(int id);
        Task<IEnumerable<TicketDto>> GetAll();
        Task<int> Insert(EditableTicketFields createTicketRequest);
        Task<bool> Update(int id, EditableTicketFields updateTicketRequest);
        Task<bool> Delete(int id);
    }
}