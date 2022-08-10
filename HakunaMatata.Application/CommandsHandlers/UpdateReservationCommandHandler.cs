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
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Reservation>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public UpdateReservationCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Reservation> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);
            var user = _uow.UserRepository.GetByIdNoTracking(userId);
            var reservations = user.Reservations;

            if (reservations == null)
                throw new UserDoesNotHaveReservation("User doesn't have this reservation assigned");

            foreach (var res in reservations)
            {
                if (res.ReservationId == request.ReservationId)
                {
                    var property = res.Property;

                    var checkinDate = DateTimeOffset.ParseExact(request.CheckinDate, "yyyy-MM-dd", null).UtcDateTime;
                    var checkoutDate = DateTimeOffset.ParseExact(request.CheckoutDate, "yyyy-MM-dd", null).UtcDateTime;
                    var totalPrice = (checkoutDate - checkinDate).Days * property.Price;

                    var updatedReservation = new Reservation
                    {
                        ReservationId = res.ReservationId,
                        Property = property,
                        CheckinDate = checkinDate,
                        CheckoutDate = checkoutDate,
                        GuestsNumber = request.GuestsNumber,
                        TotalPrice = totalPrice
                    };

                    if (request.GuestsNumber > property.MaxGuests)
                        throw new InvalidDatesException("Guests number cannot be larger than maximum guests");

                    if (updatedReservation.CheckinDate > updatedReservation.CheckoutDate)
                        throw new InvalidDatesException("Checkin date can't be later than checkoutdate");

                    if (!_uow.ReservationRepository
                        .CheckDates(
                            updatedReservation.CheckinDate, 
                            updatedReservation.CheckoutDate, 
                            updatedReservation.Property.PropertyId, 
                            updatedReservation.ReservationId))
                    {
                        throw new InvalidDatesException("Property is already reserved in this period");
                    }

                    _uow.ReservationRepository.Update(updatedReservation);
                    await _uow.SaveAsync();

                    return updatedReservation;
                }
            }

            throw new UserDoesNotHaveReservation("User doesn't have this reservation assigned");
        }
    }
}
