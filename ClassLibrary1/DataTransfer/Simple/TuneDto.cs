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
        public double HorsePower { get; set; }
        public double Weight { get; set; }

        public int TiresFrontId { get; set; }
        public int TiresRearId { get; set; }

        public string SheetName { get; set; }
        public string Notes { get; set; }
    }
}
