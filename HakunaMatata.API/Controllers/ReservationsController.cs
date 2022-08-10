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
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReservationsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetAllReservations() 
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            var query = new GetAllReservationsQuery
            {
                Token = token
            };

            var result = await _mediator.Send(query);

            var mappedResult = _mapper.Map<IEnumerable<ReservationGetDto>>(result);
            return Ok(mappedResult);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("property/{propertyId}")]
        public async Task<IActionResult> GetReservationsDatesByProperty(int propertyId)
        {
            if (propertyId <= 0)
                return BadRequest("Invalid ID");

            var query = new GetReservationsDatesByPropertyQuery
            {
                PropertyId = propertyId
            };

            var result = await _mediator.Send(query);
            var mappedResult = _mapper.Map<IEnumerable<ReservationsDatesDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("property/{propertyId}/reservations")]
        public async Task<IActionResult> GetReservationsByProperty(int propertyId)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (propertyId <= 0)
                return BadRequest("Invalid ID");

            var query = new GetReservationsByPropertyIdQuery
            {
                Token = token,
                PropertyId = propertyId
            };

            try
            {
                var result = await _mediator.Send(query);

                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("current/{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (id <= 0)
                return BadRequest("Invalid ID");

            var query = new GetReservationByIdQuery
            {
                Token = token,
                ReservationId = id
            };

            try
            {
                var result = await _mediator.Send(query);

                var mappedResult = _mapper.Map<ReservationGetDto>(result);
                return Ok(mappedResult);
            } catch (UserDoesNotHaveReservation ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("current")]
        public async Task<IActionResult> CreateReservation(ReservationCreateDto newReservation)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (newReservation == null)
                return BadRequest("New reservation can't be null");

            var command = new CreateReservationCommand
            {
                Token = token,
                PropertyId = newReservation.PropertyId,
                CheckinDate = newReservation.CheckinDate,
                CheckoutDate = newReservation.CheckoutDate,
                GuestsNumber = newReservation.GuestsNumber
            };

            try
            {
                var result = await _mediator.Send(command);

                var mappedResult = _mapper.Map<ReservationGetDto>(result);
                return Ok(mappedResult);
            }
            catch (IdNotExistentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidDatesException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("current/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (id <= 0)
                return BadRequest("Invalid ID");

            var command = new DeleteReservationCommand 
            { 
                Token = token,
                ReservationId = id 
            };

            try
            {
                var result = await _mediator.Send(command);

                if (result == null)
                    return NotFound();

                return Ok();
            }
            catch (UserDoesNotHaveReservation ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        [Route("property/{id}")]
        public async Task<IActionResult> DeleteByOwner(int id)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (id <= 0)
                return BadRequest("Invalid ID");

            var command = new DeleteReservationByOwnerCommand
            {
                Token = token,
                ReservationId = id
            };

            try
            {
                var result = await _mediator.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("current/{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationUpdateDto reservation)
        {
            var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];

            if (token == null)
                return BadRequest("token can't be null");

            if (id <= 0)
                return BadRequest("Invalid ID");

            if (reservation == null)
                return BadRequest("Reservation can't be null");

            var command = new UpdateReservationCommand
            {
                Token = token,
                ReservationId = id,
                CheckinDate = reservation.CheckinDate,
                CheckoutDate = reservation.CheckoutDate,
                GuestsNumber = reservation.GuestsNumber
            };

            try
            {
                var result = await _mediator.Send(command);

                var mappedResult = _mapper.Map<ReservationGetDto>(result);
                return Ok(mappedResult);
            }
            catch (IdNotExistentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidDatesException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserDoesNotHaveReservation ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
