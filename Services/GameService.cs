using SuperSudoku.Models;
using Cell = SuperSudoku.Models.Cell;

namespace SuperSudoku.Services
{
    public class GameService
    {
        private PuzzleService _puzzleService;
        private Puzzle _activePuzzle = null!;
        public Puzzle ActivePuzzle => _activePuzzle;

        public GameService(
            PuzzleService puzzleService
        )
        {
            _puzzleService = puzzleService;
        }

        /// <summary>
        /// REMOVE THIS METHOD!
        /// Solves the current puzzle by copying the solution into the currentBoard.
        /// For testing UI behaviour only.
        /// </summary>
        public void DebugFillBoard()
        {
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    _activePuzzle.CurrentBoard.Cells[row, col].Value =
                        _activePuzzle.Solution.Cells[row, col].Value;
                }
            }
            SaveCurrentGame();
        }

        public Puzzle StartGame(Difficulty difficulty)
        {
            _activePuzzle =
                _puzzleService.GetOrCreateActivePuzzle(difficulty);
            return _activePuzzle;
        }

        public void RestartPuzzle()
        {
            _activePuzzle.CurrentBoard = _activePuzzle.StartingBoard.Clone();
            _activePuzzle.IsCompleted = false;
            SaveCurrentGame();
        }

        public bool CompletePuzzleAndQueueNext(Difficulty difficulty)
        {
            _activePuzzle.IsCompleted = true;
            _puzzleService.ClearActivePuzzle(difficulty);
            try
            {
                _activePuzzle =
                        _puzzleService.GetOrCreateActivePuzzle(difficulty);
                SaveCurrentGame();
                return true;
            }
            catch (InvalidOperationException)
            {
                SaveCurrentGame();
                return false;
            }
        }

        public void SaveCurrentGame()
        {
            _puzzleService.SaveToDisk();
        }

        public bool IsBoardFull()
        {
            foreach (Cell cell in _activePuzzle.CurrentBoard.Cells)
            {
                if (cell.Value == null)
                    return false;
            }
            return true;
        }

        public bool IsBoardSolved()
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