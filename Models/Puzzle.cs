namespace SuperSudoku.Models
{
    /// <summary>
    /// Represents a Sudoku puzzle, containing the player's board, the solution, the difficulty level, and an optional current board state. This class serves as a container for all relevant information about a single Sudoku puzzle, allowing for easy management and access to its components throughout the application.
    /// </summary>
    public class Puzzle
	{
		public Board StartingBoard { get; set; }	
		public Board Solution { get; set; }
		public Difficulty Difficulty { get; set; }
        public Board CurrentBoard { get; set; }

        public Puzzle(Board startingBoard, Board solution, Difficulty difficulty, Board? currentBoard)
		{
			StartingBoard = startingBoard;
			Solution = solution;
			Difficulty = difficulty;
			CurrentBoard = currentBoard ?? startingBoard.Clone();
		}
	}
}
