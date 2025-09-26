using BibliotecaVirtual.Models;
using BibliotecaVirtual.DTOs;

namespace BibliotecaVirtual.Services
{
    public interface IUsuarioService
    {
        Task<bool> RegistrarUsuarioAsync(RegistroUsuarioDto dto);
        Task<Usuario?> ValidarUsuarioAsync(LoginDto dto);
    }
}