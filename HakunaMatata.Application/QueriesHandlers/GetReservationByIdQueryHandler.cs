using HakunaMatata.Application.Exceptions;
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
    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, Reservation>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public GetReservationByIdQueryHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Reservation> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);

            var user = _uow.UserRepository.GetByIdNoTracking(userId);
            var reservations = user.Reservations;

            if (reservations == null)
                throw new UserDoesNotHaveReservation("User doesn't have this reservation assigned");

            foreach (var res in reservations)
            {
                if (res.ReservationId == request.ReservationId)
                    return await _uow.ReservationRepository.GetByIdAsync(request.ReservationId);
            }

            throw new UserDoesNotHaveReservation("User doesn't have this reservation assigned");
        }
    }
}
