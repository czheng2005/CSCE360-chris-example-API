using Microsoft.AspNetCore.Mvc;

namespace csce360ChrisExampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
      
        [HttpGet(Name = "GetAllResults")]
        public IEnumerable<Result> GetAllResults()
        {
            return null;
            
        }
    }
}
