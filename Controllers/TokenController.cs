using Microsoft.AspNetCore.Mvc;
using Reboost.Services;
using System;
using System.Linq;

namespace Reboost.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly ReboostDbContext _context;

        public TokenController(TokenService tokenService, ReboostDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpGet("GetTemporaryToken")]
        public IActionResult GetTokenValue(int fkCabinetId, int fkBatteryId, int fkUserId)
        {
            try
            {
                var tokenValue = _tokenService.GetTokenValue(fkCabinetId, fkBatteryId, fkUserId);
                if (tokenValue != null)
                {
                    return Ok(tokenValue);
                }
                return NotFound("Token could not be generated.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to generate token: {ex.Message}");
            }
        }

        [HttpPost("GetTemporaryTokenInfo/{token}")]
        public IActionResult GetTokenInfo(string token)
        {
            try
            {
                var tokenEntity = _context.Tokens.FirstOrDefault(t => t.Value == token);
                if (tokenEntity == null)
                {
                    return NotFound("Token not found");
                }

                return Ok(new
                {
                    UserId = tokenEntity.FkUserId,
                    BatteryId = tokenEntity.FkBatteryId,
                    CabinetId = tokenEntity.FkCabinetId
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve token info: {ex.Message}");
            }
        }
    }
}
