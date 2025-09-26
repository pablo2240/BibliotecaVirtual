using BibliotecaVirtual.Moldels;
using System.ComponentModel.DataAnnotations;

    namespace BibliotecaVirtual.DTOs
    {
        // DTO para registro de usuario (RF1)
        public class RegistroUsuarioDto
        {
            [Required]
            public string Nombre { get; set; }

            [Required]
            [EmailAddress]
            public string Correo { get; set; }

            [Required]
            [MinLength(6)]
            public string Contrasena { get; set; }

            [Required]
            public TipoUsuario Tipo { get; set; }
        }
    }
