namespace SuperSudoku.Models
{
    public class Statistics
    {
        public DifficultyStatistics Easy { get; set; } = new();
        public DifficultyStatistics Medium { get; set; } = new();
        public DifficultyStatistics Hard { get; set; } = new();
    }
}
