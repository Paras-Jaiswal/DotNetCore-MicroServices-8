using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Net;
using System.Reflection.Metadata;
namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        //IMediator.
        //And this mediator job is to delegate that, you know, to your application layer where in CQRS pattern
        //is handled, like whether it's a query or a command based on that, it is going to take care of that.

        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductByProductId")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductByProductId(string id)
        {
            //So once we delegate this query so this will be handed the query pattern which
            //we have here.
            //And then that will be handled by that uh handler of that query.
            //And then it returns the result.

            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByProductName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypesResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<TypesResponse>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{brand}", Name = "GetProductByBrandName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByBrandName(string brand)
        {
            var query = new GetProductByBrandQuery(brand);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand createProduct)
        {
            var result = await _mediator.Send<ProductResponse>(createProduct);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<bool>> CreateProduct([FromBody] UpdateProductCommand updateProduct)
        {
            var result = await _mediator.Send(updateProduct);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}",Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
