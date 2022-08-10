using HakunaMatata.Application.Exceptions;
using HakunaMatata.Application.Queries;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using HakunaMatata.Data.DTOs;
using HakunaMatata.Data.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.QueriesHandlers
{
    public class GetReservationsByPropertyIdQueryHandler : IRequestHandler<GetReservationsByPropertyIdQuery, IEnumerable<ReservationGetForPropertyDto>>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public GetReservationsByPropertyIdQueryHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<ReservationGetForPropertyDto>> Handle(GetReservationsByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);

            var loggedUser = await _uow.UserRepository.GetByIdAsync(userId);

            if (loggedUser.Property == null)
                throw new UserDoesNotHaveProperty("User doesn't have this property");

            if (loggedUser.Property.PropertyId != request.PropertyId)
                throw new UserDoesNotHaveProperty("User doesn't have this property");

            var reservations = await _uow.ReservationRepository.GetByPropertyId(request.PropertyId);
            var reservationsForProperty = new List<ReservationGetForPropertyDto>();

            foreach (var reservation in reservations)
            {
                var user = await _uow.UserRepository.GetUserByReservationAsync(reservation.ReservationId);
                var res = new ReservationGetForPropertyDto
                {
                    Id = reservation.ReservationId,
                    Email = user.Email,
                    CheckinDate = reservation.CheckinDate,
                    CheckoutDate = reservation.CheckoutDate
                };

                reservationsForProperty.Add(res);

            }

            return reservationsForProperty;
        }
    }
}
