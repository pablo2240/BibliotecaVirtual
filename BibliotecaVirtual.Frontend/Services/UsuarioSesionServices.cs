using BibliotecaVirtual.Frontend.Models;

public class UsuarioSesionService
{
    public Usuario? UsuarioActual { get; private set; }

    public event Action? OnChange;

    public void IniciarSesion(Usuario usuario)
    {
        UsuarioActual = usuario;
        OnChange?.Invoke();
    }

    public void CerrarSesion()
    {
        UsuarioActual = null;
        OnChange?.Invoke();
    }
}