using AutoMapper;
using HakunaMatata.API.Dto;
using HakunaMatata.Application.Commands;
using HakunaMatata.Application.Exceptions;
using HakunaMatata.Application.Queries;
using HakunaMatata.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HakunaMatata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];
            
            if (token == null)
                return BadRequest("token can't be null");

            var query = new GetCurrentUserQuery { Token = token };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound("User doesn't exist");
            
            var mappedResult = _mapper.Map<UserGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserCreateDto newUser)
        {
            if (newUser == null)
                return BadRequest("New user can't be null");

            var command = new CreateUserCommand
            {
                Email = newUser.Email,
                Password = newUser.Password,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
            };

            try
            {
                var result = await _mediator.Send(command);

                var mappedResult = _mapper.Map<UserGetDto>(result);
                return Ok(mappedResult);
            }
            catch (InvalidEmailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("current")]
        public async Task<IActionResult> DeleteUser()
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            var command = new DeleteUserCommand { Token = token };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok();
        }

        [HttpPut]
        [Route("current")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto user)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (user == null)
                return BadRequest("User can't be null");

            var command = new UpdateUserCommand
            {
                Token = token,
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            try
            {
                var result = await _mediator.Send(command);

            var mappedResult = _mapper.Map<UserGetDto>(result);
            return Ok(mappedResult);
            }
            catch (InvalidEmailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IdNotExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(UserCredentialsDto creds)
        {
            var command = new LoginCommand
            {
                Email = creds.Email,
                Password = creds.Password
            };

            try
            {
                var result = await _mediator.Send(command);

                return new JsonResult(result);
            } 
            catch (UserDoesNotExistException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
