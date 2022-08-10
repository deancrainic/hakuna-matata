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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public DeleteUserCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);

            var toDelete = _uow.UserRepository.DeleteById(userId);

            await _uow.SaveAsync();

            return toDelete;
        }
    }
}
