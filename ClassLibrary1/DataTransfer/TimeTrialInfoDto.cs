﻿using System;
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
        public TimeTrial TimeTrial { get; set; }
        public TrackInfo TrackInfo { get; set; }

        public CarInfo CarInfo { get; set; }
        public Tune Tune { get; set; }
    }
}