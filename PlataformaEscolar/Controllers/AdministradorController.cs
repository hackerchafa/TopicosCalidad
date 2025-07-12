using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEscolar.Models;
using PlataformaEscolar.Repositories;

namespace PlataformaEscolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdministradorPolicy")]
    [ApiExplorerSettings(GroupName = "8-Administradores")]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorRepository _repo;
        public AdministradorController(IAdministradorRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var administradores = await _repo.GetAllAsync();
                return Ok(administradores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Administrador administrador)
        {
            try
            {
                if (administrador == null)
                    return BadRequest("El administrador no puede ser nulo.");
                if (string.IsNullOrWhiteSpace(administrador.Nombre))
                    return BadRequest("El nombre del administrador no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(administrador.Email))
                    return BadRequest("El correo electrónico no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(administrador.Password))
                    return BadRequest("La contraseña no puede estar vacía.");
                var creado = await _repo.AddAsync(administrador);
                return Ok(creado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Administrador administrador)
        {
            try
            {
                if (administrador == null)
                    return BadRequest("El administrador no puede ser nulo.");
                if (administrador.Id <= 0)
                    return BadRequest("El ID debe ser un número positivo.");
                if (string.IsNullOrWhiteSpace(administrador.Nombre))
                    return BadRequest("El nombre del administrador no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(administrador.Email))
                    return BadRequest("El correo electrónico no puede estar vacío.");
                if (string.IsNullOrWhiteSpace(administrador.Password))
                    return BadRequest("La contraseña no puede estar vacía.");
                var actualizado = await _repo.UpdateAsync(administrador);
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
                    return NotFound("No se encontró el administrador a eliminar.");
                return Ok("Administrador eliminado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
    }
} 