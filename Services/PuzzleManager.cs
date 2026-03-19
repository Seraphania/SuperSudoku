using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	internal class PuzzleManager
	{
		private PuzzleBox _PuzzleBox;
		private readonly string SuperSudokuPath = Path.Combine(
			FileSystem.AppDataDirectory,
			"SuperSudoku"
		);


		public PuzzleManager()
		{
			PuzzleBox puzzleBox = _PuzzleBox;

			Directory.CreateDirectory(SuperSudokuPath);
		}

		public void LoadOrRequestPuzzleBox()
		{
			string path = SuperSudokuPath + "puzzles";
			PuzzleBox puzzleBox = JsonWrangler.Load<PuzzleBox>(path);
			if (puzzleBox != null)
			{
				_PuzzleBox = puzzleBox;
			}
			if (puzzleBox.
		}


		private class PuzzleBox
		{
			private List<Puzzle> EasyPuzzles;
			private List<Puzzle> MediumPuzzles;
			private List<Puzzle> HardPuzzles;
			private Puzzle CurrentPuzzle;

			public PuzzleBox(List<Puzzle> easyPuzzles, List<Puzzle> mediumPuzzles, List<Puzzle> hardPuzzles, Puzzle currentPuzzle)
			{
				this.EasyPuzzles = easyPuzzles;
				this.MediumPuzzles = mediumPuzzles;
				this.HardPuzzles = hardPuzzles;
				this.CurrentPuzzle = currentPuzzle;
			}
		}
	}


}
