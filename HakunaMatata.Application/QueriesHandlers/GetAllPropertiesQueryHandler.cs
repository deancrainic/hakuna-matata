using HakunaMatata.Application.Queries;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.QueriesHandlers
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, IEnumerable<Property>>
    {
        private IUnitOfWork _uow;

        public GetAllPropertiesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Property>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            return await _uow.PropertyRepository.GetAllAsync();
        }
    }
}
