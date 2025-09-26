using BibliotecaVirtual.Moldels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaVirtual.Models
{
    // Modelo Usuario - Representa estudiantes y docentes
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string ContrasenaHash { get; set; } // RNF1: Contraseñas cifradas

        [Required]
        public TipoUsuario Tipo { get; set; }

        public DateTime FechaRegistro { get; set; }

        // Relación con Prestamos (1..*)
        public virtual ICollection<Prestamo> Prestamos { get; set; }
    }
}
