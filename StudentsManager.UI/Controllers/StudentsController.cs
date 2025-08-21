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

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            var success = await _studentApiService.AddStudentAsync(student);

            if (success)
            {
                TempData["StatusMessage"] = "Estudiante agregado exitosamente.";
            }
            else
            {
                TempData["StatusMessage"] = "Error: No se pudo agregar al estudiante.";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            var student = await _studentApiService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            var success = await _studentApiService.UpdateStudentAsync(student);

            if (success)
            {
                TempData["StatusMessage"] = "Estudiante actualizado exitosamente.";
            }
            else
            {
                TempData["StatusMessage"] = "Error: No se pudo actualizar al estudiante.";
            }

            return RedirectToAction("Index");
        }

        // Nueva acción para mostrar los detalles de un estudiante
        public async Task<IActionResult> Details(long id)
        {
            var student = await _studentApiService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound(); // Opcional: manejar el caso de que no se encuentre el estudiante
            }
            return View(student);
        }

        public async Task<IActionResult> StudentScores()
        {
            List<StudentFinalScore> studentScores = await _studentApiService.GetFinalScoresAsync();

            return View(studentScores);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var success = await _studentApiService.DeleteStudentAsync(id);

            if (success)
            {
                TempData["StatusMessage"] = "Estudiante eliminado exitosamente.";
            }

            else
            {
                TempData["StatusMessage"] = "Error: No se pudo eliminar al estudiante.";
            }

            return RedirectToAction("Index");
        }
    }
}
