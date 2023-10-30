using GranTurismoFramework;
using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class ManufacturerDao
    {
        public List<ManufacturerDto> GetManufacturers()
        {
            var manufacturers = new List<ManufacturerDto>();

            using (var context = new GranTurismoDb())
            {
                var manufacturerList = context.Manufacturers.ToList();
                var mappedList = FwMapper.MapList<Manufacturer, ManufacturerDto>(manufacturerList);

                manufacturers.AddRange(mappedList);
            }

            return manufacturers;
        }
    }
}
