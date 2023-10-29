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

        public int CarId { get; set; }
        public double PerformancePoints { get; set; }
        
        public int TiresFrontId { get; set; }
        public string TiresFront { get; set; }

        public int TiresRearId { get; set; }
        public string TiresRear { get; set; }
        
        public string SheetName { get; set; }
        public string Notes { get; set; }
    }
}
