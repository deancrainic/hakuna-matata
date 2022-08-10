using AutoMapper;
using HakunaMatata.Application.Commands;
using HakunaMatata.Data.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HakunaMatata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ImagesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        //IList<IFormFile> formFiles
        [HttpPost]
        public async Task<IActionResult> UploadImages()
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];
            
            if (token == null)
                return BadRequest("token can't be null");

            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files.First();

            var command = new CreateImageCommand { Token =  token };

            foreach (var formFile in formCollection.Files)
            {
                var file = new FileDto
                {
                    Content = formFile.OpenReadStream(),
                    Name = formFile.Name,
                    ContentType = formFile.ContentType,
                };
                command.Files.Add(file);
            }

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (id < 0)
                return BadRequest("Invalid ID");

            var command = new DeleteImageCommand
            {
                Token = token,
                ImageId = id
            };

            try
            {
                var result = await _mediator.Send(command);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
