namespace csce360ChrisExampleAPI.Models
{
    // Maps a row from dbo.Suppliers.
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string VendorCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}