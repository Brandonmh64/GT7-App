using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class TireTypeDao
    {
        public List<TireType> GetTireTypes()
        {
            var tireTypes = new List<TireType>();

            using (var context = new GranTurismoDb())
            {
                tireTypes = context.TireTypes.ToList();
            }

            return tireTypes;
        }
    }
}
