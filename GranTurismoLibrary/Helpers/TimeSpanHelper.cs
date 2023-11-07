using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Helpers
{
    public class TimeSpanHelper
    {
        public static TimeSpan AssembleTimeSpan(string minutes, string seconds, string milliSeconds)
        {

            var milliString = milliSeconds;

            if (milliSeconds.Length != 3)
            {
                if (milliSeconds.Length == 1)
                {
                    milliString = $"00{milliSeconds}";
                }
                else if (milliSeconds.Length == 2)
                {
                    milliString = $"0{milliSeconds}";
                }
            }

            var time = $"0:{minutes}:{seconds}.{milliString}";
            var parsedTime = TimeSpan.Parse(time);

            return parsedTime;
        }

        public static string GetTimeString3Milliseconds(TimeSpan time)
        {
            var timeSpan = time.ToString("c");
            var timeString = "";

            if (timeSpan.Length > 12)
            {
                timeString = timeSpan.Substring(3, timeSpan.Length - 7);
            }
            else
            {
                timeString = timeSpan.Substring(3, timeSpan.Length - 3);
                timeString = $"{timeString}.000";
            }

            return timeString;
        }

        public static string GetTimeStringNoMinutes(TimeSpan time)
        {
            var timeString = GetTimeString3Milliseconds(time);

            return timeString.Substring(3, timeString.Length - 3);
        }
    }
}
