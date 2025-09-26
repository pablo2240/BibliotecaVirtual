using BibliotecaVirtual.Data;
using BibliotecaVirtual.DTOs;
using BibliotecaVirtual.Moldels;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaVirtual.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly BibliotecaDbContext _context;

        public PrestamoService(BibliotecaDbContext context)
        {
            _context = context;
        }

        // RF4: Reservar libro
        public async Task<bool> ReservarLibroAsync(PrestamoDto dto)
        {
            var libro = await _context.Libros.FindAsync(dto.LibroId);
            if (libro == null || libro.Estado != EstadoLibro.Disponible)
                return false;

            // Cambiar estado del libro
            libro.Estado = EstadoLibro.Reservado;

            var prestamo = new Prestamo
            {
                UsuarioId = dto.UsuarioId,
                LibroId = dto.LibroId,
                FechaInicio = DateTime.Now,
                Estado = EstadoPrestamo.Activo
            };

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            return true;
        }

        // RF5: Prestar libro
        public async Task<bool> PrestarLibroAsync(int libroId)
        {
            var libro = await _context.Libros.FindAsync(libroId);
            if (libro == null) return false;

            libro.Estado = EstadoLibro.Prestado;
            await _context.SaveChangesAsync();
            return true;
        }

        // RF6: Devolver libro
        public async Task<bool> DevolverLibroAsync(int prestamoId)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(p => p.Id == prestamoId);

            if (prestamo == null) return false;

            prestamo.FechaFin = DateTime.Now;
            prestamo.Estado = EstadoPrestamo.Devuelto;
            prestamo.Libro.Estado = EstadoLibro.Disponible;

            await _context.SaveChangesAsync();
            return true;
        }

        // RF7: Generar reporte de préstamos activos
        public async Task<List<ReportePrestamoDto>> GenerarReporteActivosAsync()
        {
            return await _context.Prestamos
                .Where(p => p.Estado == EstadoPrestamo.Activo)
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .Select(p => new ReportePrestamoDto
                {
                    Id = p.Id,
                    UsuarioNombre = p.Usuario.Nombre,
                    LibroTitulo = p.Libro.Titulo,
                    FechaInicio = p.FechaInicio,
                    Estado = p.Estado
                })
                .ToListAsync();
        }
    }
}