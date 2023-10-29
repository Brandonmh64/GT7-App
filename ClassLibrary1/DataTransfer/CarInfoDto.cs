using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataTransfer
{
    public class CarInfoDto
    {
        public Car Car { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public Region Region { get; set; }
    }
}
