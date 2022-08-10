using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HakunaMatata.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(HakunaMatataContext ctx) : base(ctx) { }
        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet.Include(u => u.Property).ThenInclude(p => p.Images).Include(u => u.Reservations).ThenInclude(r => r.Property).ToListAsync();
        }

        public override IEnumerable<User> GetAll()
        {
            return _dbSet.Include(u => u.Property).ThenInclude(p => p.Images).Include(u => u.Reservations).ThenInclude(r => r.Property).ToList();
        }

        public async override Task<User> GetByIdAsync(int id)
        {
            var user = await _dbSet.Include(u => u.Property).ThenInclude(p => p.Images).Include(u => u.Reservations).ThenInclude(r => r.Property).SingleOrDefaultAsync(u => u.UserId == id);

            return user;
        }

        public override User GetById(int id)
        {
            var user = _dbSet.Include(u => u.Property).ThenInclude(p => p.Images).Include(u => u.Reservations).ThenInclude(r => r.Property).SingleOrDefault(u => u.UserId == id);

            return user;
        }

        public bool CheckEmail(string email)
        {
            if (_dbSet.Any(u => u.Email == email))
                return false;

            return true;
        }

        public bool CheckPassword(string password)
        {
            if (password.Length < 8)
                return false;

            if (password.All(c => !char.IsDigit(c)))
                return false;

            return true;
        }

        public User GetByIdNoTracking(int id)
        {
            var user = _dbSet.AsNoTracking().Include(u => u.Property).ThenInclude(p => p.Images).Include(u => u.Reservations).ThenInclude(r => r.Property).SingleOrDefault(u => u.UserId == id);

            return user;
        }

        public User GetByEmail(string email)
        {
            var user = _dbSet.Include(u => u.Property).ThenInclude(p => p.Images).Include(u => u.Reservations).ThenInclude(r => r.Property).SingleOrDefault(u => u.Email.Equals(email));

            return user;
        }

        async public Task<bool> PropertyIsAssignedAsync(int propertyId)
        {
            if (await _dbSet.Include(u => u.Property).ThenInclude(p => p.Images).Include(u => u.Reservations).ThenInclude(r => r.Property).AnyAsync(u => u.Property.PropertyId == propertyId))
                return true;

            return false;
        }

        public bool VerifyPassword(string email, string password)
        {
            var user = _dbSet.Include(u => u.Property).ThenInclude(p => p.Images).Include(u => u.Reservations).ThenInclude(r => r.Property).SingleOrDefault(u => u.Email.Equals(email));

            if (user != null && user.Password.Equals(password))
                return true;

            return false;
        }

        public async Task<User> GetUserByReservationAsync(int id)
        {
            var users = await _dbSet.ToListAsync();

            foreach (var user in users)
            {
                foreach (var res in user.Reservations)
                {
                    if (res.ReservationId == id)
                        return user;
                }
            }

            return null;
        }
    }
}
