﻿using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        public readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        

        #region Product
        public async Task<Product> GetProduct(string id)
        {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                var brandFilter = builder.Eq(p => p.Brands.Id, catalogSpecParams.BrandId);
                filter &= brandFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                var brandFilter = builder.Eq(p => p.Brands.Id, catalogSpecParams.TypeId);
                filter &= brandFilter;
            }

            var totalItems = await _context
                .Products
                .CountDocumentsAsync(filter);

            //var data = await _context
            //    .Products
            //    .Find(filter)
            //    .Skip((catalogSpecParams.PageIndex -1) * catalogSpecParams.PageSize)
            //    .Limit(catalogSpecParams.PageSize)
            //    .ToListAsync();

            var data = await DataFilter(catalogSpecParams, filter);

            return new Pagination<Product>(
                catalogSpecParams.PageIndex,
                catalogSpecParams.PageSize,
                (int)totalItems,
                data
            );
        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string brandName)
        {
            return await _context
                 .Products
                 .Find(p => p.Brands.Name.ToLower() == brandName.ToLower())
                 .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context
                .Products
                .Find(p => p.Name.ToLower() == name.ToLower())
                .ToListAsync();
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedProduct = await _context
                .Products
                .ReplaceOneAsync(p => p.Id == product.Id, product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteProduct = await _context
                .Products
                .DeleteOneAsync(id);
            return deleteProduct.IsAcknowledged && deleteProduct.DeletedCount > 0;
        }

        #endregion

        #region Brands

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context
                .Brands
                .Find(b => true)
                .ToListAsync();
        }
        #endregion

        #region Types
        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context
                .Types
                .Find(t => true)
                .ToListAsync();
        }

        #endregion


        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            var sortDefn = Builders<Product>.Sort.Ascending("Name");
            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                switch (catalogSpecParams.Sort)
                {
                    case "priceAsc":
                        sortDefn = Builders<Product>.Sort.Ascending(p => p.Price);
                        break;
                    case "priceDesc":
                        sortDefn = Builders<Product>.Sort.Descending(p => p.Price);
                        break;
                    default:
                        sortDefn = Builders<Product>.Sort.Ascending(p => p.Name);
                        break;
                }
            }

            return await _context
                .Products
                .Find(filter)
                .Sort(sortDefn)
                .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                .Limit(catalogSpecParams.PageSize)
                .ToListAsync();
        }
    }
}
