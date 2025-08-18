using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManager.Models
{
    [Table("student")]
    public class Student
    {
        public long Id { get; set; }
        public required string StudentNumber { get; set; }
        public required string Name { get; set; }
        public DateTime DateOfBirth { get; set; }


        public ICollection<Homework>? Homeworks { get; set; }
    }
}