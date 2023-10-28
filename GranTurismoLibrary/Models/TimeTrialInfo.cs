using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Models
{
    internal class TimeTrialInfo
    {
        public System.TimeSpan Time { get; set; }

        public string TrackName { get; set; }

        public string CourseName { get; set; }

        public string RegionName { get; set; }

        CarInfo CarInfo { get; set; }
    }
}
