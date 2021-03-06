﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace D.Models
{
    public class DtoBuilding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<DtoRoom> Rooms { get; set; }
    }
}