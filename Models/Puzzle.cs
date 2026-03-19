namespace SuperSudoku.Models
{
	internal class Puzzle
	{
		public Board PlayerBoard { get; set; }
		public Board Solution { get; set; }
		public Difficulty Difficulty { get; set; }
	}

	internal enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}
}
