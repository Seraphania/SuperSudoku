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
			_PuzzleBox = new PuzzleBox(
				new List<Puzzle>(),
				new List<Puzzle>(),
				new List<Puzzle>(),
				null
			);

			Directory.CreateDirectory(SuperSudokuPath);
		}

		// Load puzzles from local puzzles.json, otherwise get from API
		public void LoadOrRequestPuzzleBox()
		{
			string path = SuperSudokuPath + "puzzles";
			PuzzleBox puzzleBox = JsonWrangler.Load<PuzzleBox>(path);
			if (puzzleBox != null)
			{
				_PuzzleBox = puzzleBox;
				CheckPuzzleStore();
			}
			else
			{
				DecodeApiPuzzles();
				CheckPuzzleStore();
			}	
		}

		// Decode puzzles from API
		async  void DecodeApiPuzzles()
		{
			await foreach (var (playerboard, solution, difficulty) in ApiService.GetPuzzlesAsync())
			{
				int[] Flatten (List<List<int>> grid)
				{
					return grid.SelectMany(row => row).ToArray();
				}

				var parsedDifficulty = difficulty.ToLower() switch
				{
					"easy" => Difficulty.Easy,
					"medium" => Difficulty.Medium,
					"hard" => Difficulty.Hard,
				};

				var puzzle = new Puzzle(
						new Board(Flatten(playerboard)), 
						new Board(Flatten(solution)), 
						parsedDifficulty
					);

				switch (parsedDifficulty)
				{
					case Difficulty.Easy:
						_PuzzleBox.EasyPuzzles.Add(puzzle);
						break;

					case Difficulty.Medium:
						_PuzzleBox.MediumPuzzles.Add(puzzle);
						break;

					case Difficulty.Hard:
						_PuzzleBox.HardPuzzles.Add(puzzle);
						break;
				}
			}
		}

		// Check there are between 3 and 50 of each type of puzzle
		internal static void CheckPuzzleStore()
		{
			// Do stuff
		}

		
		internal class PuzzleBox
		{
			internal List<Puzzle> EasyPuzzles;
			internal List<Puzzle> MediumPuzzles;
			internal List<Puzzle> HardPuzzles;
			internal Puzzle CurrentPuzzle;

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
