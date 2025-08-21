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

        // Método para mostrar el formulario de edición de un estudiante existente.
        // Recibe el Id del estudiante como parámetro.
        public async Task<IActionResult> Edit(long id)
        {
            var student = await _studentApiService.GetStudentByIdAsync(id);
            if (student == null)
            {
                // Si el estudiante no se encuentra, redirige a la lista o muestra un error.
                return NotFound();
            }
            return View(student);
        }

        // Método para procesar los datos del formulario de edición y actualizar el estudiante.
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
