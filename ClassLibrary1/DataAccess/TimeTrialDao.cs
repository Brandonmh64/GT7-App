using GranTurismoFramework.DataTransfer.Simple;
using GranTurismoFramework.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class TimeTrialDao
    {
        public List<TimeTrialInfoDto> GetTimeTrials()
        {
            var timeTrials = new List<TimeTrialInfoDto>();

            using (var db = new GranTurismoDb())
            {
                var query = from tt in db.TimeTrials
                             join track in db.Tracks on tt.TrackId equals track.TrackId
                             join course in db.Courses on track.CourseId equals course.CourseId
                             join region in db.Regions on course.RegionId equals region.RegionId
                             join ownedCar in db.OwnedCars on tt.OwnedCarId equals ownedCar.OwnedCarId
                             join car in db.Cars on ownedCar.CarId equals car.CarId 
                             join manufacturer in db.Manufacturers on car.ManufacturerId equals manufacturer.ManufacturerId
                             join driver in db.Drivers on tt.DriverId equals driver.DriverId
                             join tune in db.Tunes on tt.TuneId equals tune.TuneId
                             join tiresF in db.TireTypes on tune.TiresFrontId equals tiresF.TireId
                             join tiresR in db.TireTypes on tune.TiresRearId equals tiresR.TireId
                             select (new TimeTrialInfoDto
                             {
                                 TimeTrialId = tt.TimeTrialId,
                                 SessionId = tt.SessionId,
                                 Time = tt.Time,
                                 TrackInfo = new TrackInfoDto()
                                 {
                                     Track = track,
                                     Course = course,
                                     Region = region,
                                 },
                                 OwnedCarInfo = new OwnedCarInfoDto()
                                 {
                                     PrimaryDriverId = ownedCar.PrimaryDriverId,
                                     PrimaryDriverName = driver.DriverName,
                                     ImageName = ownedCar.ImageName,
                                     Nickname = ownedCar.Nickname,
                                     Car = new CarDto()
                                     {
                                         CarId = tt.OwnedCarId,
                                         FullName = car.FullName,
                                         ManufacturerId = car.ManufacturerId,
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
                                 },
                                 Tune = new TuneInfoDto()
                                 {
                                     TuneId = tt.TuneId,
                                     OwnedCarId = ownedCar.OwnedCarId,
                                     SheetName = tune.SheetName,
                                     Notes = tune.Notes,
                                     PerformancePoints = tune.PerformancePoints,
                                     HorsePower = tune.HorsePower,
                                     Weight = tune.Weight,
                                     TiresFrontId = tune.TiresFrontId,
                                     TiresFront = tiresF.Abreviation,
                                     TiresRearId = tune.TiresRearId,
                                     TiresRear = tiresR.Abreviation
                                 },
                                 Driver = driver
                             });

                timeTrials = query.ToList();
            }

            return timeTrials;
        }

        /// <summary>
        /// Save all time trials in the provided list under the same new Session
        /// </summary>
        /// <param name="timeTrials"></param>
        public void SaveTimeTrials(List<TimeTrialInfoDto> timeTrials)
        {
            var sessionDao = new SessionDao();
            var sessionId = sessionDao.NewSession();
            var timeTrialObjects = new List<TimeTrial>();

            foreach (var timeTrialInfo in timeTrials)
            {
                var timeTrial = new TimeTrial()
                {
                    SessionId = sessionId,
                    OwnedCarId = timeTrialInfo.OwnedCarInfo.OwnedCarId,
                    TuneId = timeTrialInfo.Tune.TuneId,
                    DriverId = timeTrialInfo.Driver.DriverId,
                    TrackId = timeTrialInfo.TrackInfo.Track.TrackId,
                    Time = timeTrialInfo.Time,
                };

                timeTrialObjects.Add(timeTrial);
            }

            using (var db = new GranTurismoDb())
            {
                foreach (var tt in timeTrialObjects)
                {
                    db.TimeTrials.Add(tt);
                }

                db.SaveChanges();
            }
        }
    }
}
