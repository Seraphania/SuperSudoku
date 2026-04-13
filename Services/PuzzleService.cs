using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	public class PuzzleService
	{
		PuzzleBox _PuzzleBox;
		private readonly string _PuzzlePath = Path.Combine(
			FileSystem.AppDataDirectory,
			"SuperSudoku",
			"Puzzles"
		);

		public PuzzleService()
		{
			_PuzzleBox = new PuzzleBox();
			Directory.CreateDirectory(Path.Combine(
			FileSystem.AppDataDirectory,
			"SuperSudoku"
			));
		}

		/// <summary>
		/// Loads the puzzle box from local storage or requests it from the API if not available.	
		/// </summary>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task LoadOrRequestPuzzleBox()
		{
			PuzzleBox puzzleBox = JsonWrangler.Load<PuzzleBox>(_PuzzlePath);
			if (puzzleBox != null)
			{
				_PuzzleBox = puzzleBox;
				await CheckPuzzleStore();
				SavePuzzleBox();
			}
			else
			{
				await DecodeApiPuzzles();
				await CheckPuzzleStore();
				SavePuzzleBox();
			}	
		}

        public PuzzleBox GetPuzzleBox()
        {
            return _PuzzleBox;
        }

        public Puzzle GetActivePuzzle(Difficulty difficulty, PuzzleBox _PuzzleBox)
		{
			if (_PuzzleBox.CurrentPuzzle != null)
			{
				return _PuzzleBox.CurrentPuzzle;
			}
			else
			{
				SetActivePuzzle(difficulty);
				return _PuzzleBox.CurrentPuzzle;
			}
		}

		/// <summary>
		/// Sets the current puzzle to the first available puzzle of the specified difficulty.
		/// </summary>
		/// <param name="difficulty">The difficulty level of the puzzle to activate.</param>
		/// <exception cref="Exception">Thrown when no puzzles are available for the specified difficulty.</exception>
		public void SetActivePuzzle(Difficulty difficulty)
		{
			List<Puzzle> sourceList = difficulty switch
            {
                Difficulty.Easy => _PuzzleBox.EasyPuzzles,
                Difficulty.Medium => _PuzzleBox.MediumPuzzles,
                Difficulty.Hard => _PuzzleBox.HardPuzzles,
                _ => throw new NotImplementedException(),
            };

			if (sourceList.Count == 0)
			{
				throw new Exception("No puzzles available for this difficulty");
			}

			Puzzle active = sourceList[0];
			_PuzzleBox.CurrentPuzzle = active;
			sourceList.RemoveAt(0);
			
			SavePuzzleBox();
		}

		/// <summary>
		/// Asynchronously retrieves puzzles from the API.
		/// adds them to the appropriate puzzle collections based on difficulty.
		/// </summary>
		/// <remarks>
		/// Puzzles are categorized into easy, medium, and hard collections after being parsed from the API.
		/// API does not allow requesting specific difficulty, 
		///		excess puzzles for any difficulty (>50) are discarded
		/// response.</remarks>
		async Task DecodeApiPuzzles()
		{
			await foreach (var (playerboard, solution, difficulty) in ApiService.GetApiPuzzlesAsync())
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
                    _ => throw new NotImplementedException(),
                };

				Puzzle puzzle = new Puzzle(
						new Board(Flatten(playerboard)), 
						new Board(Flatten(solution)), 
						parsedDifficulty
					);

				switch (parsedDifficulty)
				{
					case Difficulty.Easy:
						if (_PuzzleBox.EasyPuzzles.Count <= 50) // Avoid storing too many puzzles.
							_PuzzleBox.EasyPuzzles.Add(puzzle);
						break;

					case Difficulty.Medium:
						if (_PuzzleBox.MediumPuzzles.Count <= 50)
							_PuzzleBox.MediumPuzzles.Add(puzzle);
						break;

					case Difficulty.Hard:
						if (_PuzzleBox.HardPuzzles.Count <= 50)
							_PuzzleBox.HardPuzzles.Add(puzzle);
						break;
				}
			}
		}

		/// <summary>
		/// Checks the puzzle store and decodes additional puzzles if any difficulty level has three or fewer puzzles remaining.
		/// </summary>
		async Task CheckPuzzleStore()
		{
			//PuzzleBox puzzleBox = _PuzzleBox;
			while (_PuzzleBox.EasyPuzzles.Count <=3 || 
				_PuzzleBox.MediumPuzzles.Count <=3 ||
				_PuzzleBox.HardPuzzles.Count <=3) 
			{
				await DecodeApiPuzzles();
				SavePuzzleBox();
			}
		}

		/// <summary>
		/// Saves the current puzzle box to the puzzles directory in JSON format.
		/// </summary>
		public void SavePuzzleBox()
		{
			JsonWrangler.Save<PuzzleBox>(_PuzzlePath, _PuzzleBox);
		}
	}
}
