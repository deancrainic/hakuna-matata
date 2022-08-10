using HakunaMatata.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Data.Strategies
{
    public interface ISortStrategy
    {
        IEnumerable<Property> SortProperties(IEnumerable<Property> properties);
    }
}
