namespace ConexiuniNonProfit.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
        public int MatchPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
