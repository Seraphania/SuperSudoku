namespace SuperSudoku.Models
{
    public class Puzzle
	{
		public Board StartingBoard { get; set; }	
		public Board Solution { get; set; }
		public Difficulty Difficulty { get; set; }
        public Board CurrentBoard { get; set; }
		public bool IsCompleted { get; set; }
		public TimeSpan ElapsedTime { get; set; }

        public Puzzle(
			Board startingBoard,
            Board solution,
            Difficulty difficulty,
            Board? currentBoard,
            bool isCompleted = false,
            TimeSpan elapsedTime = default
        )
        {
            StartingBoard = startingBoard;
            Solution = solution;
            Difficulty = difficulty;
            CurrentBoard = currentBoard ?? startingBoard.Clone();
            IsCompleted = isCompleted;
            ElapsedTime = elapsedTime;
        }
    }
}
