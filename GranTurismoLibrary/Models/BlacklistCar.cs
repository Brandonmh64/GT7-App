using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GranTurismoLibrary.Models
{
    public class BlacklistCar
    {
        public Image? Image { get; set; }

        public string Name { get; set; }

        public string Performance { get; set; }

        public string Time { get; set; }


        public BlacklistCar()
        {
            Name = "No Data";
            Performance = "0 PP - CH";
            Time = "00:00.000";
        }
    }
}
