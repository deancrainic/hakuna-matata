using HakunaMatata.Application.Commands;
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
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Property>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenSerivce;

        public CreatePropertyCommandHandler(IUnitOfWork uow, ITokenService tokenSerivce)
        {
            _uow = uow;
            _tokenSerivce = tokenSerivce;
        }

        public async Task<Property> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenSerivce.DecodeToken(request.Token);

            var user = await _uow.UserRepository.GetByIdAsync(userId);

            var property = new Property
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                MaxGuests = request.MaxGuests,
                Price = request.Price
            };

            await _uow.PropertyRepository.AddAsync(property);

            user.Property = property;
            _uow.UserRepository.Update(user);

            await _uow.SaveAsync();

            return property;
        }
    }
}
