using GranTurismoFramework;
using GranTurismoFramework.DataAccess;
using GranTurismoFramework.DataTransfer;
using GranTurismoFramework.DataTransfer.Simple;
using GranTurismoLibrary.Helpers;
using GranTurismoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.DataAccess
{
    public class CarDao
    {
        private GranTurismoFramework.DataAccess.CarDao _carDao { get => new GranTurismoFramework.DataAccess.CarDao(); }
        private GranTurismoFramework.DataAccess.OwnedCarDao _ownedCarDao { get => new GranTurismoFramework.DataAccess.OwnedCarDao(); }


        /* All Cars */

        public List<CarInfoDto> GetAllCarInfo()
        {
            return _carDao.GetAllCarInfo();
        }

        public CarDto GetCar(int carId)
        {
            var carMatch = GetAllCarInfo().FirstOrDefault(car => car.CarId == carId);

            return new CarDto()
            {
                CarId = carId,
                FullName = carMatch?.CarName ?? "",
                ManufacturerId = carMatch?.Manufacturer.ManufacturerId ?? 0,
            };
        }


        /* Owned Cars */

        public List<OwnedCarInfo> GetAllOwnedCars()
        {
            var ownedCars = _ownedCarDao.GetAllOwnedCars();

            return GtMapper.MapList<OwnedCarInfoDto, OwnedCarInfo>(ownedCars);
        }

        public void SaveNewOwnedcar(OwnedCarInfo ownedCar)
        {
            var dto = GtMapper.Map<OwnedCarInfo, OwnedCarInfoDto>(ownedCar);
            _ownedCarDao.SaveOwnedCar(dto);
        }


        /* Car Properties */

        public List<DriverDto> GetAllDrivers()
        {
            var driverDao = new DriverDao();
            return driverDao.GetDrivers();
        }

        public List<GranTurismoFramework.DataTransfer.Simple.TireTypeDto> GetTireTypes()
        {
            var tireDao = new TireTypeDao();
            var typeList = tireDao.GetTireTypes();

            return GtMapper.MapList<TireType, GranTurismoFramework.DataTransfer.Simple.TireTypeDto>(typeList);
        }


    }
}
