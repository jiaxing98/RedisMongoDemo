using Microsoft.Extensions.Caching.Distributed;
using RedisMongoDemo.Dto;
using RedisMongoDemo.Extensions;
using RedisMongoDemo.Models;
using RedisMongoDemo.Repositories;

namespace RedisMongoDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _cache;

        public ProductService(IProductRepository productRepository, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await GetFromCache(
                "all_products",
                async () => await _productRepository.GetAllProducts());
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            return await GetFromCache(
                $"product_{productId}",
                async () => await _productRepository.GetProductById(productId));
        }

        public async Task CreateProduct(CreateProductDto dto)
        {
            var product = new Product 
            { 
                ProductId = Guid.NewGuid(), 
                ProductName = dto.ProductName, 
                Price = dto.Price 
            };
            await _productRepository.CreateProduct(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _productRepository.UpdateProduct(product);
        }

        public async Task DeleteProduct(Guid productId)
        {
            await _productRepository.DeleteProduct(productId);
        }

        private async Task<T> GetFromCache<T>(string recordId, Func<Task<T>> func)
        {
            var data = await _cache.GetRecordAsync<T>(recordId);
            if (data == null)
            {
                // if cache is null, hit db and save to redis
                var dataFromDB = await func();
                if (dataFromDB == null) return default;

                await _cache.SetRecordAsync(recordId, dataFromDB);
                return dataFromDB;
            }

            return data;
        }
    }
}
