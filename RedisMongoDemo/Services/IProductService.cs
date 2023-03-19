using RedisMongoDemo.Dto;
using RedisMongoDemo.Models;

namespace RedisMongoDemo.Services
{
    public interface IProductService
    {
        Task CreateProduct(CreateProductDto product);
        Task DeleteProduct(Guid productId);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid productId);
        Task UpdateProduct(Product product);
    }
}