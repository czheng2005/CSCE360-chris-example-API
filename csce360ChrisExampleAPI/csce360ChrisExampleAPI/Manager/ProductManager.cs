using System.Text.Json;
using csce360ChrisExampleAPI.Manager.Interface;
using csce360ChrisExampleAPI.Models;
using csce360ChrisExampleAPI.Repository.Interface;

namespace csce360ChrisExampleAPI.Manager
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Result>> GetAllResultsAsync()
        {
            var rows = await _productRepository.GetAllProductsWithSuppliersAsync();
            var results = new List<Result>();

            foreach (var row in rows)
            {
                ProductInfo? info;
                try
                {
                    info = JsonSerializer.Deserialize<ProductInfo>(row.Info, JsonOptions);
                }
                catch (JsonException)
                {
                    // Skip rows with malformed JSON instead of failing the whole request.
                    continue;
                }

                if (info is null)
                {
                    continue;
                }

                results.Add(new Result
                {
                    CompanyName = row.CompanyName,
                    ProductName = info.ProductName,
                    Price = info.Price,
                    Category = info.Category,
                    OnSale = info.OnSale
                });
            }

            return results;
        }
    }
}
