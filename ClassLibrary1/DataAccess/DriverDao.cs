using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class DriverDao
    {
        public List<DriverDto> GetDrivers()
        {
            var drivers = new List<DriverDto>();

            using (var db = new GranTurismoDb())
            {
                var driverList = db.Drivers.ToList();
                drivers = FwMapper.MapList<Driver, DriverDto>(driverList);
            }

            return drivers;
        }
    }
}
