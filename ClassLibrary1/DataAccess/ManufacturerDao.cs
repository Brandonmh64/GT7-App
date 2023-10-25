using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class ManufacturerDao
    {
        public List<Manufacturer> GetManufacturers()
        {
            var manufacturers = new List<Manufacturer>();

            using (var context = new GranTurismoDb())
            {
                manufacturers = context.Manufacturers.ToList();
            }

            return manufacturers;
        }
    }
}
