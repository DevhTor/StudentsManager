using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Models;

namespace StudentsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeworksController(ApplicationDbContext context)
        {
            _context = context;
        }


        // Obtiene todas las calificaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Homework>>> GetHomeworks()
        {
            return await _context.Homeworks.ToListAsync();
        }


        // Obtiene una calificación por su Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Homework>> GetHomework(long id)
        {
            var homework = await _context.Homeworks.FindAsync(id);

            if (homework == null)
            {
                return NotFound();
            }

            return homework;
        }


        // Obtiene todas las calificaciones de un estudiante específico
        [HttpGet("ByStudent/{studentId}")]
        public async Task<ActionResult<IEnumerable<Homework>>> GetHomeworksByStudent(long studentId)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);
            if (!studentExists)
            {
                return NotFound("Estudiante no encontrado.");
            }

            var homeworks = await _context.Homeworks
                                        .Where(h => h.StudentId == studentId)
                                        .ToListAsync();
            return homeworks;
        }


        // Crea una nueva calificación
        [HttpPost]
        public async Task<ActionResult<Homework>> PostHomework(Homework homework)
        {
            _context.Homeworks.Add(homework);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomework", new { id = homework.Id }, homework);
        }


        // Actualiza una calificación
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomework(long id, Homework homework)
        {
            if (id != homework.Id)
            {
                return BadRequest();
            }

            _context.Entry(homework).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeworkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // Elimina una calificación
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomework(long id)
        {
            var homework = await _context.Homeworks.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }

            _context.Homeworks.Remove(homework);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HomeworkExists(long id)
        {
            return _context.Homeworks.Any(e => e.Id == id);
        }
    }
}