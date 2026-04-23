namespace SuperSudoku.Models
{
    /// <summary>
    /// Represents a single cell in a Sudoku puzzle, containing its value, position (row and column), whether it is a given clue or not, and any notes the player may have added. This class serves as the fundamental building block for the Sudoku board, allowing for easy management of individual cell states and interactions within the game.
    /// </summary>
	public class Cell
	{
        public int? Value;
        public string DisplayValue
        {
            get => Value?.ToString() ?? "";
            set => Value = int.TryParse(value, out var v)? v : null;
        }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsGiven { get; set; }
		public List<int> Notes { get; set; } = new List<int>(); // For use in later feature
	}
}
