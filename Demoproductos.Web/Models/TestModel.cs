﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demoproductos.Web.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set;}
        public DateTime Creation { get; set; }
        public DateTime Modification { get; set; }

    }
}
