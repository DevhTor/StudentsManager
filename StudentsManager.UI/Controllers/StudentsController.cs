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

            // Manejar el caso de error (ej. mostrar un mensaje de error)
            return RedirectToAction("Index"); // O a una página de error
        }
    }
}