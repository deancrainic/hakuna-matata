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
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Property>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public UpdatePropertyCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Property> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);

            var user = _uow.UserRepository.GetByIdNoTracking(userId);
            var property = user.Property;

            if (property == null)
                throw new UserDoesNotHaveProperty("User doesn't have a property assigned");

            var updatedProperty = new Property
            {
                PropertyId = property.PropertyId,
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                MaxGuests = request.MaxGuests,
                Price = request.Price
            };

            _uow.PropertyRepository.Update(updatedProperty);
            await _uow.SaveAsync();

            return updatedProperty;
        }
    }
}
