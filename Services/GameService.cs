using SuperSudoku.Models;

namespace SuperSudoku.Services
{
    internal class GameService
    {
        private SettingsService _settingsService;
        private PuzzleService _puzzleService;

        public Board CurrentBoard { get; private set; }
        public Board playerBoard { get; private set; }
        private Board _solutionBoard;

        public GameService(SettingsService settingsService, PuzzleService puzzleService)
        {
            _settingsService = settingsService;
            _puzzleService = puzzleService;
        }

        public void InitialiseGame()
        {
            var settings = _settingsService.GetSettings();
            Puzzle puzzle = _puzzleService.GetActivePuzzle(
                settings.SelectedDifficulty,
                _puzzleService.GetPuzzleBox()
            );

            if (puzzle.CurrentBoard != null)
            {
                LoadExistingGame(puzzle);
            }
            else
            {
                StartNewGame(puzzle);
            }
        }

        private void LoadExistingGame(Puzzle puzzle)
        {
            _solutionBoard = puzzle.Solution;
            playerBoard = puzzle.PlayerBoard;
            CurrentBoard = puzzle.CurrentBoard;
        }

        private void StartNewGame(Puzzle puzzle)
        {
            _solutionBoard = puzzle.Solution;
            playerBoard = puzzle.PlayerBoard;
            CurrentBoard = puzzle.PlayerBoard.Clone();
        }

        /// <summary>
        /// Explicitly start a new game of the selected difficulty discarding any partially completed game
        /// </summary>
        public void StartNewGame()
        {
            Settings settings = _settingsService.GetSettings();
            _puzzleService.SetActivePuzzle(settings.SelectedDifficulty);
            Puzzle puzzle = _puzzleService.GetActivePuzzle(
                settings.SelectedDifficulty,
                _puzzleService.GetPuzzleBox()
            );
            StartNewGame(puzzle);
        }

        /// <summary>
        /// Restart the current puzzle
        /// </summary>
        public void RestartGame()
        {
            CurrentBoard = playerBoard.Clone();
        }

        // =========================
        // Save progress
        // =========================
        public void SaveCurrentGame()
        {
            _puzzleService.SavePuzzleBox();
        }

        // =========================
        // Validation (existing WIP)
        // =========================
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
    }
}
