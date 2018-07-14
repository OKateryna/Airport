using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Ticket : Entity
    {
        public double Price { get; set; }
        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
