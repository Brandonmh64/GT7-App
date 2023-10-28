using GranTurismoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.DataAccess
{
    public class BlacklistCarDao
    {
        public List<BlacklistCarInfo> GetBlacklistCars()
        {
            var potentialBlacklistCars = new List<BlacklistCarInfo>();
            var carDao = new GranTurismoFramework.DataAccess.CarDao();

            foreach (var car in carDao.GetAllCarInfo())
            {

            }

            return potentialBlacklistCars;
        }
    }
}
