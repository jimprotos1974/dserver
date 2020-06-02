using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace D.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Room> AlternateRooms { get; set; }
    }
}