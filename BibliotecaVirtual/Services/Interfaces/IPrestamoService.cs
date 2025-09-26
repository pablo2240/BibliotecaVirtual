using BibliotecaVirtual.Models;
using BibliotecaVirtual.DTOs;

namespace BibliotecaVirtual.Services
{
    public interface IPrestamoService
    {
        Task<bool> ReservarLibroAsync(PrestamoDto dto);
        Task<bool> PrestarLibroAsync(int libroId);
        Task<bool> DevolverLibroAsync(int prestamoId);
        Task<List<ReportePrestamoDto>> GenerarReporteActivosAsync();
    }
}