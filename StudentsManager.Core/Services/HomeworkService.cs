using StudentsManager.Core.Models;
using StudentsManager.Core.Repositories;

namespace StudentsManager.Core.Services
{
    public class HomeworkService
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly IStudentRepository _studentRepository;

        public HomeworkService(IHomeworkRepository homeworkRepository, IStudentRepository studentRepository)
        {
            _homeworkRepository = homeworkRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Homework>> GetAllHomeworks()
        {
            return await _homeworkRepository.GetAllHomeworksAsync();
        }

        public async Task<Homework?> GetHomeworkById(long id)
        {
            return await _homeworkRepository.GetHomeworkByIdAsync(id);
        }

        public async Task<Homework> CreateHomework(Homework homework)
        {
            return await _homeworkRepository.AddHomeworkAsync(homework);
        }

        public async Task<bool> UpdateHomework(long id, Homework homework)
        {
            if (id != homework.Id)
            {
                return false;
            }

            var exists = await _homeworkRepository.HomeworkExistsAsync(id);
            if (!exists)
            {
                return false;
            }

            await _homeworkRepository.UpdateHomeworkAsync(homework);
            return true;
        }

        public async Task<bool> DeleteHomework(long id)
        {
            var homework = await _homeworkRepository.GetHomeworkByIdAsync(id);
            if (homework == null)
            {
                return false;
            }

            await _homeworkRepository.DeleteHomeworkAsync(homework);
            return true;
        }
    }
}