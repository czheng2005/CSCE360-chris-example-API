namespace csce360ChrisExampleAPI.Models
{
    // Maps the JSON stored in the Products.Info column.
    // If your actual JSON keys use different casing (e.g. camelCase),
    // this still works because deserialization is case-insensitive
    // (see ProductManager). If the key NAMES differ (e.g. "name" instead
    // of "ProductName"), update the property names below to match.
    public class ProductInfo
    {
        public string ProductName { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool On_Sale { get; set; }
    }
}
