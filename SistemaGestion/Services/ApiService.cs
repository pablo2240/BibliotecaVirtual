using Microsoft.AspNetCore.Identity.Data;
using System.Text;
using System.Text.Json;

namespace SistemaGestion.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:4000/api";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        // ===== AUTENTICACIÓN =====
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/auth/login", content);
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<LoginResponse>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new LoginResponse();
        }

        public async Task<ApiResponse> RegisterAsync(string name, string email, string password)
        {
            var registerData = new { name, email, password };
            var json = JsonSerializer.Serialize(registerData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/auth/register", content);
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new ApiResponse();
        }

        // ===== CURSOS =====
        public async Task<List<Curso>> GetCursosAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/courses");
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Curso>>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new List<Curso>();
        }

        public async Task<ApiResponse> CreateCursoAsync(Curso curso)
        {
            var json = JsonSerializer.Serialize(curso, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/courses", content);
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new ApiResponse();
        }

        public async Task<ApiResponse> DeleteCursoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/courses/{id}");
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new ApiResponse();
        }

        // ===== MATRÍCULAS =====
        public async Task<List<Matricula>> GetMisMatriculasAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/enrollments/my");
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Matricula>>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new List<Matricula>();
        }

        public async Task<ApiResponse> MatricularseAsync(int courseId)
        {
            var matriculaData = new { courseId };
            var json = JsonSerializer.Serialize(matriculaData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/enrollments", content);
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new ApiResponse();
        }

        public async Task<ApiResponse> CancelarMatriculaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/enrollments/{id}");
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new ApiResponse();
        }

        // ===== USUARIOS =====
        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/users");
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Usuario>>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new List<Usuario>();
        }

        // ===== PRÉSTAMOS =====
        public async Task<ApiResponse> DevolverPrestamoAsync(int prestamoId)
        {
            var response = await _httpClient.PutAsync($"{BaseUrl}/prestamos/{prestamoId}/devolver", null);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse { Error = result };
            }

            return JsonSerializer.Deserialize<ApiResponse>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new ApiResponse();
        }
    }
}