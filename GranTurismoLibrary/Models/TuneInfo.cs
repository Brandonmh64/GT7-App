using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Models
{
    public class TuneInfo
    {
        public int TuneId { get; set; }
        public int OwnedCarId { get; set; }
        public string SheetName { get; set; }
        public string Notes { get; set; }

        public double PerformancePoints { get; set; }
        public string PPText { get => GetDoubleStringReduced(PerformancePoints); }
        public double HorsePower { get; set; }
        public string HPText { get => GetDoubleStringReduced(HorsePower); }
        public double Weight { get; set; }
        public string WeightText { get => GetDoubleStringReduced(Weight); }

        public int TiresFrontId { get; set; }
        public string TiresFront { get; set; }
        public int TiresRearId { get; set; }
        public string TiresRear { get; set; }


        private string GetDoubleStringReduced(double value)
        {
            var valueString = value.ToString();
            if (valueString.Contains('.'))
            {
                var decimalSplit = valueString.Split('.');

                return $"{decimalSplit[0]}.{decimalSplit[1].Substring(0, 2)}";
            }

            return valueString;
        }
    }
}
