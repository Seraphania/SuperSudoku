using SuperSudoku.Models;
using Cell = SuperSudoku.Models.Cell;

namespace SuperSudoku.Services
{
    public class GameService
    {
        private SettingsService _settingsService;
        private PuzzleService _puzzleService;

        //public Board CurrentBoard => _activePuzzle.CurrentBoard;
        //public Board playerBoard { get; private set; }
        //private Board _solutionBoard;

        public GameService(
            SettingsService settingsService, 
            PuzzleService puzzleService
        )
        {
            _settingsService = settingsService;
            _puzzleService = puzzleService;
        }

        public void InitialiseGame()
        {
            var settings = _settingsService.GetSettings();
            _activePuzzle = _puzzleService.GetActivePuzzle(
                settings.SelectedDifficulty,
                _puzzleService.GetPuzzleBox()
            );

            if (_activePuzzle.CurrentBoard != null &&
                _activePuzzle.Difficulty == settings.SelectedDifficulty)
            {
                LoadExistingGame(_activePuzzle);
            }
            else
            {
                StartNewGame(_activePuzzle);
            }
        }

        private void LoadExistingGame(Puzzle puzzle)
        {
            _activePuzzle = puzzle;
            _solutionBoard = puzzle.Solution;
            playerBoard = puzzle.PlayerBoard;

        }

        private void StartNewGame(Puzzle puzzle)
        {
            _activePuzzle = puzzle;
            _solutionBoard = puzzle.Solution;
            playerBoard = puzzle.PlayerBoard;

            _activePuzzle.CurrentBoard = puzzle.PlayerBoard.Clone();
        }

        public void StartNewGame()
        {
            Settings settings = _settingsService.GetSettings();
            _puzzleService.SetActivePuzzle(settings.SelectedDifficulty);
            _activePuzzle = _puzzleService.GetActivePuzzle(
                settings.SelectedDifficulty,
                _puzzleService.GetPuzzleBox()
            );
            StartNewGame(_activePuzzle);
        }

        public void RestartGame()
        {
            _activePuzzle.CurrentBoard = playerBoard.Clone();
            SaveCurrentGame();
        }

        public void SaveCurrentGame()
        {
            _puzzleService.SavePuzzleBox();
        }

        public void HandleCellChanged(Cell cell) 
        {
            if (IsPuzzleComplete())
            {
                // Add to stats, congratulate user, set off fireworks... inform the king?
                return;
            }
            // Check if the move is valid - and if it's not...?
            // - validate move
            // - maybe revert
            // - maybe highlight errors

        }

        public bool IsPuzzleComplete()
        {
            if (CurrentBoard == _solutionBoard)
            {
                return true;
            }
            else
            {
                // Check if all cells are filled in - if they are, Check if the board is solved (not sure if solutions are unique from this API).
                return false;
            }
        }
    }
}
