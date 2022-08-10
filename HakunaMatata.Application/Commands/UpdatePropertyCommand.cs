using HakunaMatata.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Commands
{
    public class UpdatePropertyCommand : IRequest<Property>
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int MaxGuests { get; set; }
        public double Price { get; set; }
    }
}
