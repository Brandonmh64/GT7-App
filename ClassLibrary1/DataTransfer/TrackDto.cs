using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataTransfer
{
    public class TrackDto
    {
        public Course Course { get; set; }  
        public Track Track { get; set; }
        public Region Region { get; set; }
    }
}
