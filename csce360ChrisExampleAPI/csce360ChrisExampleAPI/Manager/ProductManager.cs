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

        public async Task<IEnumerable<Result>> GetAllResultsAsync(ProductFilter? filter = null)
        {
            filter ??= new ProductFilter();

            var rows = await _productRepository.GetAllProductsWithSuppliersAsync();
            var results = new List<Result>();

            foreach (var row in rows)
            {
                var info = TryParseInfo(row.Info);
                if (info is null)
                {
                    continue;
                }

                var result = new Result
                {
                    CompanyName = row.CompanyName,
                    ProductName = info.ProductName,
                    Price = info.Price,
                    Category = info.Category,
                    OnSale = info.On_Sale
                };

                if (PassesFilter(result, filter))
                {
                    results.Add(result);
                }
            }

            return results;
        }

        public async Task<IEnumerable<string>> GetAllCategoryNamesAsync()
        {
            var categories = await _productRepository.GetAllCategoriesAsync();

            return categories
                .Select(c => c.CategoryName)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public async Task<IEnumerable<string>> GetAllCompanyNamesAsync()
        {
            var suppliers = await _productRepository.GetAllSuppliersAsync();

            return suppliers
                .Select(s => s.CompanyName)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static ProductInfo? TryParseInfo(string rawInfo)
        {
            try
            {
                return JsonSerializer.Deserialize<ProductInfo>(rawInfo, JsonOptions);
            }
            catch (JsonException)
            {
                // Skip rows with malformed JSON instead of failing the whole request.
                return null;
            }
        }

        private static bool PassesFilter(Result result, ProductFilter filter)
        {
            if (result.Price < filter.PriceRange.MinPrice || result.Price > filter.PriceRange.MaxPrice)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(filter.Category) &&
                !string.Equals(result.Category, filter.Category, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (filter.OnSale.HasValue && result.OnSale != filter.OnSale.Value)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(filter.CompanyName) &&
                !string.Equals(result.CompanyName, filter.CompanyName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}