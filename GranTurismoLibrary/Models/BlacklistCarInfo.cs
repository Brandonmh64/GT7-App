using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GranTurismoFramework;

namespace GranTurismoLibrary.Models
{
    public class BlacklistCarInfo
    {
        public Image? Image { get; set; }

        public string Name { get; set; }

        public string Performance { get; set; }

        public string Time { get; set; }

        public Tune Tune { get; set; }


        public BlacklistCarInfo()
        {
            Name = "No Data";
            Performance = "0 PP - CH";
            Time = "00:00.000";
        }
    }
}
