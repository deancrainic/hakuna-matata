using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Core.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IImageRepository ImageRepository { get; }
        Task SaveAsync();
    }
}
