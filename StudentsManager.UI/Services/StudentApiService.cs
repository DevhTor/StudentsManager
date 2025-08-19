using StudentsManager.UI.Models;
using System.Text.Json;

namespace StudentsManager.UI.Services
{
    public class StudentApiService
    {
        private readonly HttpClient _httpClient;

        public StudentApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StudentFinalScore>> GetFinalScoresAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7003/api/Students/FinalScores");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var finalScores = JsonSerializer.Deserialize<List<StudentFinalScore>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return finalScores ?? new List<StudentFinalScore>();
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Students");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var students = JsonSerializer.Deserialize<List<Student>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return students ?? new List<Student>();
        }

        public async Task<bool> DeleteStudentAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Students/{id}");

            // Verifica si la solicitud fue exitosa (código 204 No Content para DELETE)
            return response.IsSuccessStatusCode;
        }
    }
}