using BibliotecaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestion.Models;
using SistemaGestion.Services;

namespace SistemaGestion.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ApiService _apiService;

        public AdminModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<Curso> Cursos { get; set; } = new();
        public List<Usuario> Usuarios { get; set; } = new();
        public int TotalCursos { get; set; }
        public int TotalUsuarios { get; set; }
        public int TotalMatriculas { get; set; }
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            var role = HttpContext.Session.GetString("UsuarioRole");

            if (string.IsNullOrEmpty(token) || role != "admin")
                return RedirectToPage("/Index");

            _apiService.SetAuthToken(token);

            try
            {
                Cursos = await _apiService.GetCursosAsync();
                TotalCursos = Cursos.Count;

                Usuarios = await _apiService.GetUsuariosAsync();
                TotalUsuarios = Usuarios.Count;

                // Para matrículas, podrías crear un endpoint específico o calcularlo
                TotalMatriculas = 0; // Placeholder
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar datos: {ex.Message}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCrearCursoAsync(string titulo, string profesor, decimal precio, int cupos, string descripcion)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Index");

            _apiService.SetAuthToken(token);

            try
            {
                var nuevoCurso = new Curso
                {
                    Title = titulo,
                    Teacher = profesor,
                    Price = precio,
                    Seats = cupos,
                    Description = descripcion
                };

                var response = await _apiService.CreateCursoAsync(nuevoCurso);

                if (response.Error != null)
                {
                    ErrorMessage = response.Error;
                }
                else
                {
                    Message = "Curso creado exitosamente";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al crear curso: {ex.Message}";
            }

            return await OnGetAsync();
        }

        public async Task<IActionResult> OnPostEliminarCursoAsync(int cursoId)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Index");

            _apiService.SetAuthToken(token);

            try
            {
                var response = await _apiService.DeleteCursoAsync(cursoId);

                if (response.Error != null)
                {
                    ErrorMessage = response.Error;
                }
                else
                {
                    Message = "Curso eliminado exitosamente";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al eliminar curso: {ex.Message}";
            }

            return await OnGetAsync();
        }

        public async Task<IActionResult> OnPostDevolverPrestamoAsync(int prestamoId)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Index");

            _apiService.SetAuthToken(token);

            try
            {
                var response = await _apiService.DevolverPrestamoAsync(prestamoId);

                if (response.Error != null)
                {
                    ErrorMessage = $"Error: {response.Error}";
                }
                else
                {
                    Message = $"Préstamo #{prestamoId} devuelto exitosamente";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al devolver préstamo: {ex.Message}";
            }

            return await OnGetAsync();
        }
    }
}