using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEscolar.Models;
using PlataformaEscolar.Services;

namespace PlataformaEscolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "5-Comentarios")]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _service;
        public ComentarioController(IComentarioService service)
        {
            _service = service;
        }

        [HttpPost("a-profesor")]
        [Authorize(Policy = "AlumnoPolicy")]
        public IActionResult ComentarAProfesor([FromBody] Comentario comentario)
        {
            try
            {
                if (comentario == null)
                    return BadRequest("El comentario no puede ser nulo.");
                // Aquí podrías agregar más validaciones
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpPost("a-alumno")]
        [Authorize(Policy = "ProfesorPolicy")]
        public IActionResult ComentarAAlumno([FromBody] Comentario comentario)
        {
            try
            {
                if (comentario == null)
                    return BadRequest("El comentario no puede ser nulo.");
                // Aquí podrías agregar más validaciones
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "AdministradorPolicy")]
        public async Task<IActionResult> VerTodos()
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
    }
} 