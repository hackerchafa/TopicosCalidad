using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEscolar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PlataformaEscolar.Services;

namespace PlataformaEscolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "4-Calificaciones")]
    public class CalificacionController : ControllerBase
    {
        private readonly ICalificacionService _service;
        public CalificacionController(ICalificacionService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Policy = "ProfesorPolicy")]
        public IActionResult SubirCalificacion([FromBody] Calificacion calificacion)
        {
            try
            {
                if (calificacion == null)
                    return BadRequest("La calificación no puede ser nula.");
                // Aquí podrías agregar más validaciones
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpGet("mias")]
        [Authorize(Policy = "AlumnoPolicy")]
        public IActionResult RevisarMisCalificaciones()
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

        [HttpGet]
        [Authorize(Policy = "AdministradorPolicy")]
        public async Task<IActionResult> VerTodas()
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

        [AllowAnonymous]
        [HttpPost("login-test")]
        public IActionResult LoginTest([FromBody] string rol)
        {
            // Solo para pruebas: genera un token con el rol indicado
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Role, rol)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ClaveSuperSecretaParaJWT123!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "PlataformaEscolarIssuer",
                audience: "PlataformaEscolarAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
} 