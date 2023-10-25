using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class CarDao
    {
        public List<Car> GetCars()
        {
            var cars = new List<Car>();

            using (var context = new GranTurismoDb())
            {
                cars = context.Cars.ToList();
            }

            return cars;
        }
    }
}
