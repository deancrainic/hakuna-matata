using HakunaMatata.Application.Commands;
using HakunaMatata.Data.DTOs;
using HakunaMatata.Application.Exceptions;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using HakunaMatata.Data.Services;
using MediatR;

namespace HakunaMatata.Application.CommandsHandlers
{
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, UrlsDto>
    {
        private IUnitOfWork _uow;
        private ITokenService _tokenService;
        private IFileStorageService _fileStorageService;

        public CreateImageCommandHandler(IUnitOfWork uow, ITokenService tokenService, IFileStorageService fileStorageService)
        {
            _uow = uow;
            _tokenService = tokenService;
            _fileStorageService = fileStorageService;
        }

        public async Task<UrlsDto> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.DecodeToken(request.Token);
            var user = await _uow.UserRepository.GetByIdAsync(userId);

            if (user.Property == null)
                throw new UserDoesNotHaveProperty("User doesn't have a property assigned");

            var urls = await _fileStorageService.UploadAsync(request.Files);

            foreach (var url in urls.Urls)
            {
                var img = new Image
                {
                    Path = url,
                };
                await _uow.ImageRepository.AddAsync(img);

                var property = _uow.PropertyRepository.GetById(user.Property.PropertyId);
                property.Images.Add(img);
                _uow.PropertyRepository.Update(property);
            }

            await _uow.SaveAsync();
            return urls;
        }
    }
}
