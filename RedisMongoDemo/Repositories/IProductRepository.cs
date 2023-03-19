using RedisMongoDemo.Dto;
using RedisMongoDemo.Models;

namespace RedisMongoDemo.Repositories
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);
        Task DeleteProduct(Guid productId);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid productId);
        Task UpdateProduct(Product product);
    }
}