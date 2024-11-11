using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands
{
    public class CreateProductCommand: IRequest<ProductResponse>
    {
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        //public string Id { get; set; }

        //[BsonElement("Name")]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public ProductBrand Brands { get; set; }
        public ProductType Types { get; set; }

        //[BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}
