using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using Microsoft.EntityFrameworkCore;
using HakunaMatata.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Data.Repositories
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(HakunaMatataContext ctx) : base(ctx) { }

        public async override Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _dbSet.Include(p => p.Images).ToListAsync();
        }

        public override IEnumerable<Property> GetAll()
        {
            return _dbSet.Include(p => p.Images).ToList();
        }

        public async override Task<Property> GetByIdAsync(int id)
        {
            var property = await _dbSet.Include(p => p.Images).SingleOrDefaultAsync(p => p.PropertyId == id);

            return property;
        }

        public override Property GetById(int id)
        {
            var property = _dbSet.Include(p => p.Images).SingleOrDefault(p => p.PropertyId == id);

            return property;
        }

        public Property GetByIdNoTracking(int id)
        {
            var property = _dbSet.AsNoTracking().Include(p => p.Images).SingleOrDefault(p => p.PropertyId == id);

            return property;
        }
    }
}
