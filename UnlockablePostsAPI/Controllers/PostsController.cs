using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlockablePostsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet(Name = nameof(GetPosts))]
        public async Task<IActionResult> GetPosts()
        {
            return BadRequest();
        }
    }
}
