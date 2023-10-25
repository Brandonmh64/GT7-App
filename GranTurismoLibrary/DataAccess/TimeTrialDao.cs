using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GranTurismoLibrary.Models;
using GranTurismoFramework.DataAccess;

namespace GranTurismoLibrary.DataAccess
{
    public class TimeTrialDao
    {
        public List<TimeTrialDto> GetTimeTrials()
        {
            var timeTrials = new List<TimeTrialDto>();

            var cars = new CarDao().GetCars();
            var 
        }
    }
}
