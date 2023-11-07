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
        public List<PastRecord> GetTrackTopRecords(int trackId, int topRecordCount)
        {
            var allTimeTrials = GetTrackTimeTrials(trackId).OrderBy(tt => tt.Time).ToList();
            var topRecords = new List<TimeTrialInfo>();

            foreach (var timeTrial in allTimeTrials)
            {
                if (topRecords.Count != topRecordCount)
                {
                    var carMatch = topRecords.FirstOrDefault(tt => tt.OwnedCarInfo.CarId == timeTrial.OwnedCarInfo.CarId);

                    // Add the record only if that car has not been used already
                    if (carMatch == null)
                    {
                        topRecords.Add(timeTrial);
                    }
                }
            }

            var sessionDao = new SessionDao();
            var topPastRecords = new List<PastRecord>();
            foreach (var top in topRecords)
            {
                var pastRecord = GtMapper.Map<TimeTrialInfo, PastRecord>(top);
                pastRecord.SessionDateTime = sessionDao.GetSessionDate(pastRecord.SessionId);

                topPastRecords.Add(pastRecord);
            }

            return topPastRecords;
        }

        


        // Save
        public void SaveTimeTrials(List<TimeTrialInfo> timeTrialInfos)
        {
            var ttDao = new GranTurismoFramework.DataAccess.TimeTrialDao();
            var mappedTimeTrials = GtMapper.MapList<TimeTrialInfo, TimeTrialInfoDto>(timeTrialInfos);

            ttDao.SaveTimeTrials(mappedTimeTrials);
        }




        // Analysis
        public int GetTrackTopChartPosition(int trackId, TimeSpan time)
        {
            var topTenRecords = GetTrackTopRecords(trackId, 10);

            if (topTenRecords.Count > 0)
            {
                for (int i = 0; i < topTenRecords.Count; i++)
                {
                    var comparisonRecord = topTenRecords[i];

                    if (comparisonRecord != null)
                    {
                        if (comparisonRecord.Time > time)
                        {
                            return i + 1;
                        }
                    }
                }
                return topTenRecords.Count + 1;
            }
            else
            {
                return 1;
            }
        }

        public TimeSpan GetTimeAwayFromTopRecord(int trackId, TimeSpan time)
        {
            var nextBestTrial = new TimeTrialInfo();

            foreach (var ttInfo in GetTrackTopRecords(trackId, 10).OrderBy(tt => tt.Time))
            {
                // Find the first Top Ten which is faster than current time
                if (time > ttInfo.Time)
                {
                    return time - ttInfo.Time;
                }
            }

            return new TimeSpan(1, 0, 0, 0);
        }

        public TimeSpan GetTimeAwayFromNextBestRecord(int trackId, TimeSpan time)
        {
            var nextBestTrial = new TimeTrialInfo();

            foreach (var ttInfo in GetTrackTopRecords(trackId, 10).OrderBy(tt => tt.Time).Reverse())
            {
                // Find the first Top Ten which is faster than current time
                if (time > ttInfo.Time)
                {
                    return time - ttInfo.Time;
                }
            }

            return new TimeSpan(1, 0, 0, 0);
        }
    }
}
