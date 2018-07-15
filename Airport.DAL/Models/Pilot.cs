using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport.DAL.Models
{
    public class Pilot : Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Expierence { get; set; }
        public virtual ICollection<CrewPilot> CrewPilots { get; set; }
    }
}
