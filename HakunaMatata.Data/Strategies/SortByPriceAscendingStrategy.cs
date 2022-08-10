using HakunaMatata.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Data.Strategies
{
    public class SortByPriceAscendingStrategy : ISortStrategy
    {
        public IEnumerable<Property> SortProperties(IEnumerable<Property> properties)
        {
            return properties.OrderBy(p => p.Price);
        }
    }
}
