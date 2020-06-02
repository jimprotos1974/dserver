using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace D.Models
{
    public class DtoRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public DtoBuilding Building { get; set; }
    }
}