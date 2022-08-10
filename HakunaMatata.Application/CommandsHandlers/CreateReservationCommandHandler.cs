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
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Reservation>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public CreateReservationCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Reservation> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var checkinDate = DateTimeOffset.ParseExact(request.CheckinDate, "yyyy-MM-dd", null).UtcDateTime;
            var checkoutDate = DateTimeOffset.ParseExact(request.CheckoutDate, "yyyy-MM-dd", null).UtcDateTime;

            var property = _uow.PropertyRepository.GetById(request.PropertyId);

            if (property == null)
                throw new IdNotExistentException("Property ID doesn't exist");

            if (!_uow.ReservationRepository.CheckDates(checkinDate, checkoutDate, request.PropertyId))
                throw new InvalidDatesException("Property is already reserved in this period");

            if (checkinDate > checkoutDate)
                throw new InvalidDatesException("Checkin date can't be later than checkoutdate");

            var totalPrice = (checkoutDate - checkinDate).Days * property.Price;

            var reservation = new Reservation
            {
                Property = property,
                CheckinDate = checkinDate,
                CheckoutDate = checkoutDate,
                GuestsNumber = request.GuestsNumber,
                TotalPrice = totalPrice
            };

            await _uow.ReservationRepository.AddAsync(reservation);

            var userId = _tokenService.DecodeToken(request.Token);
            var user = await _uow.UserRepository.GetByIdAsync(userId);

            user.Reservations.Add(reservation);
            _uow.UserRepository.Update(user);

            await _uow.SaveAsync();

            return reservation;
        }
    }
}
