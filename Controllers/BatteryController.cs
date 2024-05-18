using System;
using Microsoft.AspNetCore.Mvc;
using Reboost;

namespace Reboost.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BatteryController : ControllerBase
    {
        private readonly BatteryService _batteryService;

        public BatteryController(BatteryService batteryService)
        {
            _batteryService = batteryService;
        }

        [HttpPost]
        public IActionResult PostBattery([FromForm] Battery battery)
        {
            try
            {
                _batteryService.PostBattery(battery);
                return Ok("Bateria cadastrada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao cadastrar a bateria: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBatteryById(int id)
        {
            try
            {
                var battery = _batteryService.GetBatteryById(id);
                if (battery == null)
                {
                    return NotFound("Bateria não encontrada.");
                }
                return Ok(battery);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter a bateria: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllBatteries()
        {
            try
            {
                var batteries = _batteryService.GetAllBatteries();
                return Ok(batteries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter as baterias: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBattery(int id)
        {
            try
            {
                var result = _batteryService.DeleteBattery(id);
                if (!result)
                {
                    return NotFound("Bateria não encontrada.");
                }
                return Ok("Bateria deletada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao deletar a bateria: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBattery(int id, [FromBody] Battery battery)
        {
            try
            {
                var updatedBattery = _batteryService.UpdateBattery(id, battery);
                if (updatedBattery != null)
                {
                    return Ok(updatedBattery);
                }
                return NotFound("Bateria não encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao atualizar a bateria: " + ex.Message);
            }
        }

        [HttpPut("SoftDelete/{id}")]
        public IActionResult SoftDeleteBattery(int id)
        {
            try
            {
                var result = _batteryService.SoftDeleteBattery(id);
                if (!result)
                {
                    return NotFound("Bateria não encontrada.");
                }
                return Ok("Bateria desativada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao desativar a bateria: " + ex.Message);
            }
        }
    }
}
