using Microsoft.AspNetCore.Mvc;
using StudentsManager.Core.Models;
using StudentsManager.Core.Services;

namespace StudentsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return Ok(await _studentService.GetAllStudents());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        //Get FinalScore
        [HttpGet("FinalScores")]
        public async Task<ActionResult<IEnumerable<StudentFinalScore>>> GetFinalScores()
        {
            var finalScores = await _studentService.GetFinalScoresAsync();
            return Ok(finalScores);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            var newStudent = await _studentService.CreateStudent(student);
            return CreatedAtAction("GetStudent", new { id = newStudent.Id }, newStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, Student student)
        {
            var result = await _studentService.UpdateStudent(id, student);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            var result = await _studentService.DeleteStudent(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}