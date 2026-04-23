namespace SuperSudoku.Models
{
    /// <summary>
    /// Represents a Sudoku puzzle, containing the player's board, the solution, the difficulty level, and an optional current board state. This class serves as a container for all relevant information about a single Sudoku puzzle, allowing for easy management and access to its components throughout the application.
    /// </summary>
    public class Puzzle
	{
		public Board PlayerBoard { get; set; }	
		public Board Solution { get; set; }
		public Difficulty Difficulty { get; set; }
        public Board? CurrentBoard { get; set; }

        public Puzzle(Board playerBoard, Board solution, Difficulty difficulty, Board? currentBoard = null)
		{
			PlayerBoard = playerBoard;
			Solution = solution;
			Difficulty = difficulty;
			CurrentBoard = currentBoard;
		}
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
