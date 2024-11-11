
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepositories;

        public GetBasketByUserNameQueryHandler(IBasketRepository basketRepositories)
        {
            _basketRepositories = basketRepositories;
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepositories.GetBasket(request.UserName);
            var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
            return shoppingCartResponse;
        }
    }
}
