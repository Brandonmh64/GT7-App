using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GranTurismoLibrary.Models;
using GranTurismoFramework.DataAccess;
using GranTurismoLibrary.Helpers;
using GranTurismoFramework.DataTransfer;

namespace GranTurismoLibrary.DataAccess
{
    public class TimeTrialDao
    {
        public void SaveTimeTrials(List<TimeTrialInfo> timeTrialInfos)
        {
            var ttDao = new GranTurismoFramework.DataAccess.TimeTrialDao();
            var mappedTimeTrials = GtMapper.MapList<TimeTrialInfo, TimeTrialInfoDto>(timeTrialInfos);

            ttDao.SaveTimeTrials(mappedTimeTrials);
        }
    }
}
