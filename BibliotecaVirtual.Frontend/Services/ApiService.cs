using BibliotecaVirtual.Frontend.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BibliotecaVirtual.Frontend.Services
{
    public interface IApiService
    {
        Task<bool> RegistrarUsuarioAsync(RegistroUsuarioViewModel modelo);
        Task<Usuario?> LoginAsync(LoginViewModel modelo);
        Task<List<Libro>> ConsultarCatalogoAsync(ConsultaLibroViewModel modelo);
        Task<bool> ReservarLibroAsync(PrestamoViewModel modelo);
        Task<bool> PrestarLibroAsync(int libroId);
        Task<bool> DevolverLibroAsync(int prestamoId);
        Task<List<ReportePrestamo>> GenerarReporteActivosAsync();
    }

    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7272/api";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<bool> RegistrarUsuarioAsync(RegistroUsuarioViewModel modelo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/usuarios/registro", modelo);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en registro: {ex.Message}");
                return false;
            }
        }

        public async Task<Usuario?> LoginAsync(LoginViewModel modelo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/usuarios/login", modelo);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Usuario>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en login: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Libro>> ConsultarCatalogoAsync(ConsultaLibroViewModel modelo)
        {
            try
            {
                var queryString = "";
                var parametros = new List<string>();

                if (!string.IsNullOrEmpty(modelo.Titulo))
                    parametros.Add($"titulo={Uri.EscapeDataString(modelo.Titulo)}");

                if (!string.IsNullOrEmpty(modelo.Autor))
                    parametros.Add($"autor={Uri.EscapeDataString(modelo.Autor)}");

                if (!string.IsNullOrEmpty(modelo.Categoria))
                    parametros.Add($"categoria={Uri.EscapeDataString(modelo.Categoria)}");

                if (parametros.Any())
                    queryString = "?" + string.Join("&", parametros);

                var response = await _httpClient.GetAsync($"/libros/catalogo{queryString}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Libro>>() ?? new List<Libro>();
                }

                return new List<Libro>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en consulta catálogo: {ex.Message}");
                return new List<Libro>();
            }
        }

        public async Task<bool> ReservarLibroAsync(PrestamoViewModel modelo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/prestamos/reservar", modelo);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al reservar libro: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> PrestarLibroAsync(int libroId)
        {
            try
            {
                var response = await _httpClient.PutAsync($"/prestamos/{libroId}/prestar", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al prestar libro: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DevolverLibroAsync(int prestamoId)
        {
            try
            {
                var response = await _httpClient.PutAsync($"/prestamos/{prestamoId}/devolver", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al devolver libro: {ex.Message}");
                return false;
            }
        }

        public async Task<List<ReportePrestamo>> GenerarReporteActivosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/prestamos/reporte-activos");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ReportePrestamo>>() ?? new List<ReportePrestamo>();
                }

                return new List<ReportePrestamo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar reporte: {ex.Message}");
                return new List<ReportePrestamo>();
            }
        }

    }
}