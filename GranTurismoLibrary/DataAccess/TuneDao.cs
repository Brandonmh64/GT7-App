using GranTurismoFramework.DataTransfer.Simple;
using GranTurismoLibrary.Helpers;
using GranTurismoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.DataAccess
{
    public class TuneDao
    {
        private GranTurismoFramework.DataAccess.TuneDao _tuneDao { get => new GranTurismoFramework.DataAccess.TuneDao(); }


        public List<TuneInfo> GetTunes(int ownedCarId)
        {
            var tunes = _tuneDao.GetTunes(ownedCarId);
            var mappedTunes = GtMapper.MapList<TuneDto, TuneInfo>(tunes);
            return mappedTunes;
        }

        public void SaveTune(TuneDto tune)
        {
            _tuneDao.SaveTune(tune);
        }
    }
}
