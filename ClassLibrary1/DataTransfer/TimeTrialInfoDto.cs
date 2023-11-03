using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GranTurismoFramework.DataTransfer;

namespace GranTurismoFramework.DataTransfer
{
    public class TimeTrialInfoDto
    {
        public int TimeTrialId { get; set; }
        public int SessionId { get; set; }

        public System.TimeSpan Time { get; set; }

        public TrackInfoDto TrackInfo { get; set; }

        public OwnedCarInfoDto OwnedCarInfo { get; set; }
        public TuneInfoDto Tune { get; set; }
        public Driver Driver { get; set; }
    }
}
