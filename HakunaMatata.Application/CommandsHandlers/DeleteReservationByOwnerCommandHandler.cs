using HakunaMatata.Application.Commands;
using HakunaMatata.Application.Exceptions;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Data.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.CommandsHandlers
{
    public class DeleteReservationByOwnerCommandHandler : IRequestHandler<DeleteReservationByOwnerCommand>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public DeleteReservationByOwnerCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Unit> Handle(DeleteReservationByOwnerCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);

            var user = await _uow.UserRepository.GetByIdAsync(userId);

            if (user.Property == null)
            {
                throw new UserDoesNotHaveProperty("User doesn't have a property assigned");
            }

            var reservations = await _uow.ReservationRepository.GetAllAsync();

            var reservation = reservations.FirstOrDefault(r => r.ReservationId == request.ReservationId);

            if (reservation.Property.PropertyId == user.Property.PropertyId)
            {
                var toDelete = _uow.ReservationRepository.DeleteById(reservation.ReservationId);

                await _uow.SaveAsync();

                return new Unit();
            }

            throw new UserDoesNotHaveReservation("User doesn't have this reservation");
        }
    }
}
