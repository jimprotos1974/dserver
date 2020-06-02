using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace D.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Floor { get; set; }

        public int BuildingId { get; set; }
        public virtual Building Building { get; set; }

        public int AlternateBuildingId { get; set; }
        public virtual Building AlternateBuilding { get; set; }
    }
}