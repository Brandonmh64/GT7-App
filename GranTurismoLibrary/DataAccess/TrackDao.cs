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
        public List<TrackDto> GetTracks()
        {
            var tracks = new List<TrackDto>();

            foreach (var track in new GranTurismoFramework.DataAccess.TrackDao().GetTracks())
            {
                var trackDto = new TrackDto(track);
                
                tracks.Add(trackDto);
            }

            return tracks;
        }
    }
}
