using HakunaMatata.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Core.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        IEnumerable<User> GetAll();
        Task<User> GetByIdAsync(int id);
        User GetById(int id);
        User GetByIdNoTracking(int id);
        bool CheckEmail(string email);
        bool CheckPassword(string password);
        User GetByEmail(string email);
        bool VerifyPassword(string email, string password);
        Task<bool> PropertyIsAssignedAsync(int propertyId);
        Task<User> GetUserByReservationAsync(int id);
    }
}
