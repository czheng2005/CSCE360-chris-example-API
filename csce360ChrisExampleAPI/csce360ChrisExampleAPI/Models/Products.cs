namespace csce360ChrisExampleAPI.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string VendorCode { get; set; }
        public DateTime CreatedOn{ get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string Info { get; set; }   
    }
}
