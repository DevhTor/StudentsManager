namespace StudentsManager.UI.Models
{
    public class Student
    {
        public long Id { get; set; }
        public required string StudentNumber { get; set; }
        public required string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
