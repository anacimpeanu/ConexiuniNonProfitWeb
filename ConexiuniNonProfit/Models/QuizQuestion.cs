namespace ConexiuniNonProfit.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public Dictionary<string, Dictionary<string, int>> CategoryWeights { get; set; }

        public QuizQuestion()
        {
            Options = new List<string>();
            CategoryWeights = new Dictionary<string, Dictionary<string, int>>();
        }
    }
}
