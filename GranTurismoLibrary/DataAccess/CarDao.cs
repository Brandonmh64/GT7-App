﻿using GranTurismoFramework;
using GranTurismoFramework.DataAccess;
using GranTurismoFramework.DataTransfer;
using GranTurismoLibrary.Helpers;
using GranTurismoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.DataAccess
{
    public class CarDao
    {
        private GranTurismoFramework.DataAccess.CarDao _carDao { get => new GranTurismoFramework.DataAccess.CarDao(); }
        private GranTurismoFramework.DataAccess.OwnedCarDao _ownedCarDao { get => new GranTurismoFramework.DataAccess.OwnedCarDao(); }


        /* All Cars */

        public List<CarInfoDto> GetAllCarInfo()
        {
            return _carDao.GetAllCarInfo();
        }




        /* Owned Cars */

        public List<OwnedCarInfo> GetAllOwnedCars()
        {
            var ownedCars = _ownedCarDao.GetAllOwnedCars();

            return GtMapper.MapList<OwnedCarInfoDto, OwnedCarInfo>(ownedCars);
        }


        /* Car Properties */

        public List<GranTurismoFramework.DataTransfer.Simple.TireTypeDto> GetTireTypes()
        {
            var tireDao = new TireTypeDao();
            var typeList = tireDao.GetTireTypes();

            return GtMapper.MapList<TireType, GranTurismoFramework.DataTransfer.Simple.TireTypeDto>(typeList);
        }
    }
}
