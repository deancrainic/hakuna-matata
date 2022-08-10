using HakunaMatata.Application.Commands;
using HakunaMatata.Application.Exceptions;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.CommandsHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private IUnitOfWork _uow;

        public CreateUserCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!_uow.UserRepository.CheckEmail(request.Email))
                throw new InvalidEmailException("Email already exists");

            if (!_uow.UserRepository.CheckPassword(request.Password))
                throw new InvalidPasswordException("Password requirements aren't met");

            var user = new User
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await _uow.UserRepository.AddAsync(user);
            await _uow.SaveAsync();

            return user;
        }
    }
}
