using AutoMapper;
using HakunaMatata.API.Dto;
using HakunaMatata.Application.Commands;
using HakunaMatata.Application.Exceptions;
using HakunaMatata.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HakunaMatata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PropertiesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProperties()
        {
            var query = new GetAllPropertiesQuery();

            var result = await _mediator.Send(query);
            var mappedResult = _mapper.Map<List<PropertyGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("sorted/{sortType}")]
        public async Task<IActionResult> GetAllPropertiesSorted(int sortType)
        {
            var strategy = (Core.Enums.SortStrategyType)sortType;

            var query = new GetAllPropertiesSortedQuery { StrategyType = strategy };
            try
            {
                var result = await _mediator.Send(query);
                var mappedResult = _mapper.Map<List<PropertyGetDto>>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var query = new GetPropertyByIdQuery
            {
                PropertyId = id
            };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<PropertyGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route("current")]
        public async Task<IActionResult> CreateProperty(PropertyCreateDto newProperty)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (newProperty == null)
                return BadRequest("New property can't be null");

            var command = new CreatePropertyCommand
            {
                Token = token,
                Name = newProperty.Name,
                Description = newProperty.Description,
                Address = newProperty.Address,
                MaxGuests = newProperty.MaxGuests,
                Price = newProperty.Price
            };

            var result = await _mediator.Send(command);

            var mappedResult = _mapper.Map<PropertyGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route("current")]
        public async Task<IActionResult> DeleteProperty()
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            var command = new DeletePropertyCommand
            {
                Token = token
            };

            try
            {
                var result = await _mediator.Send(command);

                if (result == null)
                    return NotFound();

                return Ok();
            } catch (UserDoesNotHaveProperty ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("current")]
        public async Task<IActionResult> UpdateProperty(PropertyCreateDto property)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (property == null)
                return BadRequest("Property can't be null");

            var command = new UpdatePropertyCommand
            {
                Token = token,
                Name = property.Name,
                Description = property.Description,
                Address = property.Address,
                MaxGuests = property.MaxGuests,
                Price = property.Price
            };

            try
            {
                var result = await _mediator.Send(command);

                var mappedResult = _mapper.Map<PropertyGetDto>(result);
                return Ok(mappedResult);
            }
            catch (UserDoesNotHaveProperty ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpPost]
        //[Route("{propertyId}/Images/{imageId}")]
        //public async Task<IActionResult> AddImageToProperty(int propertyId, int imageId)
        //{
        //    if (propertyId <= 0 || imageId <= 0)
        //        return BadRequest("Invalid ID");

        //    var command = new AddImageToPropertyCommand
        //    {
        //        PropertyId = propertyId,
        //        ImageId = imageId
        //    };

        //    try
        //    {
        //        var result = await _mediator.Send(command);

        //        var mappedResult = _mapper.Map<PropertyGetDto>(result);
        //        return Ok(mappedResult);
        //    }
        //    catch (IdNotExistentException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
    }
}
