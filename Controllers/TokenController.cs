using Microsoft.AspNetCore.Mvc;
using Reboost.Services;

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
            var tokenValue = _tokenService.GetTokenValue(fkCabinetId, fkBatteryId, fkUserId);
            if (tokenValue != null)
            {
                return Ok(tokenValue);
            }
            return NotFound("Token could not be generated.");
        }

        [HttpPost("GetTemporaryTokenInfo/{token}")]
        public IActionResult GetTokenInfo(string token)
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
    }
}
