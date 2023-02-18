using Microsoft.AspNetCore.Mvc;
using UnlockablePostsAPI.Services;

namespace UnlockablePostsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoncesController : ControllerBase
    {
        private readonly ILogger<NoncesController> _logger;
        private readonly IUsersService _usersService;

        public NoncesController(ILogger<NoncesController> logger,
            IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet("new", Name = nameof(GetNewNonce))]
        public async Task<IActionResult> GetNewNonce()
        {
            bool result = _usersService.ValidateSignatureFromQueryString(HttpContext.Request.Query);
            return Ok(result);
        }

        [HttpPost("validate", Name = nameof(ValidateAndGetToken))]
        public async Task<IActionResult> ValidateAndGetToken()
        {
            return BadRequest();
        }
    }
}