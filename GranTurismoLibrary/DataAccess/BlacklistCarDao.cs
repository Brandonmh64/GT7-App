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
        public List<BlacklistCar> GetBlacklistCars()
        {
            var potentialBlacklistCars = new List<BlacklistCar>();
            var carDao = new GranTurismoFramework.DataAccess.CarDao();

            foreach (var car in carDao.GetCars())
            {

            }
        }
    }
}
