using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlataformaEscolar.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PlataformaEscolar.Controllers
{
    [ApiExplorerSettings(GroupName = "1-Auth")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDto dto)
        {
            try
            {
                var errores = new List<string>();
                // Validación de UserName
                if (string.IsNullOrWhiteSpace(dto.UserName))
                    errores.Add("El nombre de usuario no puede estar vacío.");
                else if (!char.IsUpper(dto.UserName[0]))
                    errores.Add("El nombre de usuario debe comenzar con mayúscula.");
                else if (!dto.UserName.All(c => char.IsLetter(c)))
                    errores.Add("El nombre de usuario solo puede contener letras, sin números ni símbolos.");

                // Validación de Email
                if (string.IsNullOrWhiteSpace(dto.Email))
                    errores.Add("El correo electrónico no puede estar vacío.");
                else if (!dto.Email.Contains("@") || !dto.Email.EndsWith(".com"))
                    errores.Add("El correo electrónico debe contener '@' y terminar en '.com'.");

                // Validación de Rol (solo Alumno o Profesor)
                var rolesPublicos = new[] { "alumno", "profesor" };
                if (string.IsNullOrWhiteSpace(dto.Rol))
                    errores.Add("El rol no puede estar vacío.");
                else if (!rolesPublicos.Contains(dto.Rol.Trim().ToLower()))
                    errores.Add("El rol solo puede ser: Alumno o Profesor (no se permite Administrador en el registro público).");

                // Validación de Password (deja que Identity haga la validación de complejidad, pero muestra los errores en español)
                if (errores.Count > 0)
                    return BadRequest(errores);

                var user = new IdentityUser { UserName = dto.UserName, Email = dto.Email };
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    var erroresEsp = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        switch (error.Code)
                        {
                            case "PasswordTooShort":
                                erroresEsp.Add("La contraseña debe tener al menos 6 caracteres.");
                                break;
                            case "PasswordRequiresNonAlphanumeric":
                                erroresEsp.Add("La contraseña debe tener al menos un carácter especial.");
                                break;
                            case "PasswordRequiresLower":
                                erroresEsp.Add("La contraseña debe tener al menos una letra minúscula.");
                                break;
                            case "PasswordRequiresUpper":
                                erroresEsp.Add("La contraseña debe tener al menos una letra mayúscula.");
                                break;
                            case "PasswordRequiresDigit":
                                erroresEsp.Add("La contraseña debe tener al menos un número.");
                                break;
                            case "DuplicateUserName":
                                erroresEsp.Add("El nombre de usuario ya está registrado.");
                                break;
                            case "DuplicateEmail":
                                erroresEsp.Add("El correo electrónico ya está registrado.");
                                break;
                            default:
                                erroresEsp.Add(error.Description);
                                break;
                        }
                    }
                    return BadRequest(erroresEsp);
                }
                await _userManager.AddToRoleAsync(user, char.ToUpper(dto.Rol[0]) + dto.Rol.Substring(1).ToLower());
                return Ok("Usuario registrado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        // Endpoint protegido para registrar administradores
        [HttpPost("register-admin")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AuthRegisterDto dto)
        {
            try
            {
                var errores = new List<string>();
                // Validación de UserName
                if (string.IsNullOrWhiteSpace(dto.UserName))
                    errores.Add("El nombre de usuario no puede estar vacío.");
                else if (!char.IsUpper(dto.UserName[0]))
                    errores.Add("El nombre de usuario debe comenzar con mayúscula.");
                else if (!dto.UserName.All(c => char.IsLetter(c)))
                    errores.Add("El nombre de usuario solo puede contener letras, sin números ni símbolos.");

                // Validación de Email
                if (string.IsNullOrWhiteSpace(dto.Email))
                    errores.Add("El correo electrónico no puede estar vacío.");
                else if (!dto.Email.Contains("@") || !dto.Email.EndsWith(".com"))
                    errores.Add("El correo electrónico debe contener '@' y terminar en '.com'.");

                // Validación de Rol (solo Administrador)
                if (string.IsNullOrWhiteSpace(dto.Rol) || dto.Rol.Trim().ToLower() != "administrador")
                    errores.Add("El rol solo puede ser: Administrador (en este endpoint protegido).");

                if (errores.Count > 0)
                    return BadRequest(errores);

                var user = new IdentityUser { UserName = dto.UserName, Email = dto.Email };
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    var erroresEsp = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        switch (error.Code)
                        {
                            case "PasswordTooShort":
                                erroresEsp.Add("La contraseña debe tener al menos 6 caracteres.");
                                break;
                            case "PasswordRequiresNonAlphanumeric":
                                erroresEsp.Add("La contraseña debe tener al menos un carácter especial.");
                                break;
                            case "PasswordRequiresLower":
                                erroresEsp.Add("La contraseña debe tener al menos una letra minúscula.");
                                break;
                            case "PasswordRequiresUpper":
                                erroresEsp.Add("La contraseña debe tener al menos una letra mayúscula.");
                                break;
                            case "PasswordRequiresDigit":
                                erroresEsp.Add("La contraseña debe tener al menos un número.");
                                break;
                            case "DuplicateUserName":
                                erroresEsp.Add("El nombre de usuario ya está registrado.");
                                break;
                            case "DuplicateEmail":
                                erroresEsp.Add("El correo electrónico ya está registrado.");
                                break;
                            default:
                                erroresEsp.Add(error.Description);
                                break;
                        }
                    }
                    return BadRequest(erroresEsp);
                }
                await _userManager.AddToRoleAsync(user, "Administrador");
                return Ok("Administrador registrado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginDto dto)
        {
            try
            {
                var errores = new List<string>();
                // Validación de UserName
                if (string.IsNullOrWhiteSpace(dto.UserName))
                    errores.Add("El nombre de usuario no puede estar vacío.");
                else if (!char.IsUpper(dto.UserName[0]))
                    errores.Add("El nombre de usuario debe comenzar con mayúscula.");
                else if (!dto.UserName.All(c => char.IsLetter(c)))
                    errores.Add("El nombre de usuario solo puede contener letras, sin números ni símbolos.");
                // Validación de Password
                if (string.IsNullOrWhiteSpace(dto.Password))
                    errores.Add("La contraseña no puede estar vacía.");
                if (errores.Count > 0)
                    return BadRequest(errores);

                var user = await _userManager.FindByNameAsync(dto.UserName);
                if (user == null)
                    return Unauthorized("Usuario o contraseña incorrectos.");
                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
                if (!result.Succeeded)
                    return Unauthorized("Usuario o contraseña incorrectos.");
                // Validar rol del usuario
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Count == 0)
                    return Unauthorized("El usuario no tiene un rol asignado.");
                // Generar token solo si todo coincide
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };
                foreach (var rol in roles)
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
    }
} 