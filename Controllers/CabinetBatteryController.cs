using Microsoft.AspNetCore.Mvc;
using Reboost.Services;

namespace Reboost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CabinetBatteryController : ControllerBase
    {
        private readonly CabinetBatteryService _cabinetBatteryService;

        public CabinetBatteryController(CabinetBatteryService cabinetBatteryService)
        {
            _cabinetBatteryService = cabinetBatteryService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var cabinetBattery = _cabinetBatteryService.GetByIdCabinetBattery(id);
                return Ok(cabinetBattery);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetByCabinetId([FromQuery] int? cabinetId)
        {
            try
            {
                if (cabinetId == null)
                {
                    return BadRequest("CabinetId must be provided.");
                }

                var cabinetBatteries = _cabinetBatteryService.GetByCabinetId(cabinetId.Value);
                return Ok(cabinetBatteries);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(CabinetBattery cabinetBattery)
        {
            try
            {
                var createdCabinetBattery = _cabinetBatteryService.CreateCabinetBattery(cabinetBattery);
                return CreatedAtAction(nameof(GetById), new { id = createdCabinetBattery.Id }, createdCabinetBattery);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CabinetBattery cabinetBattery)
        {
            try
            {
                cabinetBattery.Id = id;
                _cabinetBatteryService.UpdateCabinetBattery(cabinetBattery);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cabinetBatteryService.DeleteCabinetBattery(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
