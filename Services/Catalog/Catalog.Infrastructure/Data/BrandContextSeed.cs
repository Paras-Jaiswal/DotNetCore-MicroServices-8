using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrand = brandCollection.Find(b => true).Any();
            //uncomment to run locally //Comment this to deploy
            string path = Path.Combine("Data", "SeedData", "brands.json");
            if (!checkBrand)
            {
                //uncomment to run locally //Comment this to deploy
                var brandsData = File.ReadAllText(path);

                //comment to run locally //uncomment this to deploy
                //var brandsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/brands.json"); 

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    foreach (var item in brands)
                    {
                        brandCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
