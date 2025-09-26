using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.DTOs
{
    // DTO para reserva/préstamo (RF4, RF5)
    public class PrestamoDto
    {
        [Required]
        public int LibroId { get; set; }

        [Required]
        public int UsuarioId { get; set; }
    }
}
