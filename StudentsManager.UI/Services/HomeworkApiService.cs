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

        // Nuevo método para obtener una tarea por su ID
        public async Task<Homework> GetHomeworkByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"/api/Homeworks/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var homework = JsonSerializer.Deserialize<Homework>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return homework;
            }
            return null;
        }

        // Nuevo método para agregar una tarea
        public async Task<bool> AddHomeworkAsync(Homework homework)
        {
            var jsonContent = JsonSerializer.Serialize(homework);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Homeworks", httpContent);

            return response.IsSuccessStatusCode;
        }

        // Nuevo método para actualizar una tarea
        public async Task<bool> UpdateHomeworkAsync(Homework homework)
        {
            var jsonContent = JsonSerializer.Serialize(homework);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Homeworks/{homework.Id}", httpContent);

            return response.IsSuccessStatusCode;
        }

        // Nuevo método para eliminar una tarea
        public async Task<bool> DeleteHomeworkAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Homeworks/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
