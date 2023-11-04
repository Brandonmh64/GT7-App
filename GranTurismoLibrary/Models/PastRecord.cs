using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Models
{
    public class PastRecord : TimeTrialInfo
    {
        public DateTime SessionDateTime { get; set; }

        public string SessionDate { get => SessionDateTime.ToString("d"); }
    }
}
