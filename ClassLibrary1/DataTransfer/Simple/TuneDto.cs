using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataTransfer.Simple
{
    public class TuneDto
    {
        public int TuneId { get; set; }
        public int CarId { get; set; }

        public double PerformancePoints { get; set; }
        public string PPText { get => PerformancePoints.ToString(); }
        public double HorsePower { get; set; }
        public string HPText { get => HorsePower.ToString(); }
        public double Weight { get; set; }
        public string WeightText { get => Weight.ToString(); }

        public int TiresFrontId { get; set; }
        public string TiresFront { get; set; }
        public int TiresRearId { get; set; }
        public string TiresRear { get; set; }

        public string SheetName { get; set; }
        public string Notes { get; set; }
    }
}
