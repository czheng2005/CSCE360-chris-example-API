namespace csce360ChrisExampleAPI.Models
{
    // Optional filter criteria for GET /Product.
    // PriceRange always defaults to an "open" range (0 to decimal.MaxValue),
    // so it can be applied unconditionally. Category/OnSale/CompanyName left
    // null are not applied as filters.
    public class ProductFilter
    {
        public RangeFilter PriceRange { get; set; } = new RangeFilter();
        public string? Category { get; set; }
        public bool? OnSale { get; set; }
        public string? CompanyName { get; set; }
    }
}