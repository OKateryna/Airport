using System.ComponentModel.DataAnnotations;

namespace Airport.DAL.Models
{
    public class PlaneType : Entity
    {
        [Required]
        public string PlaneModel { get; set; }
        [Required]
        public int PlacesAmount { get; set; }
        [Required]
        public int CarryingCapacity { get; set; }
    }
}
