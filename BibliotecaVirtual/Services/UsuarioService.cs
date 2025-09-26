using Microsoft.EntityFrameworkCore;
using BibliotecaVirtual.Data;
using BibliotecaVirtual.Models;
using BibliotecaVirtual.DTOs;
using BC = BCrypt.Net.BCrypt;

namespace BibliotecaVirtual.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly BibliotecaDbContext _context;

        public UsuarioService(BibliotecaDbContext context)
        {
            _context = context;
        }

        // RF1: Registrar usuarios
        public async Task<bool> RegistrarUsuarioAsync(RegistroUsuarioDto dto)
        {
            // Verificar si el correo ya existe
            var existeUsuario = await _context.Usuarios
                .AnyAsync(u => u.Correo == dto.Correo);

            if (existeUsuario) return false;

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                ContrasenaHash = BC.HashPassword(dto.Contrasena), // RNF1: Cifrado
                Tipo = dto.Tipo,
                FechaRegistro = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        // RF2: Iniciar sesión
        public async Task<Usuario?> ValidarUsuarioAsync(LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == dto.Correo);

            if (usuario == null || !BC.Verify(dto.Contrasena, usuario.ContrasenaHash))
                return null;

            return usuario;
        }
    }
}