using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Exceptions
{
    public class UserDoesNotHaveProperty : Exception
    {
        public UserDoesNotHaveProperty(string? message) : base(message)
        {
        }
    }
}
