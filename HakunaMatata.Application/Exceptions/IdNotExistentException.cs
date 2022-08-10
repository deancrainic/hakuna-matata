using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Exceptions
{
    public class IdNotExistentException : Exception
    {
        public IdNotExistentException(string? message) : base(message)
        {
        }
    }
}
