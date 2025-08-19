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
    }
}