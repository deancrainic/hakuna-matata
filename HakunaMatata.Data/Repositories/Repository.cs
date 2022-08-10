using HakunaMatata.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HakunaMatata.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected HakunaMatataContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(HakunaMatataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public T DeleteById(int id)
        {
            var toDelete = _dbSet.Find(id);

            if (toDelete != null)
                _dbSet.Remove(toDelete);

            return toDelete;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T toUpdate)
        {           
            _dbSet.Update(toUpdate);
        }
    }
}
