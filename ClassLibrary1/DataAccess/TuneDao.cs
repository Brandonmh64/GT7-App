using GranTurismoFramework;
using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class TuneDao
    {
        public List<TuneDto> GetTunes()
        {
            var tunes = new List<TuneDto>();

            using (var context = new GranTurismoDb())
            {
                var tuneList = context.Tunes.ToList();
                var mappedList = FwMapper.MapList<Tune, TuneDto>(tuneList);

                tunes.AddRange(mappedList);
            }

            return tunes;
        }
    }
}
