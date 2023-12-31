﻿using GranTurismoLibrary.Helpers;
using GranTurismoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.DataAccess
{
    public class TrackDao
    {
        public List<TrackInfo> GetAllTrackInfo()
        {
            var trackDao = new GranTurismoFramework.DataAccess.TrackDao();

            var mappedTracks = GtMapper.MapList<GranTurismoFramework.DataTransfer.TrackInfoDto, TrackInfo>(trackDao.GetAllTrackInfo());
            return mappedTracks;
        }

        public TrackInfo GetTrack(int trackId)
        {
            return GetAllTrackInfo().FirstOrDefault(track => track.TrackId == trackId);
        }
    }
}
