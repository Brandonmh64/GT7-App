using GranTurismoFramework;
using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataTransfer
{
    public class CarInfoDto
    {
        public string CarName { get => Car.FullName; }
        public int CarId { get => Car.CarId; }

        public CarDto Car { get; set; }

        public ManufacturerDto Manufacturer { get; set; }

        public RegionDto Region { get; set; }
    }
}
