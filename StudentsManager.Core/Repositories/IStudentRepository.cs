using StudentsManager.Core.Models;

namespace StudentsManager.Core.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(long id);
        Task<bool> StudentExistsAsync(long id);
        Task<Student> AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsWithHomeworksAsync();
    }
}