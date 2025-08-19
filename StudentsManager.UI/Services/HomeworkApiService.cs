using StudentsManager.UI.Models;
using System.Text.Json;

namespace StudentsManager.UI.Services
{
    public class HomeworkApiService
    {
        private readonly HttpClient _httpClient;

        public HomeworkApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Homework>> GetAllHomeworksAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7003/api/Homeworks");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var homeworks = JsonSerializer.Deserialize<List<Homework>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return homeworks ?? new List<Homework>();
        }
    }
}
