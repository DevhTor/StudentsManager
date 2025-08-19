using Microsoft.AspNetCore.Mvc;
using StudentsManager.UI.Models;
using StudentsManager.UI.Services;

namespace StudentsManager.UI.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentApiService _studentApiService;

        public StudentsController(StudentApiService studentApiService)
        {
            _studentApiService = studentApiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Student> students = await _studentApiService.GetAllStudentsAsync();
            return View(students);
        }

        public async Task<IActionResult> StudentScores()
        {
            List<StudentFinalScore> studentScores = await _studentApiService.GetFinalScoresAsync();

            return View(studentScores);
        }
    }
}