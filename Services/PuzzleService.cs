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
				NormalizePuzzleBox();
				await EnsurePuzzleStockAsync();
				SaveToDisk();

				IsReady = true;
			}
			finally 
			{
				IsLoading = false;
			}
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
			}
			catch 
			{
				_puzzleBox = new PuzzleBox();
			}
		}

		async Task EnsurePuzzleStockAsync()
		{
			while (_puzzleBox.EasyPuzzles.Count <= MinimumPuzzleCount ||
				_puzzleBox.MediumPuzzles.Count <= MinimumPuzzleCount ||
				_puzzleBox.HardPuzzles.Count <= MinimumPuzzleCount)
			{
				await FetchPuzzleAsync();
				NormalizePuzzleBox();
			}
		}

		public void SaveToDisk()
		{
			// NormalizePuzzleBox(); Needed?
			JsonWrangler.Save<PuzzleBox>(AppPaths.FilePath("puzzles"), _puzzleBox);
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

		//PuzzleService
		//├── InitializeAsync *
		//├── EnsurePuzzleStockAsync *
		//├── LoadFromDisk *
		//├── SaveToDisk *
		//├── FetchPuzzlesAsync *
		//├── GetPuzzle
		//└── SetCurrentPuzzle

		public Puzzle GetPuzzle()
        {
            return _puzzleBox.CurrentPuzzle;
        }

        public Puzzle GetCurrentPuzzle(Difficulty difficulty, PuzzleBox _PuzzleBox)
		{
			if (_PuzzleBox.CurrentPuzzle != null)
			{
				return _PuzzleBox.CurrentPuzzle;
			}
			else
			{
				SetCurrentPuzzle(difficulty);
				return _PuzzleBox.CurrentPuzzle;
			}
		}

		public void SetCurrentPuzzle(Difficulty difficulty)
		{
			List<Puzzle> sourceList = difficulty switch
            {
                Difficulty.Easy => _puzzleBox.EasyPuzzles,
                Difficulty.Medium => _puzzleBox.MediumPuzzles,
                Difficulty.Hard => _puzzleBox.HardPuzzles,
                _ => throw new NotImplementedException(),
            };

			if (sourceList.Count == 0)
			{
				throw new Exception("No puzzles available for this difficulty");
			}

			_puzzleBox.CurrentPuzzle = sourceList[0];
            sourceList.RemoveAt(0);
			EnsurePuzzleStockAsync();

			SaveToDisk();
		}

        private void NormalizePuzzleBox()
        {
            foreach (var puzzle in _puzzleBox.EasyPuzzles
                .Concat(_puzzleBox.MediumPuzzles)
                .Concat(_puzzleBox.HardPuzzles))
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
