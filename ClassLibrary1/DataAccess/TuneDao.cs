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

        public List<TuneDto> GetTunes(int ownedCarId)
        {
            var tunes = new List<TuneDto>();

            using (var db = new GranTurismoDb())
            {
                var query = from t in db.Tunes
                            join tire in db.TireTypes on t.TiresFrontId equals tire.TireId
                            join tire2 in db.TireTypes on t.TiresRearId equals tire2.TireId
                            select new TuneDto()
                            {
                                TuneId = t.TuneId,
                                CarId = t.CarId,
                                PerformancePoints = t.PerformancePoints,
                                HorsePower = t.HorsePower,
                                Weight = t.Weight,
                                SheetName = t.SheetName,
                                Notes = t.Notes,

                                TiresFrontId = tire.TireId,
                                TiresFront = tire.Abreviation,
                                TiresRearId = tire2.TireId,
                                TiresRear = tire2.Abreviation,
                            };
                tunes = query.ToList();
            }

            return tunes;
        }


        public void SaveTune(TuneDto tuneDto)
        {
            var tune = FwMapper.Map<TuneDto, Tune>(tuneDto);

            using (var db = new GranTurismoDb())
            {
                db.Tunes.Add(tune);
                db.SaveChanges();
            }
        }
    }
}
