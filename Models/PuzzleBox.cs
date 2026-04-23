namespace SuperSudoku.Models
{
    /// <summary>
    /// The PuzzleBox class serves as a container for managing collections of Sudoku puzzles categorized by difficulty (Easy, Medium, Hard) and the current active puzzle. It provides properties to hold lists of puzzles for each difficulty level and a property to track the currently active puzzle. This class is essential for organizing and accessing puzzles within the application, allowing for efficient management of the puzzle collections and the current game state.
    /// </summary>
    public class PuzzleBox 
    {
        public List<Puzzle> EasyPuzzles { get; set; } = new List<Puzzle>();
        public List<Puzzle> MediumPuzzles { get; set; } = new List<Puzzle>();
        public List<Puzzle> HardPuzzles { get; set; } = new List<Puzzle>();
        public Puzzle CurrentPuzzle { get; set; }

        /// <summary>
        /// Initializes a new instance of the PuzzleBox class with empty lists for easy, medium, and hard puzzles, and a null current puzzle. This constructor sets up the basic structure of the PuzzleBox, allowing for the addition of puzzles and the management of the current active puzzle as needed throughout the application.
        /// </summary>
        public PuzzleBox() { }

        /// <summary>
        ///     Initializes a new instance of the PuzzleBox class with specified lists of easy, medium, and hard puzzles, as well as a current puzzle. This constructor allows for the creation of a PuzzleBox with predefined puzzles and an active puzzle, facilitating the management of the puzzle collections and the current state of the game from the outset.
        /// </summary>
        /// <param name="easyPuzzles">A list of easy difficulty puzzles.</param>
        /// <param name="mediumPuzzles">A list of medium difficulty puzzles.</param>
        /// <param name="hardPuzzles">A list of hard difficulty puzzles.</param>
        /// <param name="currentPuzzle">The current active puzzle.</param>
        public PuzzleBox(
            List<Puzzle> easyPuzzles,
            List<Puzzle> mediumPuzzles,
            List<Puzzle> hardPuzzles,
            Puzzle currentPuzzle)
        {
            this.EasyPuzzles = easyPuzzles?? new List<Puzzle>();
            this.MediumPuzzles = mediumPuzzles?? new List<Puzzle>();
            this.HardPuzzles = hardPuzzles ?? new List<Puzzle>();
            this.CurrentPuzzle = currentPuzzle;
        }
    }
}