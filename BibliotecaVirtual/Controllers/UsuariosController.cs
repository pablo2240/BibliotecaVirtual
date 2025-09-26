using Microsoft.AspNetCore.Mvc;
using BibliotecaVirtual.Services;
using BibliotecaVirtual.DTOs;

namespace BibliotecaVirtual.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroUsuarioDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _usuarioService.RegistrarUsuarioAsync(dto);

            if (!resultado)
                return Conflict("El correo ya está registrado");

            return Ok("Usuario registrado exitosamente");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _usuarioService.ValidarUsuarioAsync(dto);

            if (usuario == null)
                return Unauthorized("Credenciales inválidas");

            // Aquí normalmente generarías un JWT token
            return Ok(new { usuario.Id, usuario.Nombre, usuario.Tipo });
        }
    }
}