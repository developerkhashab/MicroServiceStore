using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Private Readonly

        private readonly ICatalogContext _context;

        #endregion

        #region Constructor
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        public async Task CreateProduct(Product product) => await _context.Products.InsertOneAsync(product);

        public async Task<Product> GetProduct(string id) => await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProducts() => await _context.Products.Find(p => true).ToListAsync();

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            var deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, category);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

            return await _context.Products.Find(filter).ToListAsync();

        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        #endregion

    }
}
