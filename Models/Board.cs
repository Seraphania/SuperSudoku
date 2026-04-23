namespace SuperSudoku.Models
{
    /// <summary>
    /// Represents the Sudoku board, which is a 9x9 grid of cells. This class provides methods to access rows, columns, and boxes of the board, as well as to retrieve individual cells. It serves as the core data structure for managing the state of the Sudoku puzzle throughout the game.
    /// </summary>
    public class Board
	{
		public static int boardSize = 9; // Placeholder, may expand in future
		public Cell[,] Cells { get; set; } = new Cell[boardSize, boardSize];

        /// <summary>
        /// Initializes a new instance of the Board class, creating a 9x9 grid of cells. Each cell is initialized with its corresponding row and column indices, and all values are set to null (indicating empty cells). This constructor sets up the basic structure of the Sudoku board, allowing for further manipulation and population of cell values as needed.
        /// </summary>
        public Board()
		{
			Cells = new Cell[boardSize, boardSize];
		}

        /// <summary>
        /// Initializes a new instance of the Board class using a flat array of integers representing the cell values. The input array should contain 81 integers (for a 9x9 board), where each integer corresponds to a cell's value (0 for empty cells). The constructor populates the Cells property based on the provided values, setting the appropriate row and column indices for each cell. This allows for easy creation of a board from a predefined set of values, such as those retrieved from an API or loaded from a file.
        /// </summary>
        /// <param name="values">An array of integers representing the cell values, where 0 indicates an empty cell.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Board(int[] values)
		{
			if (values == null) throw new ArgumentNullException(nameof(values));
			for (int i = 0; i < boardSize * boardSize; i++)
			{
				int row = i / boardSize;
				int col = i % boardSize;
				Cells[row, col] = new Cell
				{
					Row = row,
					Column = col,
					Value = values[i] == 0 ? (int?)null : values[i],
				};
			}
		}

        /// <summary>
        /// Creates a deep copy of the current Board instance. This method iterates through each cell in the original board and creates a new Cell object with the same properties (Row, Column, Value, IsGiven, and Notes) for the new board. The resulting Board instance is completely independent of the original, allowing for modifications without affecting the original state. This is particularly useful for scenarios such as undo/redo functionality or when exploring potential moves in a Sudoku solver.
        /// </summary>
        /// <returns>A new Board instance that is a deep copy of the current board.</returns>
        public Board Clone()
        {
            var newBoard = new Board();
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    var original = Cells[row, col];
                    newBoard.Cells[row, col] = new Cell
                    {
                        Row = original.Row,
                        Column = original.Column,
                        Value = original.Value,
                        IsGiven = original.IsGiven,
                        Notes = new List<int>(original.Notes)
                    };
                }
            }
            return newBoard;
        }

        /// <summary>
        /// Retrieves the Cell object located at the specified row and column indices. The method takes two integer parameters, row and col, which represent the zero-based indices of the desired cell. It returns the Cell object from the Cells 2D array corresponding to those indices. This method provides a convenient way to access individual cells on the board for reading or modifying their properties, such as value or notes.
        /// </summary>
        /// <param name="row">The zero-based index of the row.</param>
        /// <param name="col">The zero-based index of the column.</param>
        /// <returns>The Cell object located at the specified row and column.</returns>
        public Cell GetCell(int row, int col)
		{
			return Cells[row, col];
		}

        /// <summary>
        /// Retrieves an enumerable collection of Cell objects that belong to the specified row index. The method takes an integer parameter, row, which represents the zero-based index of the desired row. It uses a for loop to iterate through all columns in the specified row and yields each Cell object from the Cells 2D array corresponding to that row. This allows for easy access to all cells in a particular row, which can be useful for operations such as validating the row or updating cell values.
        /// </summary>
        /// <param name="row">The zero-based index of the row.</param>
        /// <returns>An enumerable collection of Cell objects in the specified row.</returns>
        public IEnumerable<Cell> GetRow(int row)
		{
			for (int c = 0; c < boardSize; c++)
			{
				yield return Cells[row, c];
			}
		}

        /// <summary>
        /// Retrieves an enumerable collection of Cell objects that belong to the specified column index. The method takes an integer parameter, column, which represents the zero-based index of the desired column. It uses a for loop to iterate through all rows in the specified column and yields each Cell object from the Cells 2D array corresponding to that column. This allows for easy access to all cells in a particular column, which can be useful for operations such as validating the column or updating cell values.
        /// </summary>
        /// <param name="column">The zero-based index of the column.</param>
        /// <returns>An enumerable collection of Cell objects in the specified column.</returns>
		public IEnumerable<Cell> GetColumn(int column)
		{
			for (int r = 0; r < boardSize; r++)
			{
				yield return Cells[r, column];
			}
		}

        /// <summary>
        /// Retrieves an enumerable collection of Cell objects that belong to the specified box index. The method takes an integer parameter, boxIndex, which represents the zero-based index of the desired 3x3 box (for a standard 9x9 Sudoku). It calculates the starting row and column indices for the box based on the boxIndex and iterates through the cells within that box using nested for loops. Each Cell object from the Cells 2D array corresponding to the box is yielded, allowing for easy access to all cells in a particular box, which can be useful for operations such as validating the box or updating cell values.
        /// </summary>
        /// <param name="boxIndex">The zero-based index of the 3x3 box.</param>
        /// <returns>An enumerable collection of Cell objects in the specified box.</returns>
		public IEnumerable<Cell> GetBox(int boxIndex)
		{
			int boardRoot = (int)Math.Sqrt(boardSize);
			int startRow = (boxIndex / boardRoot) * boardRoot;
			int startCol = (boxIndex % boardRoot) * boardRoot;
			for (int r = startRow; r < startRow +boardRoot; r++)
			{
				for (int c = startCol; c < startCol + boardRoot; c++)
				{
					yield return Cells[r, c];
				}
			}
		}
	}
}
