using csce360ChrisExampleAPI.Manager.Interface;
using csce360ChrisExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace csce360ChrisExampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        // GET /Product - returns products, optionally filtered by price range,
        // category, on-sale status, and/or company name via query string.
        [HttpGet(Name = "GetAllResults")]
        public async Task<ActionResult<IEnumerable<Result>>> GetAllResults(
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? category,
            [FromQuery] bool? onSale,
            [FromQuery] string? companyName)
        {
            var priceRange = new RangeFilter();
            if (minPrice.HasValue)
            {
                priceRange.MinPrice = minPrice.Value;
            }
            if (maxPrice.HasValue)
            {
                priceRange.MaxPrice = maxPrice.Value;
            }

            if (!priceRange.IsValidRange)
            {
                return BadRequest("minPrice cannot be greater than maxPrice.");
            }

            var filter = new ProductFilter
            {
                PriceRange = priceRange,
                Category = category,
                OnSale = onSale,
                CompanyName = companyName
            };

            var results = await _productManager.GetAllResultsAsync(filter);
            return Ok(results);
        }

        // POST /Product/search - same filtering as GET /Product, but takes the
        // filter criteria as a JSON body instead of a query string. Use this
        // when the filter is large/complex enough that a query string is unwieldy.
        [HttpPost("search", Name = "SearchProducts")]
        public async Task<ActionResult<IEnumerable<Result>>> SearchProducts([FromBody] ProductFilter? filter)
        {
            filter ??= new ProductFilter();

            if (!filter.PriceRange.IsValidRange)
            {
                return BadRequest("minPrice cannot be greater than maxPrice.");
            }

            var results = await _productManager.GetAllResultsAsync(filter);
            return Ok(results);
        }

        // GET /Category - returns all category names from dbo.Categories,
        // for populating the category single-select filter dropdown.
        // "~/" makes this an absolute route, ignoring the controller's
        // "[controller]" ("Product") base route.
        [HttpGet("~/Category", Name = "GetAllCategories")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllCategories()
        {
            var categories = await _productManager.GetAllCategoryNamesAsync();
            return Ok(categories);
        }

        // GET /Company - returns all company names from dbo.Suppliers,
        // for populating the company single-select filter dropdown.
        [HttpGet("~/Company", Name = "GetAllCompanies")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllCompanies()
        {
            var companies = await _productManager.GetAllCompanyNamesAsync();
            return Ok(companies);
        }
    }
}