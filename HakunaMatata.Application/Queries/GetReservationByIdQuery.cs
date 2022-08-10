using HakunaMatata.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Queries
{
    public class GetReservationByIdQuery : IRequest<Reservation>
    {
        public string Token { get; set; }
        public int ReservationId { get; set; }
    }
}
