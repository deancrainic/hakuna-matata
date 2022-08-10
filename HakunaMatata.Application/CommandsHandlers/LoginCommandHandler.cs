using HakunaMatata.Application.Commands;
using HakunaMatata.Application.Exceptions;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Data.Services;
using MediatR;

namespace HakunaMatata.Application.CommandsHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private IUnitOfWork _uow;
        private ITokenService _token;

        public LoginCommandHandler(IUnitOfWork uow, ITokenService token)
        {
            _uow = uow;
            _token = token;
        }

        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _uow.UserRepository.GetByEmail(request.Email);

            if (user == null)
            {
                throw new UserDoesNotExistException("Wrong email or password");
            }

            if (!_uow.UserRepository.VerifyPassword(request.Email, request.Password))
            {
                throw new UserDoesNotExistException("Wrong email or password");
            }

            string token = _token.CreateToken(user);
            return Task.FromResult(token);
        }
    }
}
