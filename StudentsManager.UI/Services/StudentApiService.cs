using StudentsManager.UI.Models;
using System.Text;
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

        // Nuevo método para obtener un estudiante por su Id.
        public async Task<Student> GetStudentByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"/api/Students/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var student = JsonSerializer.Deserialize<Student>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return student;
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            var jsonContent = JsonSerializer.Serialize(student);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Students", httpContent);

            return response.IsSuccessStatusCode;
        }

        // Nuevo método para actualizar un estudiante existente.
        public async Task<bool> UpdateStudentAsync(Student student)
        {
            var jsonContent = JsonSerializer.Serialize(student);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Students/{student.Id}", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStudentAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Students/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
