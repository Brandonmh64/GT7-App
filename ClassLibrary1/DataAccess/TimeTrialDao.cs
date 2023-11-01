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

            using (var context = new GranTurismoDb())
            {
                var query = from tt in context.TimeTrials
                             join ownedCar in context.OwnedCars on tt.OwnedCarId equals ownedCar.CarId
                             join d in context.Drivers on ownedCar.PrimaryDriverId equals d.DriverId
                             join c in context.Cars on ownedCar.CarId equals c.CarId
                             join m in context.Manufacturers on c.ManufacturerId equals m.ManufacturerId
                             join tune in context.Tunes on tt.TuneId equals tune.TuneId
                             join track in context.Tracks on tt.TrackId equals track.TrackId
                             join course in context.Courses on track.TrackId equals course.CourseId
                             join region in context.Regions on course.RegionId equals region.RegionId
                             select (new TimeTrialInfoDto
                             {
                                 TrackInfo = new TrackInfoDto()
                                 {
                                     Track = track,
                                     Course = course,
                                     Region = region,
                                 },
                                 OwnedCarInfo = new OwnedCarInfoDto()
                                 {
                                     PrimaryDriverId = ownedCar.PrimaryDriverId,
                                     PrimaryDriverName = d.DriverName,
                                     ImageName = ownedCar.ImageName,
                                     Nickname = ownedCar.Nickname,
                                     Car = new CarDto()
                                     {
                                         CarId = tt.OwnedCarId,
                                         FullName = c.FullName,
                                         ManufacturerId = c.ManufacturerId,
                                     },
                                     Manufacturer = new ManufacturerDto()
                                     {
                                         ManufacturerId = m.ManufacturerId,
                                         Name = m.Name,
                                         RegionId = region.RegionId,
                                     },
                                     Region = new RegionDto()
                                     {
                                         RegionId = region.RegionId,
                                         Name = region.Name,
                                     }
                                 },
                                 Tune = tune,
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
                    DriverId = timeTrialInfo.DriverId,
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
