using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.DataAccess
{
    public class RegionDao
    {
        public List<RegionDto> GetRegions()
        {
            var regionDao = new GranTurismoFramework.DataAccess.RegionDao();
            var regionList = regionDao.GetRegions();

            return regionList;
        }
    }
}
