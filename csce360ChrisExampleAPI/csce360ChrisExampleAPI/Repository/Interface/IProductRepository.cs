using csce360ChrisExampleAPI.Models;

namespace csce360ChrisExampleAPI.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductWithSupplier>> GetAllProductsWithSuppliersAsync();
    }
}
