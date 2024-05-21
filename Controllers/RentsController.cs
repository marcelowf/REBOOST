using Microsoft.AspNetCore.Mvc;

namespace Reboost.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly RentService _rentService;

        public RentsController(RentService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost]
        public IActionResult RentBattery(Rent rent)
        {
            try
            {
                _rentService.RentBattery(rent);
                return Ok("Bateria alugada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao alugar a bateria: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRentById(int id)
        {
            try
            {
                var rent = _rentService.GetRentById(id);
                if (rent == null)
                {
                    return NotFound("Aluguel não encontrado.");
                }
                return Ok(rent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter o aluguel: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllRents(
            [FromQuery] int? id,
            [FromQuery] bool? isActive,
            [FromQuery] DateTime? beginDate,
            [FromQuery] DateTime? finishDate,
            [FromQuery] int? fkCabinetFromId,
            [FromQuery] int? fkCabinetToId,
            [FromQuery] int? fkUserId,
            [FromQuery] int? fkBatteryId)
        {
            try
            {
                var rents = _rentService.GetFilteredRents(id, isActive, beginDate, finishDate, fkCabinetFromId, fkCabinetToId, fkUserId, fkBatteryId);
                return Ok(rents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao obter os aluguéis: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRent(int id)
        {
            try
            {
                var result = _rentService.DeleteRent(id);
                if (!result)
                {
                    return NotFound("Aluguel não encontrado.");
                }
                return Ok("Aluguel excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao excluir o aluguel: " + ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateRent(int id, Rent rent)
        {
            try
            {
                var updatedRent = _rentService.UpdateRent(id, rent);
                if (updatedRent == null)
                {
                    return NotFound("Aluguel não encontrado.");
                }
                return Ok(updatedRent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao atualizar o aluguel: " + ex.Message);
            }
        }

        [HttpPut("SoftDelete/{id}")]
        public IActionResult SoftDeleteRent(int id)
        {
            try
            {
                var result = _rentService.SoftDeleteRent(id);
                if (!result)
                {
                    return NotFound("Aluguel não encontrado.");
                }
                return Ok("Aluguel desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao desativar o aluguel: " + ex.Message);
            }
        }
    }
}
