using StudentsManager.UI.Models;
using System.Text;
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
            var response = await _httpClient.GetAsync("/api/Homeworks");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var homeworks = JsonSerializer.Deserialize<List<Homework>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return homeworks ?? new List<Homework>();
        }

        // Nuevo método para agregar una tarea
        public async Task<bool> AddHomeworkAsync(Homework homework)
        {
            var jsonContent = JsonSerializer.Serialize(homework);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Se ha corregido la URL para que sea relativa
            var response = await _httpClient.PostAsync("/api/Homeworks", httpContent);

            return response.IsSuccessStatusCode;
        }
    }
}
