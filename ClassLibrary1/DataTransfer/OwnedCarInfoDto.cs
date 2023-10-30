﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataTransfer
{
    public class OwnedCarInfoDto : CarInfoDto
    {
        public int PrimaryDriverId { get; set; }    
        public string PrimaryDriverName { get; set; }

        public string ImageName {  get; set; }
    }
}
