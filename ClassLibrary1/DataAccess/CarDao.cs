using GranTurismoFramework.DataTransfer;
using GranTurismoFramework.DataTransfer.Simple;
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
                                Car = new CarDto()
                                {
                                    CarId = car.CarId,
                                    ManufacturerId = manufacturer.ManufacturerId,
                                    FullName = car.FullName,
                                },
                                Manufacturer = new ManufacturerDto()
                                {
                                    ManufacturerId = manufacturer.ManufacturerId,
                                    Name = manufacturer.Name,
                                    RegionId = region.RegionId,
                                },
                                Region = new RegionDto()
                                {
                                    RegionId = region.RegionId,
                                    Name = region.Name,
                                }
                            };
                cars = query.ToList();
            }

            return cars;
        }
    }
}
