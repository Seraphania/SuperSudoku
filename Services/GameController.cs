using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	internal class GameController
	{
		SettingsService _settingsService;
		PuzzleService _puzzleService;

		Puzzle _activePuzzle;
        Board _playerBoard;
        Board _solutionBoard;

		public GameController() 
		{
			_settingsService = new SettingsService();
			_puzzleService = new PuzzleService();
		}

        public void StartNewGame() 
		{
			Settings settings = _settingsService.GetSettings();
			Puzzle activePuzzle = _puzzleService.SetActivePuzzle(settings.SelectedDifficulty);
			_activePuzzle = activePuzzle;
			_playerBoard = activePuzzle.
		}

		public void RestartGame()
		{
			Settings settings = _settingsService.GetSettings();
            Puzzle activePuzzle = _puzzleService.GetActivePuzzle(settings.SelectedDifficulty);
        }

		// Access for UI binding
		public Board GetBoard() 
		{
			
		}

		// Game state checks
		public bool IsPuzzleComplete();
		public bool IsMoveValid(int row, int col, int? value);

		// Settings
		public void SetValidationMode(ValidationMode mode);

		// === Internal logic ===

		// Hook into cells
		private void SubscribeToBoard(Board board);
		private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e);

		// Validation
		private bool ValidateCell(Cell cell);
		private bool HasRowConflict(Cell cell);
		private bool HasColumnConflict(Cell cell);
		private bool HasBoxConflict(Cell cell);

		// Behavior modes
		private void HandleValidMove(Cell cell);
		private void HandleInvalidMove(Cell cell);

		// Utility
		private void RevertCell(Cell cell, int? previousValue);
	}
}
