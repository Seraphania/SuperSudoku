using SuperSudoku.Models;

namespace SuperSudoku.Services
{
    /// <summary>
    /// The PuzzleService class is responsible for managing the lifecycle of Sudoku puzzles within the application. It handles loading puzzles from local storage, requesting new puzzles from an external API when necessary, and maintaining the current active puzzle for the user. The service ensures that there are always enough puzzles available for each difficulty level and provides methods to retrieve and set the active puzzle.
    /// </summary>
    public class PuzzleService
	{
		PuzzleBox _PuzzleBox;
		private readonly string _PuzzlePath = Path.Combine(
			FileSystem.AppDataDirectory,
			"SuperSudoku",
			"puzzles"
		);

        /// <summary>
        /// Initializes a new instance of the PuzzleService class, creating a new PuzzleBox and ensuring the necessary directory structure exists for storing puzzles.
        /// </summary>
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
				NormalizePuzzleBox();
                SavePuzzleBox();
			}
			else
			{
				while (puzzleBox == null)
				{
                    await DecodeApiPuzzles();
                    await CheckPuzzleStore();
                    NormalizePuzzleBox();                    
                    SavePuzzleBox();
                    _PuzzleBox = puzzleBox;
                }                    
			}	
		}

		/// <summary>
		/// Gets the current puzzle box.
		/// </summary>
		/// <returns>The current puzzle box.</returns>
        public PuzzleBox GetPuzzleBox()
        {
            return _PuzzleBox;
        }

        /// <summary>
        /// Retrieves the active puzzle from the puzzle box. If no active puzzle is set, it sets a new active puzzle based on the specified difficulty and returns it.
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="_PuzzleBox"></param>
        /// <returns></returns>
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

			_PuzzleBox.CurrentPuzzle = sourceList[0];
            sourceList.RemoveAt(0);
			CheckPuzzleStore();
			
			SavePuzzleBox();
		}

		/// <summary>
		/// Asynchronously retrieves puzzles from the API.
		/// adds them to the appropriate puzzle collections based on difficulty.
		/// </summary>
		/// <remarks>
		/// Puzzles are categorized into easy, medium, and hard collections after being parsed from the API.
		/// API does not allow requesting specific difficulty, 
		///	excess puzzles for any difficulty (>50) are discarded
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
						if (_PuzzleBox.EasyPuzzles.Count < 50)
							_PuzzleBox.EasyPuzzles.Add(puzzle);
						break;

					case Difficulty.Medium:
						if (_PuzzleBox.MediumPuzzles.Count < 50)
							_PuzzleBox.MediumPuzzles.Add(puzzle);
						break;

					case Difficulty.Hard:
						if (_PuzzleBox.HardPuzzles.Count < 50)
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
			while (_PuzzleBox.EasyPuzzles.Count <=3 || 
				_PuzzleBox.MediumPuzzles.Count <=3 ||
				_PuzzleBox.HardPuzzles.Count <=3) 
			{
				await DecodeApiPuzzles();
                NormalizePuzzleBox();
                SavePuzzleBox();
			}
		}

		/// <summary>
		/// Saves the current puzzle box to the puzzles directory in JSON format.
		/// </summary>
		public void SavePuzzleBox()
		{
            NormalizePuzzleBox();
            JsonWrangler.Save<PuzzleBox>(_PuzzlePath, _PuzzleBox);
		}

        private void NormalizePuzzleBox()
        {
            foreach (var puzzle in _PuzzleBox.EasyPuzzles
                .Concat(_PuzzleBox.MediumPuzzles)
                .Concat(_PuzzleBox.HardPuzzles))
            {
                NormalizeBoard(puzzle.PlayerBoard);
                NormalizeBoard(puzzle.Solution, puzzle.PlayerBoard);

                if (puzzle.CurrentBoard != null)
                    NormalizeBoard(puzzle.CurrentBoard, puzzle.PlayerBoard);
            }
        }

        private void NormalizeBoard(Board board)
        {
            for (int row = 0; row < Board.boardSize; row++)
            {
                for (int col = 0; col < Board.boardSize; col++)
                {
                    var cell = board.Cells[row, col];

                    cell.IsGiven = cell.Value != null && cell.Value != 0;
                }
            }
        }

        private void NormalizeBoard(Board board, Board playerBoard)
        {
            for (int row = 0; row < Board.boardSize; row++)
            {
                for (int col = 0; col < Board.boardSize; col++)
                {
                    var cell = board.Cells[row, col];
					if (cell.Value == playerBoard.GetCell(row, col).Value)
						cell.IsGiven = true;
                }
            }
        }
    }
}
