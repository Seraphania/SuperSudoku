using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	internal class GameController
	{
		SettingsService _settingsService = new SettingsService();
		PuzzleService _puzzleService = new PuzzleService();

		Settings _settings;

		Puzzle _activePuzzle;
		Board _currentBoard;
        Board _playerBoard;
        Board _solutionBoard;

		public GameController(Settings settings=null, Puzzle puzzle=null) 
		{

            _settings = settings?? _settingsService.GetSettings();
			_activePuzzle = puzzle?? _puzzleService.GetActivePuzzle(_settings.SelectedDifficulty);
			_playerBoard = _activePuzzle.PlayerBoard;
			_solutionBoard = _activePuzzle.Solution;
            _currentBoard = _activePuzzle.CurrentBoard?? _playerBoard;
        }

        public void StartNewGame() 
		{
			_puzzleService.SetActivePuzzle(_settings.SelectedDifficulty);
			_activePuzzle = _puzzleService.GetActivePuzzle(_settings.SelectedDifficulty);
        }

		public void RestartGame()
		{
			_currentBoard = _activePuzzle.PlayerBoard;
        }

		/// <summary>
		/// Access for UI binding
		/// </summary>
		/// <returns>Board</returns>
		public Board GetBoard() 
		{
			return _currentBoard;
		}

		/// <summary>
		/// Check if puzzle is full and correct
		/// </summary>
		/// <returns> returns true if board is successfully completed </returns>
		//public bool IsPuzzleComplete() 
		//{
  //          if (_currentBoard == _solutionBoard)
  //          {
  //              return true;
  //          }
		//	for (int i = 0; i < 9; i++)
		//	{
		//		if (_currentBoard.GetRow(i).Any(0)) // syntax?
		//		{
		//			return false;
		//		}
		//	}
		//	for (int i = 0; i < 9; i++)
		//	{
		//		for (int j = 0; j < 9; j++)
  //              {
  //                  if (!IsMoveValid(i, j, _currentBoard.GetCell(i, j).Value))
  //                  {
  //                      return false;
  //                  }
  //              }
  //          }
		//	return true;
		//}

		public bool IsMoveValid(int row, int col, int? value) 
		{
			throw new NotImplementedException();
		}

		//// Validation - Later for helpful mode.
		//private bool ValidateCell(Cell cell);
		//private bool HasRowConflict(Cell cell);
		//private bool HasColumnConflict(Cell cell);
		//private bool HasBoxConflict(Cell cell);

		//// Behavior modes
		//private void HandleValidMove(Cell cell);
		//private void HandleInvalidMove(Cell cell);

		//// Utility
		//private void RevertCell(Cell cell, int? previousValue);
	}
}
