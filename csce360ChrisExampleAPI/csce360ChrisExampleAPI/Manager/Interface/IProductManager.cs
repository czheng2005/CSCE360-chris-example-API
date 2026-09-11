using csce360ChrisExampleAPI.Models;

namespace csce360ChrisExampleAPI.Manager.Interface
{
    public interface IProductManager
    {
        Task<IEnumerable<Result>> GetAllResultsAsync(ProductFilter? filter = null);

        Task<IEnumerable<string>> GetAllCategoryNamesAsync();

        Task<IEnumerable<string>> GetAllCompanyNamesAsync();
    }
}