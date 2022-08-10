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
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(HakunaMatataContext ctx) : base(ctx) { }
        public async override Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _dbSet.Include(r => r.Property).ToListAsync();
        }

        public override IEnumerable<Reservation> GetAll()
        {
            return _dbSet.Include(r => r.Property).ToList();
        }

        public async override Task<Reservation> GetByIdAsync(int id)
        {
            var reservation = await _dbSet.Include(r => r.Property).SingleOrDefaultAsync(u => u.ReservationId == id);

            return reservation;
        }

        public override Reservation GetById(int id)
        {
            var reservation = _dbSet.Include(r => r.Property).SingleOrDefault(u => u.ReservationId == id);

            return reservation;
        }

        public bool CheckDates(DateTime checkin, DateTime checkout, int propertyId)
        {
            if (_dbSet
                .Where(r => r.Property.PropertyId == propertyId)
                .Any(r =>
                (checkin < r.CheckoutDate && checkin >= r.CheckinDate) ||
                (checkout > r.CheckinDate && checkout <= r.CheckoutDate) ||
                (checkin <= r.CheckinDate && checkout >= r.CheckoutDate)))
                return false;

            if (checkin == checkout)
                return false;

            return true;
        }

        public bool CheckDates(DateTime checkin, DateTime checkout, int propertyId, int reservationId)
        {
            if (_dbSet
                .Where(r => r.Property.PropertyId == propertyId && r.ReservationId != reservationId)
                .Any(r =>
                (checkin < r.CheckoutDate && checkin >= r.CheckinDate) ||
                (checkout > r.CheckinDate && checkout <= r.CheckoutDate) ||
                (checkin <= r.CheckinDate && checkout >= r.CheckoutDate)))
                return false;

            if (checkin == checkout)
                return false;

            return true;
        }

        public Reservation GetByIdNoTracking(int id)
        {
            var reservation = _dbSet.AsNoTracking().Include(r => r.Property).SingleOrDefault(u => u.ReservationId == id);

            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetByPropertyId(int id)
        {
            var reservations = _dbSet.Include(r => r.Property).Where(r => r.Property.PropertyId == id).ToListAsync();

            return await reservations;
        }
    }
}
