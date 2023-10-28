using GranTurismoFramework;
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
                                 TrackInfo = new TrackInfo()
                                 {
                                     Track = track,
                                     Course = course,
                                     Region = region,
                                 },
                                 CarInfo = new CarInfo()
                                 {
                                     Car = c,
                                     Manufacturer = m,
                                 },
                                 Tune = tune,
                             });

                timeTrials = query.ToList();
            }

            return timeTrials;
        }
    }
}
