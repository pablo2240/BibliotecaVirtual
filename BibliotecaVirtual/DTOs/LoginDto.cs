using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.DTOs
{
    //RF2
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contrasena { get; set; }
    }

}
