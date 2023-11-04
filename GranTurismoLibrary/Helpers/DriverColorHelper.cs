using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Helpers
{
    public class DriverColorHelper
    {
        public static Dictionary<int, Color> IdDictionary { get; set; } = new Dictionary<int, Color>()
        {
            { 1, Color.Transparent },
            { 2, Brandon },
            { 3, Bryant },
        };


        public static Color Brandon { get => Color.FromArgb(115, 79, 149); }
        public static Color Bryant { get => Color.FromArgb(0, 56, 167); }
    }
}
