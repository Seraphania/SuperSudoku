namespace SuperSudoku.Models
{
	public class Settings
	{
		public Difficulty SelectedDifficulty { get; set; }
	}

	/// <summary>
	/// Represents the difficulty level of a Sudoku puzzle, categorized as Easy, Medium, or Hard. This enumeration is used to classify puzzles based on their complexity and the techniques required to solve them.
	/// </summary>
	public enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}
}
