using Microsoft.AspNetCore.Mvc;

namespace Reboost.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult PostUser(User user)
        {
            try
            {
                var createdUser = _userService.PostUser(user);
                if (createdUser == null)
                {
                    return Conflict("O e-mail fornecido já está em uso.");
                }
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o usuário: " + ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(string Email, string Password)
        {
            try
            {
                var User = _userService.ValidateUser(Email, Password);

                if (User == null)
                {
                    Console.WriteLine(Email, Password);
                    return Unauthorized();
                }

                var token = _userService.GenerateToken(User);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to login: {ex.Message}");
            }
        }

        [HttpPost("DecodeLoginToken")]
        public IActionResult DecodeLoginToken(string token)
        {
            try
            {
                var decodedInfo = _userService.DecodeToken(token);
                
                if (decodedInfo == null)
                {
                    return NotFound("Token not found");
                }

                var result = new
                {
                    Email = decodedInfo.Value.Email,
                    UserId = decodedInfo.Value.UserId
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to decode token: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound("Usuário não encontrado.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter o usuário: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUsers(
            [FromQuery] int? id,
            [FromQuery] bool? isAdmin,
            [FromQuery] bool? isActive,
            [FromQuery] float? billing,
            [FromQuery] string? name,
            [FromQuery] string? email,
            [FromQuery] string? password,
            [FromQuery] DateTime? lastLogin,
            [FromQuery] DateTime? createdAt)
        {
            try
            {
                var users = _userService.GetFilteredUsers(id, isAdmin, isActive, billing, name, email, password, lastLogin, createdAt);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter os usuários: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var result = _userService.DeleteUser(id);
                if (!result)
                {
                    return NotFound("Usuário não encontrado.");
                }
                return Ok("Usuário deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao deletar o usuário: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                var updatedUser = _userService.UpdateUser(id, user);
                if (updatedUser != null)
                {
                    return Ok(updatedUser);
                }
                return NotFound("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao atualizar o usuário: " + ex.Message);
            }
        }

        [HttpPut("SoftDelete/{id}")]
        public IActionResult SoftDeleteUser(int id)
        {
            try
            {
                var result = _userService.SoftDeleteUser(id);
                if (!result)
                {
                    return NotFound("Usuário não encontrado.");
                }
                return Ok("Usuário desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao desativar o usuário: " + ex.Message);
            }
        }
    }
}
