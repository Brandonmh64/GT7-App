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
        public List<CarInfo> GetAllCarInfo()
        {
            var cars = new List<CarInfo>();

            using (var db = new GranTurismoDb())
            {
                var query = from car in db.Cars
                            join manufacturer in db.Manufacturers on car.ManufacturerId equals manufacturer.ManufacturerId
                            select new CarInfo
                            {
                                Car = car,
                                Manufacturer = manufacturer,
                            };
                cars = query.ToList();
            }

            return cars;
        }
    }
}
