using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Core.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        void Update(T toUpdate);
        T DeleteById(int id);
        Task AddAsync(T entity);
    }
}
