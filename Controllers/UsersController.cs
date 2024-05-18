using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reboost;

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
        public IActionResult PostUser([FromForm] User user)
        {
            try
            {
                _userService.PostUser(user);
                return Ok("Funcionário cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao cadastrar o funcionário: " + ex.Message);
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
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
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
