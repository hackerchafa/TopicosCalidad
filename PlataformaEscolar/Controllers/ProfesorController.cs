using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEscolar.Models;
using PlataformaEscolar.Repositories;

namespace PlataformaEscolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ProfesorPolicy")]
    [ApiExplorerSettings(GroupName = "7-Profesores")]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorRepository _repo;
        public ProfesorController(IProfesorRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var profesores = await _repo.GetAllAsync();
                return Ok(profesores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Profesor profesor)
        {
            try
            {
                if (profesor == null)
                    return BadRequest("El profesor no puede ser nulo.");
                if (string.IsNullOrWhiteSpace(profesor.Nombre))
                    return BadRequest("El nombre del profesor no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(profesor.Email))
                    return BadRequest("El correo electrónico no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(profesor.Password))
                    return BadRequest("La contraseña no puede estar vacía.");
                var creado = await _repo.AddAsync(profesor);
                return Ok(creado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Profesor profesor)
        {
            try
            {
                if (profesor == null)
                    return BadRequest("El profesor no puede ser nulo.");
                if (profesor.Id <= 0)
                    return BadRequest("El ID debe ser un número positivo.");
                if (string.IsNullOrWhiteSpace(profesor.Nombre))
                    return BadRequest("El nombre del profesor no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(profesor.Email))
                    return BadRequest("El correo electrónico no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(profesor.Password))
                    return BadRequest("La contraseña no puede estar vacía.");
                var actualizado = await _repo.UpdateAsync(profesor);
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
                    return NotFound("No se encontró el profesor a eliminar.");
                return Ok("Profesor eliminado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
    }
} 