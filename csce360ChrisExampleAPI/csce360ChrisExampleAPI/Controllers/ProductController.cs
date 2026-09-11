using csce360ChrisExampleAPI.Manager.Interface;
using csce360ChrisExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace csce360ChrisExampleAPI.Controllers
{
    // GET /Product - returns products (optionally filtered by price range,
    // category, on-sale status, and/or company name).
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

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
    }

    // GET /Category - returns all category names from dbo.Categories,
    // for populating the category single-select filter dropdown.
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public CategoryController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet(Name = "GetAllCategories")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllCategories()
        {
            var categories = await _productManager.GetAllCategoryNamesAsync();
            return Ok(categories);
        }
    }

    // GET /Company - returns all company names from dbo.Suppliers,
    // for populating the company single-select filter dropdown.
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public CompanyController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet(Name = "GetAllCompanies")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllCompanies()
        {
            var companies = await _productManager.GetAllCompanyNamesAsync();
            return Ok(companies);
        }
    }
}