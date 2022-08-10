using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException(string? message) : base(message)
        {
        }
    }
}
