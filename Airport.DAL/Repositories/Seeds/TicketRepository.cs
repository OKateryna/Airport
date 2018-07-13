using System.Collections.Generic;
using System.Linq;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public class TicketRepository: BaseRepository<Ticket>
    {
        public TicketRepository()
        {
            SeedData.Add(new Ticket { Id = 1, FlightId = 1, Price = 112 });
            SeedData.Add(new Ticket { Id = 2, FlightId = 2, Price = 212 });
            SeedData.Add(new Ticket {Id = 3, FlightId = 4, Price = 222.2});
            SeedData.Add(new Ticket {Id = 4, FlightId = 3, Price = 100});
        }
        
        public override bool Update(Ticket ticket)
        {
            var oldPlane = SeedData.FirstOrDefault(p => p.Id == ticket.Id);
            if (oldPlane == null)
                return false;

            oldPlane.Price = ticket.Price;
            oldPlane.FlightId = ticket.FlightId;

            return true;
        }
    }
}
