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
        // Get 
        public List<TimeTrialInfo> GetTimeTrials()
        {
            var ttDao = new GranTurismoFramework.DataAccess.TimeTrialDao();
            var allTimeTrials = ttDao.GetTimeTrials();
            var mappedList = GtMapper.MapList<TimeTrialInfoDto, TimeTrialInfo>(allTimeTrials);

            return mappedList;
        }


        public List<TimeTrialInfo> GetTrackTimeTrials(int trackId)
        {
            var allTimeTrials = GetTimeTrials();

            var filtredList = allTimeTrials.Where(tt => tt.Track.TrackId == trackId).ToList();
            return filtredList;
        }
        
        /// <summary>
        /// Filter the Track's Time Trial results down to only include the top 10 
        /// records set by unique cars
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public List<TimeTrialInfo> GetTrackTopTen(int trackId)
        {
            var allTimeTrials = GetTrackTimeTrials(trackId).OrderBy(tt => tt.Time).ToList();
            var topTen = new List<TimeTrialInfo>();

            foreach (var timeTrial in allTimeTrials)
            {
                if (topTen.Count != 10)
                {
                    var carMatch = topTen.FirstOrDefault(tt => tt.OwnedCarInfo.CarId == timeTrial.OwnedCarInfo.CarId);

                    // Add the record to top 10 only if that car has not been used already
                    if (carMatch == null)
                    {
                        topTen.Add(timeTrial);
                    }
                }
            }

            return topTen;
        }



        // Save
        public void SaveTimeTrials(List<TimeTrialInfo> timeTrialInfos)
        {
            var ttDao = new GranTurismoFramework.DataAccess.TimeTrialDao();
            var mappedTimeTrials = GtMapper.MapList<TimeTrialInfo, TimeTrialInfoDto>(timeTrialInfos);

            ttDao.SaveTimeTrials(mappedTimeTrials);
        }
    }
}
