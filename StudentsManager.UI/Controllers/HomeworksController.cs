using Microsoft.AspNetCore.Mvc;
using StudentsManager.UI.Models;
using StudentsManager.UI.Services;

namespace StudentsManager.UI.Controllers
{
    public class HomeworksController : Controller
    {
        private readonly HomeworkApiService _homeworkApiService;

        public HomeworksController(HomeworkApiService homeworkApiService)
        {
            _homeworkApiService = homeworkApiService;
        }

        public async Task<IActionResult> Homeworks()
        {
            List<Homework> homeworks = await _homeworkApiService.GetAllHomeworksAsync();
            return View(homeworks);
        }
    }
}
