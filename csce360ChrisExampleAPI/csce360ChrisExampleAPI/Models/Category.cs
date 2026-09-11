namespace csce360ChrisExampleAPI.Models
{
    // Maps a row from dbo.Categories.
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}