using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Exceptions
{
    public class PropertyIsAlreadyAssignedException : Exception
    {
        public PropertyIsAlreadyAssignedException(string? message) : base(message)
        {
        }
    }
}
