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
                             join c in context.Cars on tt.CarId equals c.CarId
                             join m in context.Manufacturers on c.ManufacturerId equals m.ManufacturerId
                             join tune in context.Tunes on tt.TuneId equals tune.TuneId
                             join track in context.Tracks on tt.TrackId equals track.TrackId
                             join course in context.Courses on track.TrackId equals course.CourseId
                             join region in context.Regions on course.RegionId equals region.RegionId
                             select (new TimeTrialInfoDto
                             {
                                 TimeTrial = tt,
                                 TrackInfo = new TrackInfoDto()
                                 {
                                     Track = track,
                                     Course = course,
                                     Region = region,
                                 },
                                 CarInfo = new CarInfoDto()
                                 {
                                     Car = new CarDto()
                                     {
                                         CarId = tt.CarId,
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
    }
}
