using Microsoft.EntityFrameworkCore;
using StudentsManager.Core.Data;
using StudentsManager.Core.Models;

namespace StudentsManager.Core.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeworkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Homework>> GetAllHomeworksAsync()
        {
            return await _context.Homeworks.ToListAsync();
        }

        public async Task<Homework?> GetHomeworkByIdAsync(long id)
        {
            return await _context.Homeworks.FindAsync(id);
        }

        public async Task<Homework> AddHomeworkAsync(Homework homework)
        {
            _context.Homeworks.Add(homework);
            await _context.SaveChangesAsync();
            return homework;
        }

        public async Task UpdateHomeworkAsync(Homework homework)
        {
            _context.Entry(homework).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHomeworkAsync(Homework homework)
        {
            _context.Homeworks.Remove(homework);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HomeworkExistsAsync(long id)
        {
            return await _context.Homeworks.AnyAsync(e => e.Id == id);
        }
    }
}