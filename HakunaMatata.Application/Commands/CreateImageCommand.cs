using HakunaMatata.Data.DTOs;
using HakunaMatata.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.Commands
{
    public class CreateImageCommand : IRequest<UrlsDto>
    {
        public string Token { get; set; }
        public ICollection<FileDto> Files { get; set; } = new List<FileDto>();
    }
}
