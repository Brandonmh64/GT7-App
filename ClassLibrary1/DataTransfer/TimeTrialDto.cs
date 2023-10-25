using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GranTurismoFramework.DataTransfer
{
    public class TimeTrialDto
    {
        public TimeTrial TimeTrial { get; set; }

        public TrackDto Track { get; set; }

        // Car
        public Car Car { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Tune Tune { get; set; }
    }
}
