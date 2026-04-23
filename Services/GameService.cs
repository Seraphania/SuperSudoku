using SuperSudoku.Models;
using Cell = SuperSudoku.Models.Cell;

namespace SuperSudoku.Services
{
    internal class GameService
    {
        private SettingsService _settingsService;
        private PuzzleService _puzzleService;

        private Puzzle _activePuzzle;

        public Board CurrentBoard => _activePuzzle.CurrentBoard;
        public Board playerBoard { get; private set; }
        private Board _solutionBoard;

        /// <summary>
        /// Initializes a new instance of the GameService class with the provided SettingsService and PuzzleService. This constructor sets up the necessary services for managing game state, including loading settings and retrieving puzzles. The GameService is responsible for handling game initialization, progress saving, and game state management throughout the application.
        /// </summary>
        /// <param name="settingsService">The SettingsService instance used to manage application settings.</param>
        /// <param name="puzzleService">The PuzzleService instance used to manage Sudoku puzzles.</param>
        public GameService(SettingsService settingsService, PuzzleService puzzleService)
        {
            _settingsService = settingsService;
            _puzzleService = puzzleService;
        }

        /// <summary>
        /// Initializes the game by loading the active puzzle based on the selected difficulty from the settings. If there is an existing game in progress (indicated by a non-null CurrentBoard), it loads that game state. Otherwise, it starts a new game using the player's board as the current board. This method ensures that the game state is correctly set up for the user to continue playing or start fresh based on their preferences.
        /// </summary>
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

        /// <summary>
        /// Starts a new game by retrieving a new puzzle based on the selected difficulty from the settings and setting it as the active puzzle in the PuzzleService. This method allows the user to begin a new game with a fresh puzzle, resetting any previous game state and providing a new challenge based on their chosen difficulty level.
        /// </summary>
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

        /// <summary>
        /// Restarts the current game by resetting the CurrentBoard to a clone of the original player board. This method allows the user to start over without changing the active puzzle, effectively discarding any progress made in the current game and providing a fresh start while keeping the same puzzle configuration. After resetting the board, it saves the current game state to ensure that the restart is persisted.
        /// </summary>
        public void RestartGame()
        {
            _activePuzzle.CurrentBoard = playerBoard.Clone();
            SaveCurrentGame();
        }

        /// <summary>
        /// Saves the current game state by updating the active puzzle's CurrentBoard property with the current board state and then persisting the changes using the PuzzleService. This method ensures that the user's progress is saved and can be resumed later, allowing for a seamless gaming experience even if the application is closed or interrupted.
        /// </summary>
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
