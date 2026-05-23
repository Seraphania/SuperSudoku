namespace SuperSudoku.Models
{
    public class Board
	{
		public const int BoardSize = 9;
		public Cell[,] Cells { get; set; } = new Cell[BoardSize, BoardSize];

        public Board()
		{
		}

        public Board(int[] values)
		{
			if (values == null) 
				throw new ArgumentNullException(nameof(values));

			if (values.Length != BoardSize * BoardSize)
				throw new ArgumentException(
					$"Values array must have exactly {BoardSize * BoardSize} elements.",
					nameof(values)
				);

            for (int i = 0; i < BoardSize * BoardSize; i++)
			{
				int row = i / BoardSize;
				int col = i % BoardSize;
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
            var newBoard = new Board();
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var original = Cells[row, col];
                    newBoard.Cells[row, col] = new Cell
                    {
                        Row = original.Row,
                        Column = original.Column,
                        Value = original.Value,
                        Notes = new List<int>(original.Notes)
                    };
                }
            }
            return newBoard;
        }

        public Cell GetCell(int rowIndex, int columnIndex)
			=> Cells[rowIndex, columnIndex];

        public IEnumerable<Cell> GetRow(int rowIndex)
		{
			for (int c = 0; c < BoardSize; c++)
			{
				yield return Cells[rowIndex, c];
			}
		}

		public IEnumerable<Cell> GetColumn(int columnIndex)
		{
			for (int r = 0; r < BoardSize; r++)
			{
				yield return Cells[r, columnIndex];
			}
		}

		public IEnumerable<Cell> GetBox(int subgridIndex)
		{
			int boardRoot = (int)Math.Sqrt(BoardSize);
			int startRow = (subgridIndex / boardRoot) * boardRoot;
			int startCol = (subgridIndex % boardRoot) * boardRoot;
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
