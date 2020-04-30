using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace D.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Country Country { get; set; }
    }
}