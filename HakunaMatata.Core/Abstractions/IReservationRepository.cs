using HakunaMatata.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Core.Abstractions
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        IEnumerable<Reservation> GetAll();
        Task<Reservation> GetByIdAsync(int id);
        Reservation GetById(int id);
        Reservation GetByIdNoTracking(int id);
        bool CheckDates(DateTime checkin, DateTime checkout, int propertyId);
        bool CheckDates(DateTime checkin, DateTime checkout, int propertyId, int reservationId);
        Task<IEnumerable<Reservation>> GetByPropertyId(int id);
    }
}
