using HakunaMatata.Application.Queries;
using HakunaMatata.Core.Abstractions;
using HakunaMatata.Core.Enums;
using HakunaMatata.Core.Models;
using HakunaMatata.Data.Exceptions;
using HakunaMatata.Data.Strategies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Application.QueriesHandlers
{
    public class GetAllPropertiesSortedQueryHandler : IRequestHandler<GetAllPropertiesSortedQuery, IEnumerable<Property>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllPropertiesSortedQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Property>> Handle(GetAllPropertiesSortedQuery request, CancellationToken cancellationToken)
        {
            var properties = await _uow.PropertyRepository.GetAllAsync();
            IEnumerable<Property> sortedProperties;
            SortContext _sortContext;

            switch (request.StrategyType)
            {
                case SortStrategyType.PriceAsc:
                    _sortContext = new SortContext(new SortByPriceAscendingStrategy());
                    sortedProperties = _sortContext.SortProperties(properties);
                    break;
                case SortStrategyType.PriceDesc:
                    _sortContext = new SortContext(new SortByPriceDescendingStrategy());
                    sortedProperties = _sortContext.SortProperties(properties);
                    break;
                case SortStrategyType.Newest:
                    _sortContext = new SortContext(new SortByNewestStrategy());
                    sortedProperties = _sortContext.SortProperties(properties);
                    break;
                case SortStrategyType.Oldest:
                    _sortContext = new SortContext(new SortByOldestStrategy());
                    sortedProperties = _sortContext.SortProperties(properties);
                    break;
                default:
                    _sortContext = new SortContext(null);
                    try
                    {
                        sortedProperties = _sortContext.SortProperties(properties);
                    }
                    catch
                    {
                        throw;
                    }
                    break;
            }

            return sortedProperties;
        }
    }
}
