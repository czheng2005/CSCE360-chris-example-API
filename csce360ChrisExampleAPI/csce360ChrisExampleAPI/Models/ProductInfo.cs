using System.Text.Json.Serialization;

namespace csce360ChrisExampleAPI.Models
{
    // Maps the JSON stored in the Products.Info column, e.g.:
    // {"name": "Fuji Apples", "price": 4, "category": "grocery", "on_sale": false}
    //
    // Deserialization is case-insensitive (see ProductManager), which covers
    // Price/price, Category/category, and On_Sale/on_sale. The JSON key "name"
    // does NOT match "ProductName" by casing alone, so it needs an explicit
    // JsonPropertyName mapping.
    public class ProductInfo
    {
        [JsonPropertyName("name")]
        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool On_Sale { get; set; }
    }
}