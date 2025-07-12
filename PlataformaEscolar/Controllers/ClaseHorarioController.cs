using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEscolar.Models;
using PlataformaEscolar.Services;

namespace PlataformaEscolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "3-Clases-Horarios")]
    public class ClaseHorarioController : ControllerBase
    {
        private readonly IClaseHorarioService _service;
        public ClaseHorarioController(IClaseHorarioService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Policy = "AdministradorPolicy")]
        public IActionResult Crear([FromBody] ClaseHorario claseHorario)
        {
            try
            {
                if (claseHorario == null)
                    return BadRequest("El horario no puede ser nulo.");
                // Aquí podrías agregar más validaciones
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdministradorPolicy")]
        public IActionResult Modificar(int id, [FromBody] ClaseHorario claseHorario)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("El ID debe ser un número positivo.");
                if (claseHorario == null)
                    return BadRequest("El horario no puede ser nulo.");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "AdministradorPolicy")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpGet("mias")]
        [Authorize(Policy = "ProfesorPolicy,AlumnoPolicy")]
        public IActionResult MisClases()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
    }
} 