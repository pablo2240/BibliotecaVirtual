using BibliotecaVirtual.DTOs;
using BibliotecaVirtual.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaVirtual.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamosController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpPost("reservar")]
        public async Task<IActionResult> ReservarLibro([FromBody] PrestamoDto dto)
        {
            var resultado = await _prestamoService.ReservarLibroAsync(dto);

            if (!resultado)
                return BadRequest("No se pudo reservar el libro");

            return Ok("Libro reservado exitosamente");
        }

        [HttpPut("{libroId}/prestar")]
        public async Task<IActionResult> PrestarLibro(int libroId)
        {
            var resultado = await _prestamoService.PrestarLibroAsync(libroId);

            if (!resultado)
                return NotFound("Libro no encontrado");

            return Ok("Libro prestado exitosamente");
        }

        [HttpPut("{prestamoId}/devolver")]
        public async Task<IActionResult> DevolverLibro(int prestamoId)
        {
            var resultado = await _prestamoService.DevolverLibroAsync(prestamoId);

            if (!resultado)
                return NotFound("Préstamo no encontrado");

            return Ok("Libro devuelto exitosamente");
        }

        [HttpGet("reporte-activos")]
        public async Task<IActionResult> GenerarReporteActivos()
        {
            var reporte = await _prestamoService.GenerarReporteActivosAsync();
            return Ok(reporte);
        }
    }
}

