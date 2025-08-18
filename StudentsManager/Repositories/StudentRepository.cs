using Microsoft.EntityFrameworkCore;
using StudentsManager.Data;
using StudentsManager.Models;

namespace StudentsManager.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(long id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<bool> StudentExistsAsync(long id)
        {
            return await _context.Students.AnyAsync(e => e.Id == id);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsWithHomeworksAsync()
        {
            return await _context.Students
                                 .Include(s => s.Homeworks)
                                 .ToListAsync();
        }
    }
}