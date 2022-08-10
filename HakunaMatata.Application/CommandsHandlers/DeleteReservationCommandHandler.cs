using HakunaMatata.Application.Commands;
using HakunaMatata.Application.Exceptions;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using HakunaMatata.Data.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.CommandsHandlers
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Reservation>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public DeleteReservationCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Reservation> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);

            var user = await _uow.UserRepository.GetByIdAsync(userId);
            var reservations = user.Reservations;

            if (reservations == null)
                throw new UserDoesNotHaveReservation("User doesn't have this reservation assigned");

            foreach (var res in reservations)
            {
                if (res.ReservationId == request.ReservationId)
                {
                    var toDelete = _uow.ReservationRepository.DeleteById(res.ReservationId);

                    await _uow.SaveAsync();

                    return toDelete;
                }
            }

            throw new UserDoesNotHaveReservation("User doesn't have this reservation assigned");
        }
    }
}
