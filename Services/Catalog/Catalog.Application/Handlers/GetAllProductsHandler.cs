using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository _repository;
        public GetAllProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _repository.GetAllProducts(request.CatalogSpecParams);
            var productReponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
            return productReponseList;
        }
    }
}
