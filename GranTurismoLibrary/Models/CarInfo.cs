﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Models
{
    internal class CarInfo
    {
        public int CarId { get; set; }
        public string FullName { get; set; }
        public string ImageName { get; set; }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
}