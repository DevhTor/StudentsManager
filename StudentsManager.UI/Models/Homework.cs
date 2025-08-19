namespace StudentsManager.UI.Models
{
    public class Homework
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public required string Description { get; set; }
        public int Score { get; set; }
    }
}
