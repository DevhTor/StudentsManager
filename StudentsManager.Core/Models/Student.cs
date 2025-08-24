using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManager.Core.Models
{
    [Table("student")]
    public class Student
    {
        public long Id { get; set; }
        public required string StudentNumber { get; set; }
        public required string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }


        public ICollection<Homework>? Homeworks { get; set; }
    }
}