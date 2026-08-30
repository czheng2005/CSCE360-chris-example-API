namespace csce360ChrisExampleAPI.Models
{
    // Raw row returned from the Products/Suppliers join.
    // The Manager layer is responsible for parsing Info into a Result.
    public class ProductWithSupplier
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty; // raw JSON from Products.Info
    }
}
