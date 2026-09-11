namespace csce360ChrisExampleAPI.Models
{
    // Generic min/max range filter. Currently used for the price filter,
    // but shaped so it can back any other numeric range filter later.
    public class RangeFilter
    {
        // Minimum defaults to 0 if not provided
        public decimal MinPrice { get; set; } = 0;

        // Maximum defaults to decimal.MaxValue if not provided (i.e. no upper bound)
        public decimal MaxPrice { get; set; } = decimal.MaxValue;

        public bool IsValidRange => MaxPrice >= MinPrice;
    }
}