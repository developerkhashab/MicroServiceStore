using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DataSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DataSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DataSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
