﻿using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool checkProducts = productCollection.Find(b => true).Any();
            //uncomment to run locally //Comment this to deploy
            string path = Path.Combine("Data", "SeedData", "products.json");
            if (!checkProducts)
            {
                //uncomment to run locally //Comment this to deploy
                var productsData = File.ReadAllText(path);

                //comment to run locally //uncomment this to deploy
                //var productsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        productCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
