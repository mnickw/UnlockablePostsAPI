using Microsoft.AspNetCore.Mvc;
using System;
using UnlockablePostsAPI.InputModels;
using UnlockablePostsAPI.Services;

namespace UnlockablePostsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoncesController : ControllerBase
    {
        private readonly ILogger<NoncesController> _logger;
        private readonly IUsersService _usersService;
        private readonly INonceService _nonceService;

        public NoncesController(ILogger<NoncesController> logger,
            IUsersService usersService,
            INonceService nonceService)
        {
            _logger = logger;
            _usersService = usersService;
            _nonceService = nonceService;
        }

        [HttpGet("new", Name = nameof(GetNewNonce))]
        public async Task<ActionResult<Guid>> GetNewNonce()
        {
            bool result = _usersService.ValidateSignatureFromQueryString(HttpContext.Request.Query);
            
            if (result == false)
                return BadRequest("Can't validate signature.");

            var vk_user_id_str = HttpContext.Request.Query["vk_user_id"].FirstOrDefault();

            if (string.IsNullOrEmpty(vk_user_id_str))
                return BadRequest("No vk_user_id in query string.");

            if (!long.TryParse(vk_user_id_str, out var vk_user_id))
                return BadRequest("Can't parse vk_user_id to long.");

            var guid = await _nonceService.CreateNonce(vk_user_id);

            return guid;
        }

        [HttpPost("validate", Name = nameof(ValidateAndGetToken))]
        public async Task<ActionResult<bool>> ValidateAndGetToken([FromBody] NonceValidationDTO dto)
        {
            if (string.IsNullOrEmpty(dto.NewAddress) || string.IsNullOrEmpty(dto.SignedNonce))
                return BadRequest("Address or signed nonce was empty.");

            bool result = _usersService.ValidateSignatureFromQueryString(HttpContext.Request.Query);

            if (result == false)
                return BadRequest("Can't validate signature.");

            var vk_user_id_str = HttpContext.Request.Query["vk_user_id"].FirstOrDefault();

            if (string.IsNullOrEmpty(vk_user_id_str))
                return BadRequest("No vk_user_id in query string.");

            if (!long.TryParse(vk_user_id_str, out var vk_user_id))
                return BadRequest("Can't parse vk_user_id to long.");

            bool validationSucceed = await _nonceService.ValidateNonce(vk_user_id, dto.SignedNonce, dto.NewAddress);

            if (!validationSucceed)
                return BadRequest("Can't validate nonce.");

            long? existingVkUserId = await _usersService.CheckExistingUserForAddress(dto.NewAddress);
            if (existingVkUserId != null)
            {
                if (existingVkUserId == vk_user_id)
                    return true;
                return BadRequest("Another user already using this address."); ;
            }

            await _usersService.AddAddressForUser(vk_user_id, dto.NewAddress);

            return true;
        }
    }
}