using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotecaVirtual.Migrations
{
    /// <inheritdoc />
    public partial class AgregarSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "Autor", "Categoria", "Estado", "FechaRegistro", "Titulo" },
                values: new object[,]
                {
                    { 1, "Miguel de Cervantes", "Literatura Clásica", 1, new DateTime(2025, 9, 24, 23, 25, 50, 922, DateTimeKind.Local).AddTicks(2833), "Don Quijote de La Mancha" },
                    { 2, "Gabriel García Márquez", "Literatura Latinoamericana", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(443), "Cien Años de Soledad" },
                    { 3, "George Orwell", "Ciencia Ficción", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(464), "1984" },
                    { 4, "Antoine de Saint-Exupéry", "Literatura Juvenil", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(467), "El Principito" },
                    { 5, "Luis Joyanes Aguilar", "Informática", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(472), "Fundamentos de Programación" },
                    { 6, "Thomas H. Cormen", "Informática", 2, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(474), "Estructura de Datos y Algoritmos" },
                    { 7, "Abraham Silberschatz", "Informática", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(476), "Base de Datos: Diseño y Implementación" },
                    { 8, "Jorge Orlando Melo", "Historia", 3, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(479), "Historia de Colombia" },
                    { 9, "Michael Spivak", "Matemáticas", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(481), "Cálculo Diferencial e Integral" },
                    { 10, "Hugh D. Young", "Física", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(483), "Física Universitaria" },
                    { 11, "Cristiano Ronaldo", "Deporte", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(485), "El Método Ronaldo" },
                    { 12, "Tim S. Grover", "Deporte", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(488), "Relentless: From Good to Great to Unstoppable" },
                    { 13, "Joyanes Aguilar", "Programación", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(490), "Programación en C# Paso a Paso" },
                    { 14, "Robert C. Martin", "Programación", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(492), "Clean Code: A Handbook of Agile Software Craftsmanship" },
                    { 15, "Stuart Russell", "Programación", 3, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(495), "Introducción a la Inteligencia Artificial" },
                    { 16, "Karlos Arguiñano", "Cocina", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(497), "Cocina Fácil para Estudiantes" },
                    { 17, "Julia Child", "Cocina", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(499), "Mastering the Art of French Cooking" },
                    { 18, "Ibán Yarza", "Cocina", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(502), "Pan Casero: Recetas Tradicionales" },
                    { 19, "Nancy Clark", "Deporte", 2, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(504), "Nutrición y Dietética Deportiva" },
                    { 20, "Jamie Oliver", "Cocina", 1, new DateTime(2025, 9, 24, 23, 25, 51, 63, DateTimeKind.Local).AddTicks(507), "Recetas Saludables para Cada Día" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
