using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSudoku.Models;

namespace SuperSudoku.Services
{
    internal class GameService
    {
        private SettingsService _settingsService;
        private PuzzleService _puzzleService;

        public Board CurrentBoard { get; private set; }
        private Board _playerBoard;
        private Board _solutionBoard;

        public GameService(SettingsService settingsService, PuzzleService puzzleService)
        {
            _settingsService = settingsService;
            _puzzleService = puzzleService;
            var settings = _settingsService.GetSettings();
            var puzzle = puzzleService.GetActivePuzzle(settings.SelectedDifficulty, puzzleService.GetPuzzleBox());

            _solutionBoard = puzzle.Solution;
            _playerBoard = puzzle.PlayerBoard;
            CurrentBoard = puzzle.CurrentBoard ?? puzzle.PlayerBoard.Clone();
        }

        public void StartNewGame()
        {
            //_puzzleService.SetActivePuzzle(_settings.SelectedDifficulty);
            //_activePuzzle = _puzzleService.GetActivePuzzle(_settings.SelectedDifficulty);
        }

        public void RestartGame()
        {
            //_currentBoard = _activePuzzle.PlayerBoard;
        }

        /// <summary>
        /// Access for UI binding
        /// </summary>
        /// <returns>Board</returns>
        public Board GetBoard()
        {
            throw new NotImplementedException();
        }

        public void SaveCurrentGame();


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
