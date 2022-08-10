using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Data.Exceptions
{
    public class StrategyDoesNotExistException : Exception
    {
        public StrategyDoesNotExistException(string? message) : base(message)
        {
        }
    }
}
