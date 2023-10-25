using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Models
{
    public class TrackDto
    {
        public string TrackName { get => _track.Track.Name; }

        public string CourseName { get => _track.Course.Name; }

        public string Region { get => _track.Region.Name; }



        private GranTurismoFramework.DataTransfer.TrackDto _track;


        public TrackDto(GranTurismoFramework.DataTransfer.TrackDto trackDto)
        {
            _track = trackDto;
        }
    }
}
