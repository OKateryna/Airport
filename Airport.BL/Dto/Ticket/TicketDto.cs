using Airport.BL.Dto.Flight;

namespace Airport.BL.Dto.Ticket
{
    public class TicketDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public FlightDto Flight { get; set; }
    }
}