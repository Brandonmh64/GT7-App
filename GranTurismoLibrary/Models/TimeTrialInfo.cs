using GranTurismoFramework;
using GranTurismoFramework.DataTransfer.Simple;
using GranTurismoLibrary.DataAccess;
using GranTurismoLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Models
{
    public class TimeTrialInfo
    {
        public int TimeTrialId { get; set; }
        public int SessionId { get; set; }

        public System.TimeSpan Time { get; set; }
        public string TimeString { get => TimeSpanHelper.GetTimeString3Milliseconds(Time); }

        public TrackInfo Track { get; set; }
        public CourseDto Course { get; set; }
        public RegionDto Region { get; set; }


        public OwnedCarInfo OwnedCarInfo { get; set; }
        public string OwnedCarName { get => OwnedCarInfo.Nickname; }
        public string CarModel { get => OwnedCarInfo.FullName; }


        public TuneInfo TuneInfo { get; set; }
        public string SheetName { get => TuneInfo.SheetName; }
        public string PP { get => TuneInfo.PerformancePoints.ToString("F2"); }
        public string TireCombo { get => $"{TuneInfo.TiresFront.Substring(0, 2)}/{TuneInfo.TiresRear.Substring(0, 2)}"; }
        

        public DriverDto Driver { get; set; }
        public string DriverName { get => Driver.DriverName; }

        
        private static TimeTrialDao _ttDao { get => new TimeTrialDao(); }
        public int ProjectedTopTenRank { get => _ttDao.GetTrackTopChartPosition(Track.TrackId, Time); }
        public TimeSpan TimeToNextTopRank { get => _ttDao.GetTimeAwayFromNextBestRecord(Track.TrackId, Time); }
        public string TopRankProjection { get => $"{ProjectedTopTenRank}, +{TimeSpanHelper.GetTimeStringNoMinutes(TimeToNextTopRank)}"; }
    }
}
