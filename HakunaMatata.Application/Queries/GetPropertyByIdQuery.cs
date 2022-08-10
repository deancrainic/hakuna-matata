using HakunaMatata.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Queries
{
    public class GetPropertyByIdQuery : IRequest<Property>
    {
        public int PropertyId { get; set; }
    }
}
