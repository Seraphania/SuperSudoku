namespace SuperSudoku.Models
{
	internal class Puzzle
	{
		public Board PlayerBoard { get; set; }	
		public Board Solution { get; set; }
		public Difficulty Difficulty { get; set; }
        public Board? CurrentBoard { get; set; }

        public Puzzle(Board playerBoard, Board solution, Difficulty difficulty, Board currentBoard = null)
		{
			PlayerBoard = playerBoard;
			Solution = solution;
			Difficulty = difficulty;
			CurrentBoard = currentBoard;
		}

	}

	internal enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}
}
