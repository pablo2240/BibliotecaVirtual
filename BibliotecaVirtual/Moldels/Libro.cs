using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Moldels
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(100)]
        public string Autor { get; set; }

        [Required]
        [StringLength(50)]
        public string Categoria { get; set; }

        [Required]
        public EstadoLibro Estado { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación con Prestamos (1..*)
        public virtual ICollection<Prestamo> Prestamos { get; set; }
    }
}
