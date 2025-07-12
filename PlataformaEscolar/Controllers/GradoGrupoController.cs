using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEscolar.Models;
using PlataformaEscolar.Services;

namespace PlataformaEscolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "2-Grado-Grupo")]
    public class GradoGrupoController : ControllerBase
    {
        private readonly IGradoGrupoService _service;
        public GradoGrupoController(IGradoGrupoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear([FromBody] GradoGrupo gradoGrupo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(gradoGrupo.Nombre))
                    return BadRequest("El nombre del grado/grupo no puede estar vacío.");
                if (gradoGrupo.Nombre.Any(char.IsDigit))
                    return BadRequest("El nombre del grado/grupo no puede contener números.");
                // Aquí podrías agregar más validaciones si lo deseas
                var creado = await _service.AddAsync(gradoGrupo);
                return Ok(creado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Modificar(int id, [FromBody] GradoGrupo gradoGrupo)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("El ID debe ser un número positivo.");
                if (string.IsNullOrWhiteSpace(gradoGrupo.Nombre))
                    return BadRequest("El nombre del grado/grupo no puede estar vacío.");
                var actualizado = await _service.UpdateAsync(gradoGrupo);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
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

        [HttpGet("mios")]
        [Authorize(Roles = "Alumno,Profesor")]
        public async Task<IActionResult> MisGrupos()
        {
            try
            {
                // Aquí podrías filtrar por usuario autenticado si lo deseas
                var todos = await _service.GetAllAsync();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
    }
} 