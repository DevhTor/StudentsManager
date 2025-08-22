using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManager.Models
{
    [Table("homework")]
    public class Homework
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public required string Description { get; set; }
        public int Score { get; set; }


        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

    }
}