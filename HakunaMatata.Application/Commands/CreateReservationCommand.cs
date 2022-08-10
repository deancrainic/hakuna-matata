using HakunaMatata.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Commands
{
    public class CreateReservationCommand : IRequest<Reservation>
    {
        public string Token { get; set; }
        public int PropertyId { get; set; }
        public string CheckinDate { get; set; }
        public string CheckoutDate { get; set; }
        public int GuestsNumber { get; set; }
        public double TotalPrice { get; set; }
    }
}
