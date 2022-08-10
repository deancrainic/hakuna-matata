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
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Property>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public DeletePropertyCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Property> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);

            var property = (await _uow.UserRepository.GetByIdAsync(userId)).Property;

            if (property == null)
                throw new UserDoesNotHaveProperty("User doesn't have a property assigned");

            var toDelete = _uow.PropertyRepository.DeleteById(property.PropertyId);
            
            await _uow.SaveAsync();

            return toDelete;
        }
    }
}
