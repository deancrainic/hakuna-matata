using HakunaMatata.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Core.Abstractions
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<IEnumerable<Property>> GetAllAsync();
        IEnumerable<Property> GetAll();
        Task<Property> GetByIdAsync(int id);
        Property GetById(int id);
        Property GetByIdNoTracking(int id);
    }
}
