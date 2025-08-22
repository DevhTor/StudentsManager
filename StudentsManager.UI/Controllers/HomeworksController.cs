using Microsoft.AspNetCore.Mvc;
using StudentsManager.UI.Models;
using StudentsManager.UI.Services;

namespace StudentsManager.UI.Controllers
{
    public class HomeworksController : Controller
    {
        private readonly HomeworkApiService _homeworkApiService;
        private readonly StudentApiService _studentApiService; // Agregado para obtener la lista de estudiantes

        public HomeworksController(HomeworkApiService homeworkApiService, StudentApiService studentApiService)
        {
            _homeworkApiService = homeworkApiService;
            _studentApiService = studentApiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Homework> homeworks = await _homeworkApiService.GetAllHomeworksAsync();
            return View(homeworks);
        }

        public async Task<IActionResult> Add()
        {
            var students = await _studentApiService.GetAllStudentsAsync();
            ViewBag.Students = students; // Pasa la lista de estudiantes a la vista
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Homework homework)
        {
            var success = await _homeworkApiService.AddHomeworkAsync(homework);

            if (success)
            {
                TempData["StatusMessage"] = "Tarea agregada exitosamente.";
            }
            else
            {
                TempData["StatusMessage"] = "Error: No se pudo agregar la tarea.";
            }

            return RedirectToAction("Index");
        }
    }
}
