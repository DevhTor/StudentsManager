namespace StudentsManager.Core.Models
{
    public class StudentFinalScore
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; } = "n/a";
        public double FinalScoreNumber { get; set; }
        public string FinalScoreLetter { get; set; } = "n/a";
    }
}