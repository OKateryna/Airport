using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport.DAL.Models
{
    public class Stewardess : Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public virtual ICollection<CrewStewardess> CrewStewardesses { get; set; }
    }
}
