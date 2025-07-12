using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEscolar.Models;
using PlataformaEscolar.Repositories;

namespace PlataformaEscolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AlumnoPolicy")]
    [ApiExplorerSettings(GroupName = "6-Alumnos")]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoRepository _repo;
        public AlumnoController(IAlumnoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var alumnos = await _repo.GetAllAsync();
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Alumno alumno)
        {
            try
            {
                if (alumno == null)
                    return BadRequest("El alumno no puede ser nulo.");
                if (string.IsNullOrWhiteSpace(alumno.Nombre))
                    return BadRequest("El nombre del alumno no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(alumno.Email))
                    return BadRequest("El correo electrónico no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(alumno.Password))
                    return BadRequest("La contraseña no puede estar vacía.");
                var creado = await _repo.AddAsync(alumno);
                return Ok(creado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Alumno alumno)
        {
            try
            {
                if (alumno == null)
                    return BadRequest("El alumno no puede ser nulo.");
                if (alumno.Id <= 0)
                    return BadRequest("El ID debe ser un número positivo.");
                if (string.IsNullOrWhiteSpace(alumno.Nombre))
                    return BadRequest("El nombre del alumno no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(alumno.Email))
                    return BadRequest("El correo electrónico no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(alumno.Password))
                    return BadRequest("La contraseña no puede estar vacía.");
                var actualizado = await _repo.UpdateAsync(alumno);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("El ID debe ser un número positivo.");
                var eliminado = await _repo.DeleteAsync(id);
                if (!eliminado)
                    return NotFound("No se encontró el alumno a eliminar.");
                return Ok("Alumno eliminado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
    }
} 