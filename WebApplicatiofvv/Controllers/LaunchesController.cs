using Microsoft.AspNetCore.Mvc;
using WebApplicatiofvv.Services;

namespace WebApplicatiofvv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaunchesController(LaunchService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await service.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await service.GetLaunchById(id);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
