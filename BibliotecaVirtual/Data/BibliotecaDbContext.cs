using BibliotecaVirtual.Models;
using BibliotecaVirtual.Moldels;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaVirtual.Data
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Prestamos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Libro)
                .WithMany(l => l.Prestamos)
                .HasForeignKey(p => p.LibroId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices para mejorar rendimiento (RNF3)
            modelBuilder.Entity<Libro>()
                .HasIndex(l => l.Titulo);

            modelBuilder.Entity<Libro>()
                .HasIndex(l => l.Autor);

            modelBuilder.Entity<Libro>()
                .HasIndex(l => l.Categoria);

            // ========================================
            // 📚 SEED DATA - DATOS INICIALES
            // ========================================

            // Crear libros iniciales
            modelBuilder.Entity<Libro>().HasData(
                new Libro
                {
                    Id = 1,
                    Titulo = "Don Quijote de La Mancha",
                    Autor = "Miguel de Cervantes",
                    Categoria = "Literatura Clásica",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (30/9/2023)
                },
                new Libro
                {
                    Id = 2,
                    Titulo = "Cien Años de Soledad",
                    Autor = "Gabriel García Márquez",
                    Categoria = "Literatura Latinoamericana",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (13/2/2022)
                },
                new Libro
                {
                    Id = 3,
                    Titulo = "1984",
                    Autor = "George Orwell",
                    Categoria = "Ciencia Ficción",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (31/5/2023)
                },
                new Libro
                {
                    Id = 4,
                    Titulo = "El Principito",
                    Autor = "Antoine de Saint-Exupéry",
                    Categoria = "Literatura Juvenil",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (5/1/2023)
                },
                new Libro
                {
                    Id = 5,
                    Titulo = "Fundamentos de Programación",
                    Autor = "Luis Joyanes Aguilar",
                    Categoria = "Informática",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (2/11/2023)
                },
                new Libro
                {
                    Id = 6,
                    Titulo = "Estructura de Datos y Algoritmos",
                    Autor = "Thomas H. Cormen",
                    Categoria = "Informática",
                    Estado = EstadoLibro.Prestado,
                    FechaRegistro = new DateTime (21/8/2021)
                },
                new Libro
                {
                    Id = 7,
                    Titulo = "Base de Datos: Diseño y Implementación",
                    Autor = "Abraham Silberschatz",
                    Categoria = "Informática",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (2/20/2020)
                },
                new Libro
                {
                    Id = 8,
                    Titulo = "Historia de Colombia",
                    Autor = "Jorge Orlando Melo",
                    Categoria = "Historia",
                    Estado = EstadoLibro.Reservado,
                    FechaRegistro = new DateTime (3/9/2021)
                },
                new Libro
                {
                    Id = 9,
                    Titulo = "Cálculo Diferencial e Integral",
                    Autor = "Michael Spivak",
                    Categoria = "Matemáticas",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (3/23/2023)
                },
                new Libro
                {
                    Id = 10,
                    Titulo = "Física Universitaria",
                    Autor = "Hugh D. Young",
                    Categoria = "Física",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (3/1/2021)
                },
                new Libro
                {
                    Id = 11,
                    Titulo = "El Método Ronaldo",
                    Autor = "Cristiano Ronaldo",
                    Categoria = "Deporte",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (12/3/2023)
                },
                new Libro
                {
                    Id = 12,
                    Titulo = "Relentless: From Good to Great to Unstoppable",
                    Autor = "Tim S. Grover",
                    Categoria = "Deporte",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (11/11/2021)
                },
                new Libro
                {
                    Id = 13,
                    Titulo = "Programación en C# Paso a Paso",
                    Autor = "Joyanes Aguilar",
                    Categoria = "Programación",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (2/2/2022)
                },
                new Libro
                {
                    Id = 14,
                    Titulo = "Clean Code: A Handbook of Agile Software Craftsmanship",
                    Autor = "Robert C. Martin",
                    Categoria = "Programación",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (5/5/2025)
                },
                new Libro
                {
                    Id = 15,
                    Titulo = "Introducción a la Inteligencia Artificial",
                    Autor = "Stuart Russell",
                    Categoria = "Programación",
                    Estado = EstadoLibro.Reservado,
                    FechaRegistro = new DateTime (3/9/2023)
                },
                new Libro
                {
                    Id = 16,
                    Titulo = "Cocina Fácil para Estudiantes",
                    Autor = "Karlos Arguiñano",
                    Categoria = "Cocina",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (3/9/2023)
                },
                new Libro
                {
                    Id = 17,
                    Titulo = "Mastering the Art of French Cooking",
                    Autor = "Julia Child",
                    Categoria = "Cocina",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (3/9/2023)
                },
                new Libro
                {
                    Id = 18,
                    Titulo = "Pan Casero: Recetas Tradicionales",
                    Autor = "Ibán Yarza",
                    Categoria = "Cocina",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (4/3/2022)
                },
                new Libro
                {
                    Id = 19,
                    Titulo = "Nutrición y Dietética Deportiva",
                    Autor = "Nancy Clark",
                    Categoria = "Deporte",
                    Estado = EstadoLibro.Prestado,
                    FechaRegistro = new DateTime (1/2/2023)
                },
                new Libro
                {
                    Id = 20,
                    Titulo = "Recetas Saludables para Cada Día",
                    Autor = "Jamie Oliver",
                    Categoria = "Cocina",
                    Estado = EstadoLibro.Disponible,
                    FechaRegistro = new DateTime (12/6/2025)
                }
            );
    
            base.OnModelCreating(modelBuilder);
        }
    }
}