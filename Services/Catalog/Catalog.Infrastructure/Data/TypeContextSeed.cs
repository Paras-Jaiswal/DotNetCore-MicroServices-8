using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typeCollection)
        {
            bool checkType = typeCollection.Find(b => true).Any();
            //uncomment to run locally //Comment this to deploy
            string path = Path.Combine("Data", "SeedData", "types.json"); 
            if (!checkType)
            {
                //uncomment to run locally //Comment this to deploy
                var typesData = File.ReadAllText(path); 

                //comment to run locally //uncomment this to deploy
                //var typesData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/types.json"); 

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types != null)
                {
                    foreach (var item in types)
                    {
                        typeCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
