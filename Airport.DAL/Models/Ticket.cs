namespace Airport.DAL.Models
{
    public class Ticket : Entity
    {
        public double Price { get; set; }
        public int FlightId { get; set; }
    }
}
