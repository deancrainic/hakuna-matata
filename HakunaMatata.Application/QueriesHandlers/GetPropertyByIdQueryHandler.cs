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
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Property>
    {
        private IUnitOfWork _uow;

        public GetPropertyByIdQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Property> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.PropertyRepository.GetByIdAsync(request.PropertyId);
        }
    }
}
