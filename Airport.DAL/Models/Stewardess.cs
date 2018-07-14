using System;
using System.Collections.Generic;

namespace Airport.DAL.Models
{
    public class Stewardess : Entity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
