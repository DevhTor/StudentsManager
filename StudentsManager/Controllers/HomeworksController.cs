using Microsoft.AspNetCore.Mvc;
using StudentsManager.Models;
using StudentsManager.Services;

namespace StudentsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private readonly HomeworkService _homeworkService;

        public HomeworksController(HomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        //Get All Homeworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Homework>>> GetHomeworks()
        {
            return Ok(await _homeworkService.GetAllHomeworks());
        }

        //Get Homework by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Homework>> GetHomework(long id)
        {
            var homework = await _homeworkService.GetHomeworkById(id);
            if (homework == null)
            {
                return NotFound();
            }
            return Ok(homework);
        }

        //Add Homework
        [HttpPost]
        public async Task<ActionResult<Homework>> AddHomework(Homework homework)
        {
            var newHomework = await _homeworkService.CreateHomework(homework);
            return CreatedAtAction("GetHomework", new { id = newHomework.Id }, newHomework);
        }

        //Update Homework
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHomework(long id, Homework homework)
        {
            var result = await _homeworkService.UpdateHomework(id, homework);
            if (!result)
            {
                return BadRequest(); // O NotFound() si el ID no existe
            }
            return NoContent();
        }

        //Delete Homework
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomework(long id)
        {
            var result = await _homeworkService.DeleteHomework(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}