using BibliotecaVirtual.Moldels;

namespace BibliotecaVirtual.DTOs
{
    // DTO para reporte (RF7)
    public class ReportePrestamoDto
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; }
        public string LibroTitulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public EstadoPrestamo Estado { get; set; }
    }
}
