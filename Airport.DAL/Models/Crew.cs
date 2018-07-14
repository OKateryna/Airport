using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.DAL.Models
{
    public class Crew : Entity
    {
        public string Name { get; set; }
        public virtual CrewPilot CrewPilot { get; set; }
        public virtual IEnumerable<CrewStewardess> CrewStewardesses { get; set; }
    }
}
