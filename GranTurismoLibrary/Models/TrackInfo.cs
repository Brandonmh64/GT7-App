using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Models
{
    public class TrackInfo
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
}
