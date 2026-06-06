using SuperSudoku.Models;

namespace SuperSudoku.Services
{
    public class StatisticsService
    {
        private Statistics _statistics;

        public StatisticsService()
        {
            _statistics = LoadFromDisk() ?? new Statistics();
        }

        private Statistics? LoadFromDisk()
        {
            return JsonWrangler.Load<Statistics>(
                    AppPaths.FilePath("statistics")
            );
        }

        private void SaveToDisk()
        {
            JsonWrangler.Save(
                AppPaths.FilePath("statistics"),
                _statistics
            );
        }

        public void ResetStatistics()
        {
            _statistics = new Statistics();
            SaveToDisk();
        }

        public void RecordPuzzleCompletion(Difficulty difficulty, TimeSpan timeTaken)
        {
            IncrementSolvedCount(difficulty);
            UpdateFastestTime(difficulty, timeTaken);

            SaveToDisk();
        }

        private void IncrementSolvedCount(Difficulty difficulty)
        {
            GetStatistics(difficulty).SolvedCount++;
        }

        private void UpdateFastestTime(Difficulty difficulty, TimeSpan time)
        {
            var stats = GetStatistics(difficulty);

            if (stats.FastestTime == null || 
                time < stats.FastestTime)
            {
                stats.FastestTime = time;
            }
        }

        public DifficultyStatistics GetStatistics(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return _statistics.Easy;
                case Difficulty.Medium:
                    return _statistics.Medium;
                case Difficulty.Hard:
                    return _statistics.Hard;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(difficulty));
            }
        }
    }
}