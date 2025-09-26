using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestion.Models;
using SistemaGestion.Services;

namespace SistemaGestion.Pages
{
    public class CursosModel : PageModel
    {
        private readonly ApiService _apiService;

        public CursosModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<Curso> Cursos { get; set; } = new();
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Index");

            _apiService.SetAuthToken(token);

            try
            {
                Cursos = await _apiService.GetCursosAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar cursos: {ex.Message}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostMatricularAsync(int cursoId)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Index");

            _apiService.SetAuthToken(token);

            try
            {
                var response = await _apiService.MatricularseAsync(cursoId);

                if (response.Error != null)
                {
                    ErrorMessage = response.Error;
                }
                else
                {
                    Message = "🎉 ¡Te has matriculado exitosamente!";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al matricularse: {ex.Message}";
            }

            return await OnGetAsync();
        }
    }
}