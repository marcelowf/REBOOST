using Microsoft.AspNetCore.Mvc;
using Reboost.Services;

namespace Reboost.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetTokenValue(int fkCabinetId, int fkBatteryId, int fkUserId)
        {
            var tokenValue = _tokenService.GetTokenValue(fkCabinetId, fkBatteryId, fkUserId);
            if (tokenValue != null)
            {
                return Ok(tokenValue);
            }
            return NotFound("Token could not be generated.");
        }

        [HttpGet("Change")]
        public IActionResult ChangeTokenValue(int fkCabinetId, int fkBatteryId, int fkUserId)
        {
            var tokenValue = _tokenService.ChangeTokenValue(fkCabinetId, fkBatteryId, fkUserId);
            if (tokenValue != null)
            {
                return Ok(tokenValue);
            }
            return NotFound("Token not found.");
        }
    }
}
