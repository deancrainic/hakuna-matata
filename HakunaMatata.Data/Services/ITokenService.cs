using HakunaMatata.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Data.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
        int DecodeToken(string token);
    }
}
