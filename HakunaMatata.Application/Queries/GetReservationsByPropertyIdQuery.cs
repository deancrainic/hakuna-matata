using HakunaMatata.Core.Models;
using HakunaMatata.Data.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Queries
{
    public class GetReservationsByPropertyIdQuery : IRequest<IEnumerable<ReservationGetForPropertyDto>>
    {
        public string Token { get; set; }
        public int PropertyId { get; set; }
    }
}