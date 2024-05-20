using Microsoft.AspNetCore.Mvc;

namespace Reboost.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CabinetController : ControllerBase
    {
        private readonly CabinetService _cabinetService;

        public CabinetController(CabinetService cabinetService)
        {
            _cabinetService = cabinetService;
        }

        [HttpPost]
        public IActionResult PostCabinet([FromBody] Cabinet cabinet)
        {
            try
            {
                _cabinetService.PostCabinet(cabinet);
                return Ok("Gabinete cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao cadastrar o gabinete: " + ex.Message);
            }
        }

        [HttpGet("{cabinetId}/Batteries")]
        public IActionResult GetBatteriesByCabinetId(int cabinetId)
        {
            try
            {
                var batteries = _cabinetService.GetBatteriesByCabinetId(cabinetId);

                if (batteries == null || batteries.Count == 0)
                {
                    return NotFound("No batteries found for the given cabinet ID.");
                }

                return Ok(batteries);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving batteries: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCabinetById(int id)
        {
            try
            {
                var cabinet = _cabinetService.GetCabinetById(id);
                if (cabinet == null)
                {
                    return NotFound("Gabinete não encontrado.");
                }
                return Ok(cabinet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter o gabinete: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllCabinets([FromQuery] int? cabinetId, [FromQuery] string? addressZipCode)
        {
            try
            {
                var cabinets = _cabinetService.GetFilteredCabinets(cabinetId, addressZipCode);
                return Ok(cabinets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter os gabinetes: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCabinet(int id)
        {
            try
            {
                var result = _cabinetService.DeleteCabinet(id);
                if (!result)
                {
                    return NotFound("Gabinete não encontrado.");
                }
                return Ok("Gabinete deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao deletar o gabinete: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCabinet(int id, [FromBody] Cabinet cabinet)
        {
            try
            {
                var updatedCabinet = _cabinetService.UpdateCabinet(id, cabinet);
                if (updatedCabinet != null)
                {
                    return Ok(updatedCabinet);
                }
                return NotFound("Armário não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao atualizar o armário: " + ex.Message);
            }
        }

        [HttpPut("SoftDelete/{id}")]
        public IActionResult SoftDeleteCabinet(int id)
        {
            try
            {
                var result = _cabinetService.SoftDeleteCabinet(id);
                if (!result)
                {
                    return NotFound("Gabinete não encontrado.");
                }
                return Ok("Gabinete desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao desativar o gabinete: " + ex.Message);
            }
        }
    }
}
