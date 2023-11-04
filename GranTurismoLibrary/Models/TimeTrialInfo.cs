using GranTurismoFramework;
using GranTurismoFramework.DataTransfer.Simple;
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
        public string TimeString { get => GetTimeString(); }

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


        private string GetTimeString()
        {
            var timeSpan = Time.ToString("c");

            if (timeSpan.Length > 8)
            {
                return timeSpan.Substring(3, timeSpan.Length - 7);
            }
            else
            {
                return timeSpan.Substring(3, timeSpan.Length - 3);
            }
        }
    }
}
