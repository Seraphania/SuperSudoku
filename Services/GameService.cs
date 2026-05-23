using SuperSudoku.Models;
using Cell = SuperSudoku.Models.Cell;

namespace SuperSudoku.Services
{
    public class GameService
    {
        private SettingsService _settingsService;
        private PuzzleService _puzzleService;
        private Puzzle _activePuzzle = null!;
        public Puzzle ActivePuzzle => _activePuzzle;

        public GameService(
            SettingsService settingsService,
            PuzzleService puzzleService
        )
        {
            _settingsService = settingsService;
            _puzzleService = puzzleService;
        }

        public Puzzle StartGame(Difficulty difficulty)
        {
            _activePuzzle =
                _puzzleService.GetOrCreateActivePuzzle(difficulty);
            return _activePuzzle;
        }

        public void RestartGame()
        {
            _activePuzzle.CurrentBoard = _activePuzzle.StartingBoard.Clone();
            SaveCurrentGame();
        }

        public void SaveCurrentGame()
        {
            _puzzleService.SaveToDisk();
        }

        public bool CheckGameCompletion(Cell cell)
        {
            if (!IsBoardFull())
                return false;

            return (IsBoardSolved());
        }

        private bool IsBoardFull()
        {
            foreach (Cell cell in _activePuzzle.CurrentBoard.Cells)
            {
                if (cell.Value == null)
                    return false;
            }
            return true;
        }

        private bool IsBoardSolved()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (_activePuzzle.CurrentBoard.Cells[row, col].Value != _activePuzzle.Solution.Cells[row, col].Value
                    )
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
