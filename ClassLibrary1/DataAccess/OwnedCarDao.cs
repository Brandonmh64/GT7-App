using GranTurismoFramework.DataTransfer;
using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class OwnedCarDao
    {
        public List<OwnedCarInfoDto> GetAllOwnedCars()
        {
            var carDao = new CarDao();
            var allCars = carDao.GetAllCarInfo();

            var ownedCarList = new List<OwnedCarInfoDto>();

            using (var db = new GranTurismoDb())
            {
                var query = from owned in db.OwnedCars
                            join driver in db.Drivers on owned.PrimaryDriverId equals driver.DriverId
                            join all in db.Cars on owned.CarId equals all.CarId
                            select new OwnedCarInfoDto
                            {
                                OwnedCarId = owned.OwnedCarId,
                                PrimaryDriverId = driver.DriverId,
                                PrimaryDriverName = driver.DriverName,
                                ImageName = owned.ImageName,
                                Nickname = owned.Nickname,
                                Car = new CarDto()
                                {
                                    CarId = owned.CarId,
                                    FullName = all.FullName,
                                    ManufacturerId = all.ManufacturerId,
                                },
                            };
                ownedCarList = query.ToList();

                foreach (var ownedCar in ownedCarList)
                {
                    var carMatch = allCars.FirstOrDefault(car => car.Car.CarId == ownedCar.Car.CarId);

                    if (carMatch != null)
                    {
                        ownedCar.Manufacturer = carMatch.Manufacturer;
                        ownedCar.Region = carMatch.Region;
                    }
                }
            }

            return ownedCarList;
        }

        public void SaveOwnedCar(OwnedCarInfoDto ownedcarInfo)
        {
            var ownedCar = FwMapper.Map<OwnedCarInfoDto, OwnedCar>(ownedcarInfo);

            using (var db = new GranTurismoDb())
            {
                db.OwnedCars.Add(ownedCar);
                db.SaveChanges();
            }
        }
    }
}
