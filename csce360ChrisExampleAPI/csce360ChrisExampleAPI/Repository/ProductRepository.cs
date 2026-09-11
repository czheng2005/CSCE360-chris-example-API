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

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            const string sql = @"
                SELECT CategoryID, CategoryName, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy
                FROM dbo.Categories
                ORDER BY CategoryName;";

            var results = new List<Category>();

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new Category
                {
                    CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                    CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                    CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                    CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                    UpdatedOn = reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
                    UpdatedBy = reader.GetString(reader.GetOrdinal("UpdatedBy"))
                });
            }

            return results;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            const string sql = @"
                SELECT SupplierID, VendorCode, CompanyName, ContactName, Region,
                       CreatedOn, CreatedBy, UpdatedOn, UpdatedBy
                FROM dbo.Suppliers
                ORDER BY CompanyName;";

            var results = new List<Supplier>();

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new Supplier
                {
                    SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                    VendorCode = reader.GetString(reader.GetOrdinal("VendorCode")),
                    CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                    ContactName = reader.GetString(reader.GetOrdinal("ContactName")),
                    Region = reader.GetString(reader.GetOrdinal("Region")),
                    CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                    CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                    UpdatedOn = reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
                    UpdatedBy = reader.GetString(reader.GetOrdinal("UpdatedBy"))
                });
            }

            return results;
        }
    }
}