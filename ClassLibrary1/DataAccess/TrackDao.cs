using GranTurismoFramework;
using GranTurismoFramework.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class TrackDao
    {
        public List<TrackInfo> GetAllTrackInfo()
        {
            var tracks = new List<TrackInfo>();

            using (var context = new GranTurismoDb())
            {
                var query = from t in context.Tracks
                            join c in context.Courses on t.CourseId equals c.CourseId
                            join r in context.Regions on c.RegionId equals r.RegionId
                            select (new TrackInfo()
                            {
                                Course = c,
                                Track = t,
                                Region = r
                            });
                tracks = query.ToList();
            }

            return tracks;
        }
    }
}
