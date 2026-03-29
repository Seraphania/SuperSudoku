namespace SuperSudoku.Models
{
	internal class Board
	{
		public static int boardSize = 9;
		public Cell[,] Cells { get; set; } = new Cell[boardSize, boardSize];

		public Board(int[] values)
		{
			for (int i = 0; i < boardSize*boardSize; i++)
			{
				int row = i / boardSize;
				int col = i % boardSize;
				Cells[row, col] = new Cell
				{
					Row = row,
					Column = col,
					Value = values[i] == 0 ? (int?)null : values[i],
					IsOriginal = values[i] != 0 // Add a method later to update isOriginal for soulution boards
				};
			}
		}

		public Cell GetCell(int row, int col)
		{
			return Cells[row, col];
		}

		public IEnumerable<Cell> GetRow(int row)
		{
			for (int c = 0; c < boardSize; c++)
			{
				yield return Cells[row, c];
			}
		}

		public IEnumerable<Cell> GetColumn(int column)
		{
			for (int r = 0; r < boardSize; r++)
			{
				yield return Cells[r, column];
			}
		}

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
