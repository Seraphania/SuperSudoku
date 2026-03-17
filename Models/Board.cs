using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSudoku.Models
{
	internal class Board
	{
		public Cell[,] Cells { get; set; } = new Cell[9, 9];

		public Board(int[] jsonValues)
		{
			for (int i = 0; i < 81; i++)
			{
				int row = i / 9;
				int col = i % 9;
				Cells[row, col] = new Cell
				{
					Row = row,
					Column = col,
					Value = jsonValues[i] == 0 ? (int?)null : jsonValues[i],
					IsOriginal = jsonValues[i] != 0
				};
			}
		}
	}
}
