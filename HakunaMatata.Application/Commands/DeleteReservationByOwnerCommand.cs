using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Commands
{
    public class DeleteReservationByOwnerCommand : IRequest
    {
        public string Token { get; set; }
        public int ReservationId { get; set; }
    }
}
