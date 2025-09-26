using BibliotecaVirtual.DTOs;
using BibliotecaVirtual.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaVirtual.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService _libroService;

        public LibrosController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet("catalogo")]
        public async Task<IActionResult> ConsultarCatalogo([FromQuery] ConsultaLibroDto dto)
        {
            var libros = await _libroService.ConsultarCatalogoAsync(dto);
            return Ok(libros);
        }
    }
}
