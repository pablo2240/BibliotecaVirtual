using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Frontend.Models
{
    // ViewModels para las vistas
    public class RegistroUsuarioViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecciona el tipo de usuario")]
        public int Tipo { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasena { get; set; } = string.Empty;
    }

    public class ConsultaLibroViewModel
    {
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Categoria { get; set; }
    }

    public class PrestamoViewModel
    {
        [Required(ErrorMessage = "El ID del libro es requerido")]
        public int LibroId { get; set; }

        public int UsuarioId { get; set; }
    }

    // DTOs para la API
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public int Tipo { get; set; }
    }

    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    public class ReportePrestamo
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public string LibroTitulo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public int Estado { get; set; }
    }

    public enum TipoUsuario
    {
        Estudiante = 1,
        Docente = 2
    }

    public enum EstadoLibro
    {
        Disponible = 1,
        Prestado = 2,
        Reservado = 3
    }
}