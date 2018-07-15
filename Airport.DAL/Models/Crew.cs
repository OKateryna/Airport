using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Crew : Entity
    {
        [Required]
        public string Name { get; set; }
        public virtual CrewPilot CrewPilot { get; set; }
        public virtual ICollection<CrewStewardess> CrewStewardesses { get; set; }
    }
}
