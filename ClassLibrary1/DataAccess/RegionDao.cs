using GranTurismoFramework;
using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class RegionDao
    {
        public List<RegionDto> GetRegions()
        {
            var mappedList = new List<RegionDto>();

            using (var context = new GranTurismoDb())
            {
                var list = context.Regions.ToList();
                mappedList = FwMapper.MapList<Region, RegionDto>(list);
            }

            return mappedList;
        }
    }
}
