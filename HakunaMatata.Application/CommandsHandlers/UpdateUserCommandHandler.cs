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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;

        public UpdateUserCommandHandler(IUnitOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);
            var user = _uow.UserRepository.GetByIdNoTracking(userId);

            if (user == null)
                throw new IdNotExistentException("Id not found");

            if (!request.Email.Equals(user.Email))
                if (!_uow.UserRepository.CheckEmail(request.Email))
                    throw new InvalidEmailException("Email already exists");

            User updatedUser;

            if (request.Password.Equals(""))
            {
                updatedUser = new User
                {
                    UserId = userId,
                    Email = request.Email,
                    Password = user.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };
            } else
            {
                if (!_uow.UserRepository.CheckPassword(request.Password))
                    throw new InvalidPasswordException("Password requirements aren't met");

                updatedUser = new User
                {
                    UserId = userId,
                    Email = request.Email,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };
            }

            

            _uow.UserRepository.Update(updatedUser);
            await _uow.SaveAsync();

            return updatedUser;
        }
    }
}
