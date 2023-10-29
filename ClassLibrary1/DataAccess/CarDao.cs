using GranTurismoFramework.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class CarDao
    {
        public List<CarInfoDto> GetAllCarInfo()
        {
            var cars = new List<CarInfoDto>();

            using (var db = new GranTurismoDb())
            {
                var query = from car in db.Cars
                            join manufacturer in db.Manufacturers on car.ManufacturerId equals manufacturer.ManufacturerId
                            join region in db.Regions on manufacturer.RegionId equals region.RegionId
                            select new CarInfoDto
                            {
                                Car = car,
                                Manufacturer = manufacturer,
                                Region = region,
                            };
                cars = query.ToList();
            }

            return cars;
        }
    }
}
