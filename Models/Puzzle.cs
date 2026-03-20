namespace SuperSudoku.Models
{
	internal class Puzzle
	{
		private Board PlayerBoard { get; set; }
		private Board Solution { get; set; }
		private Difficulty Difficulty { get; set; }

		public Puzzle(Board playerBoard, Board solution, Difficulty difficulty)
		{
			PlayerBoard = playerBoard;
			Solution = solution;
			Difficulty = difficulty;
		}

		public Puzzle(){}
	}

	internal enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}
}
