using SuperSudoku.Models;

namespace SuperSudoku.Services
{
    public class PuzzleService
	{
		private PuzzleBox _puzzleBox;

		public bool IsReady { get; private set; }
		public bool IsLoading { get; private set; }
		private const int MinimumPuzzleCount = 3;
		private const int MaximumPuzzleCount = 50;

		public PuzzleService()
		{
			_puzzleBox = new PuzzleBox();
		}

		public async Task InitialiseAsync()
		{
			if (IsLoading || IsReady)
				return;

			IsLoading = true;
			try
			{
				LoadFromDisk();
				await EnsurePuzzleStockAsync();
				SaveToDisk();

				IsReady = true;
			}
			finally 
			{
				IsLoading = false;
			}
		}

		public bool HasPuzzle(Difficulty difficulty)
		{
			return GetActiveSlot(difficulty) != null ||
				GetPuzzleList(difficulty).Count > 0;
        }

        public void LoadFromDisk() 
		{
			try
			{
				PuzzleBox? puzzleBox = 
					JsonWrangler.Load<PuzzleBox>(
						AppPaths.FilePath("puzzles")
				);
				_puzzleBox = puzzleBox ?? new PuzzleBox();
				NormalisePuzzleBox(_puzzleBox);
            }
			catch 
			{
				_puzzleBox = new PuzzleBox();
			}
		}

        private void NormalisePuzzleBox(PuzzleBox puzzleBox)
        {
			foreach (Puzzle puzzle in puzzleBox.EasyPuzzles
				.Concat(puzzleBox.MediumPuzzles)
				.Concat(puzzleBox.HardPuzzles))
			{
                if (puzzle.CurrentBoard == null)
                    puzzle.CurrentBoard = puzzle.StartingBoard.Clone();
            }    
        }

        async Task EnsurePuzzleStockAsync()
		{
			while (_puzzleBox.EasyPuzzles.Count <= MinimumPuzzleCount ||
				_puzzleBox.MediumPuzzles.Count <= MinimumPuzzleCount ||
				_puzzleBox.HardPuzzles.Count <= MinimumPuzzleCount)
			{
				await FetchPuzzleAsync();
			}
		}

		public PuzzleBox GetPuzzleBox()
		{
			return _puzzleBox;
		}

		public void SaveToDisk()
		{
			JsonWrangler.Save(AppPaths.FilePath("puzzles"), _puzzleBox);
		}

		async Task FetchPuzzleAsync()
		{
			await foreach (Puzzle puzzle in ApiService.GetApiPuzzlesAsync())
			{
				switch (puzzle.Difficulty)
				{
					case Difficulty.Easy:
						if (_puzzleBox.EasyPuzzles.Count < 50)
							_puzzleBox.EasyPuzzles.Add(puzzle);
						break;

					case Difficulty.Medium:
						if (_puzzleBox.MediumPuzzles.Count < 50)
							_puzzleBox.MediumPuzzles.Add(puzzle);
						break;

					case Difficulty.Hard:
						if (_puzzleBox.HardPuzzles.Count < 50)
							_puzzleBox.HardPuzzles.Add(puzzle);
						break;
				}
			}
		}

		public Puzzle? GetActivePuzzle(Difficulty difficulty)
		{
			var active = GetActiveSlot(difficulty);
            if (active != null && !active.IsCompleted)
				return active;
			return null;
        }

		public Puzzle GetOrCreateActivePuzzle(Difficulty difficulty) 
		{
			var active = GetActiveSlot(difficulty);
			if (active != null)
				return active;

			var list = GetPuzzleList(difficulty);

			if (list.Count == 0)
				throw new InvalidOperationException(
					$"No cached {difficulty} puzzles available."
				);

			Puzzle puzzle = list[0];
			list.RemoveAt(0);

			SetActiveSlot(difficulty, puzzle);
			_ = EnsurePuzzleStockAsync();
			return puzzle;
		}

		public void ClearActivePuzzle(Difficulty difficulty)
		{
			SetActiveSlot(difficulty, null);
		}

		private List<Puzzle> GetPuzzleList(Difficulty difficulty) => difficulty switch
		{
			Difficulty.Easy => _puzzleBox.EasyPuzzles,
			Difficulty.Medium => _puzzleBox.MediumPuzzles,
			Difficulty.Hard => _puzzleBox.HardPuzzles,
			_ => throw new ArgumentOutOfRangeException(nameof(difficulty))
		};

		private Puzzle? GetActiveSlot(Difficulty difficulty) => difficulty switch
		{
			Difficulty.Easy => _puzzleBox.ActiveEasyPuzzle,
			Difficulty.Medium => _puzzleBox.ActiveMediumPuzzle,
			Difficulty.Hard => _puzzleBox.ActiveHardPuzzle,
			_ => throw new ArgumentOutOfRangeException(nameof(difficulty))
		};

		private void SetActiveSlot(Difficulty difficulty, Puzzle? puzzle)
		{
			switch (difficulty)
			{
				case Difficulty.Easy:
					_puzzleBox.ActiveEasyPuzzle = puzzle;
					break;

				case Difficulty.Medium:
					_puzzleBox.ActiveMediumPuzzle = puzzle;
					break;

				case Difficulty.Hard:
					_puzzleBox.ActiveHardPuzzle = puzzle;
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(difficulty));
			}
		}
	}
}
