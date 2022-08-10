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
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public DeleteImageCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);
            var user = await _uow.UserRepository.GetByIdAsync(userId);

            if (user.Property == null)
                throw new UserDoesNotHaveProperty("User doesn't have a property assigned");

            foreach (var image in user.Property.Images)
            {
                if (image.ImageId == request.ImageId)
                {
                    _uow.ImageRepository.DeleteById(request.ImageId);
                    await _uow.SaveAsync();
                    return new Unit();
                }
            }

            throw new PropertyDoesNotHaveThisImageException("This image isn't from you property");
        }
    }
}
