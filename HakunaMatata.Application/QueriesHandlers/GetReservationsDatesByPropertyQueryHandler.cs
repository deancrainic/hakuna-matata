using HakunaMatata.Application.Queries;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using HakunaMatata.Data.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.QueriesHandlers
{
    public class GetReservationsDatesByPropertyQueryHandler : IRequestHandler<GetReservationsDatesByPropertyQuery, IEnumerable<Reservation>>
    {
        private IUnitOfWork _uow;

        public GetReservationsDatesByPropertyQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Reservation>> Handle(GetReservationsDatesByPropertyQuery request, CancellationToken cancellationToken)
        {
            return await _uow.ReservationRepository.GetByPropertyId(request.PropertyId);
        }
    }
}
