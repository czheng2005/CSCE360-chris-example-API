using csce360ChrisExampleAPI.Models;

namespace csce360ChrisExampleAPI.Manager.Interface
{
    public interface IProductManager
    {
        Task<IEnumerable<Result>> GetAllResultsAsync();
    }
}
