using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class RegionDao
    {
        public List<Region> GetRegions()
        {
            var regions = new List<Region>();

            using (var context = new GranTurismoDb())
            {
                regions = context.Regions.ToList();
            }

            return regions;
        }
    }
}
