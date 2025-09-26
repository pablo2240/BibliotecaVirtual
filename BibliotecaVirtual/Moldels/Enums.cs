namespace BibliotecaVirtual.Moldels
{
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

    public enum EstadoPrestamo
    {
        Activo = 1,
        Devuelto = 2,
        Vencido = 3
    }
}
