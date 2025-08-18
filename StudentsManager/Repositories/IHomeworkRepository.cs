using StudentsManager.Models;

namespace StudentsManager.Repositories
{
    public interface IHomeworkRepository
    {
        Task<IEnumerable<Homework>> GetAllHomeworksAsync();
        Task<Homework?> GetHomeworkByIdAsync(long id);
        Task<Homework> AddHomeworkAsync(Homework homework);
        Task UpdateHomeworkAsync(Homework homework);
        Task DeleteHomeworkAsync(Homework homework);
        Task<bool> HomeworkExistsAsync(long id);
    }
}