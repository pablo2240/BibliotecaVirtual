using BibliotecaVirtual.DTOs;
using BibliotecaVirtual.Moldels;

namespace BibliotecaVirtual.Services
{
    public interface ILibroService
    {
        Task<List<Libro>> ConsultarCatalogoAsync(ConsultaLibroDto dto);
    }
}