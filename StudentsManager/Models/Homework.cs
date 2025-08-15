using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManager.Models
{
    [Table("homework")]
    public class Homework
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public string Description { get; set; }
        public string Grade { get; set; }

        // Propiedad de navegación
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}