using BibliotecaVirtual.Data;
using BibliotecaVirtual.DTOs;
using BibliotecaVirtual.Moldels;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaVirtual.Services
{
    public class LibroService : ILibroService
    {
        private readonly BibliotecaDbContext _context;

        public LibroService(BibliotecaDbContext context)
        {
            _context = context;
        }

        // RF3: Consultar catálogo (optimizado para RNF3: < 3 segundos)
        public async Task<List<Libro>> ConsultarCatalogoAsync(ConsultaLibroDto dto)
        {
            var query = _context.Libros.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Titulo))
                query = query.Where(l => l.Titulo.Contains(dto.Titulo));

            if (!string.IsNullOrEmpty(dto.Autor))
                query = query.Where(l => l.Autor.Contains(dto.Autor));

            if (!string.IsNullOrEmpty(dto.Categoria))
                query = query.Where(l => l.Categoria.Contains(dto.Categoria));

            return await query.Take(100).ToListAsync(); // Limitar resultados para rendimiento
        }
    }
}
