using StudentsManager.Models;
using StudentsManager.Repositories;

namespace StudentsManager.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<Student?> GetStudentById(long id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<Student> CreateStudent(Student student)
        {
            // Aquí iría cualquier lógica de negocio adicional antes de guardar
            return await _studentRepository.AddStudentAsync(student);
        }

        public async Task<bool> UpdateStudent(long id, Student student)
        {
            if (id != student.Id)
            {
                return false;
            }

            var exists = await _studentRepository.StudentExistsAsync(id);
            if (!exists)
            {
                return false;
            }

            await _studentRepository.UpdateStudentAsync(student);
            return true;
        }

        public async Task<bool> DeleteStudent(long id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return false;
            }

            await _studentRepository.DeleteStudentAsync(student);
            return true;
        }

        public async Task<IEnumerable<StudentFinalScore>> GetFinalScoresAsync()
        {
            var students = await _studentRepository.GetAllStudentsWithHomeworksAsync();

            var finalScores = students.Select(s => new StudentFinalScore
            {
                StudentId = s.Id,
                StudentName = s.Name,
                FinalScoreNumber = s.Homeworks.Any() ? s.Homeworks.Average(h => h.Score) : 0,
            }).ToList();

            foreach (var score in finalScores)
            {
                score.FinalScoreLetter = ConvertScoreToLetter(score.FinalScoreNumber);
            }

            return finalScores;
        }

        private string ConvertScoreToLetter(double score)
        {
            if (score >= 90) return "A";
            if (score >= 80) return "B";
            if (score >= 70) return "C";
            if (score >= 60) return "D";
            return "F";
        }
    }
}