using Microsoft.AspNetCore.Mvc;

namespace UnlockablePostsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoncesController : ControllerBase
    {
        private readonly ILogger<NoncesController> _logger;

        public NoncesController(ILogger<NoncesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("new", Name = nameof(GetNewNonce))]
        public async Task<IActionResult> GetNewNonce()
        {
            return BadRequest();
        }

        [HttpPost("validate", Name = nameof(ValidateAndGetToken))]
        public async Task<IActionResult> ValidateAndGetToken()
        {
            return BadRequest();
        }
    }
}