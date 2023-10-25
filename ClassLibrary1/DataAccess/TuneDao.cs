using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class TuneDao
    {
        public List<Tune> GetTunes()
        {
            var tunes = new List<Tune>();

            using (var context = new GranTurismoDb())
            {
                tunes = context.Tunes.ToList();
            }

            return tunes;
        }
    }
}
