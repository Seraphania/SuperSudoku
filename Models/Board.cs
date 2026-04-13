namespace SuperSudoku.Models
{
	public class Board
	{
		public static int boardSize = 9; // Placeholder, may expand in future
		public Cell[,] Cells { get; set; } = new Cell[boardSize, boardSize];

		public Board()
		{
			Cells = new Cell[boardSize, boardSize];
		}

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

        public Board Clone()
        {
            int[] values = new int[boardSize * boardSize];

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    values[row * boardSize + col] = Cells[row, col].Value ?? 0;
                }
            }
            return new Board(values);
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
