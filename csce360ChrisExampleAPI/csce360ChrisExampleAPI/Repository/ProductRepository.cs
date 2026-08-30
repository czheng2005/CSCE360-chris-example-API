using csce360ChrisExampleAPI.Models;
using csce360ChrisExampleAPI.Repository.Interface;
using Microsoft.Data.SqlClient;

namespace csce360ChrisExampleAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not found in configuration.");
        }

        public async Task<IEnumerable<ProductWithSupplier>> GetAllProductsWithSuppliersAsync()
        {
            const string sql = @"
                SELECT s.CompanyName, p.Info
                FROM dbo.Products p
                INNER JOIN dbo.Suppliers s ON p.VendorCode = s.VendorCode;";

            var results = new List<ProductWithSupplier>();

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new ProductWithSupplier
                {
                    CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                    Info = reader.GetString(reader.GetOrdinal("Info"))
                });
            }

            return results;
        }
    }
}
