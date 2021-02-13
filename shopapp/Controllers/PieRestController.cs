using Microsoft.AspNetCore.Mvc;
using shopapp.Repositories;

namespace shopapp.Controllers
{
    [Route("api/pie")]
    [ApiController]
    public class PieRestController : ControllerBase
    {
        public readonly IPieRepository _pieRepository;

        public PieRestController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["search"].ToString();
                var pies =  _pieRepository.GetSearchPies(term);
                return Ok(pies);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
