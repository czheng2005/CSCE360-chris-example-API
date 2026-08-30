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

        [HttpGet(Name = "GetAllResults")]
        public async Task<ActionResult<IEnumerable<Result>>> GetAllResults()
        {
            var results = await _productManager.GetAllResultsAsync();
            return Ok(results);
        }
    }
}
