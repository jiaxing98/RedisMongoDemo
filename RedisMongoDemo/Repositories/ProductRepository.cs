using MongoDB.Driver;
using RedisMongoDemo.Data;
using RedisMongoDemo.Models;

namespace RedisMongoDemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<Product> _collection;
        private const string _collectionName = "Products";

        private readonly FilterDefinitionBuilder<Product> _filter = Builders<Product>.Filter;
        private readonly UpdateDefinitionBuilder<Product> _update = Builders<Product>.Update;

        public ProductRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<Product>(_collectionName);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var filter = _filter.Eq(x => x.ProductId, productId);
            var result = await _collection.Find(filter).SingleOrDefaultAsync();
            return result;
        }

        public async Task CreateProduct(Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            // use ReplaceOneAsync to replace whole document
            var filter = _filter.Eq(x => x.ProductId, product.ProductId);
            var update = _update.Set(x => x.ProductName, product.ProductName)
                                .Set(x => x.Price, product.Price);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteProduct(Guid productId)
        {
            var filter = _filter.Eq(x => x.ProductId, productId);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
