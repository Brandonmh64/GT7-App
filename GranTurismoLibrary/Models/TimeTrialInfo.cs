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
        public System.TimeSpan Time { get; set; }

        public TrackInfo Track { get; set; }

        public CourseDto Course { get; set; }

        public RegionDto Region { get; set; }


        public OwnedCarInfo OwnedCarInfo { get; set; }

        public TuneInfo TuneInfo { get; set; }

        public DriverDto Driver { get; set; }
    }
}
