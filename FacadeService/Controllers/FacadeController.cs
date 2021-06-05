using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FacadeService.Controllers
{
    [Route("api/[controller]")]
    public class FacadeController : Controller
    {
        private readonly ILogger<FacadeController> _logger;

        public FacadeController(ILogger<FacadeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetFacade(string applicationId)
        {
            try
            {
                if (string.IsNullOrEmpty(applicationId))
                {
                    throw new Exception("Wrong input parameters");
                }

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostFacade(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    throw new Exception("Wrong input parameters");
                }

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}