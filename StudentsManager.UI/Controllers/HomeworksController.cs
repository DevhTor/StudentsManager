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
            var students = await _studentApiService.GetAllStudentsAsync(); // Obtiene la lista de estudiantes
            ViewBag.Students = students; // Pasa la lista de estudiantes a la vista
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

        // Método GET para editar una tarea
        public async Task<IActionResult> Edit(long id)
        {
            var homework = await _homeworkApiService.GetHomeworkByIdAsync(id); // Obtiene la tarea por su ID
            if (homework == null)
            {
                return NotFound();
            }

            var students = await _studentApiService.GetAllStudentsAsync();
            ViewBag.Students = students; // Pasa la lista de estudiantes a la vista
            return View(homework);
        }

        // Método POST para guardar la edición de una tarea
        [HttpPost]
        public async Task<IActionResult> Edit(Homework homework)
        {
            var success = await _homeworkApiService.UpdateHomeworkAsync(homework);

            if (success)
            {
                TempData["StatusMessage"] = "Tarea actualizada exitosamente.";
            }
            else
            {
                TempData["StatusMessage"] = "Error: No se pudo actualizar la tarea.";
            }

            return RedirectToAction("Index");
        }


        // Método para eliminar una tarea
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _homeworkApiService.DeleteHomeworkAsync(id);

            if (success)
            {
                TempData["StatusMessage"] = "Tarea eliminada exitosamente.";
            }
            else
            {
                TempData["StatusMessage"] = "Error: No se pudo eliminar la tarea.";
            }

            return RedirectToAction("Index");
        }

        // Método para mostrar los detalles de una tarea
        public async Task<IActionResult> Detail(long id)
        {
            var homework = await _homeworkApiService.GetHomeworkByIdAsync(id);
            if (homework == null)
            {
                return NotFound();
            }

            var students = await _studentApiService.GetAllStudentsAsync();
            // Busca el estudiante por ID
            var student = students?.FirstOrDefault(s => s.Id == homework.StudentId);
            ViewBag.StudentName = student?.Name ?? "Estudiante no encontrado"; // Pasa el nombre del estudiante a la vista

            return View(homework);
        }
    }
}
