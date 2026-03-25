using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	internal class GameService
	{
		// === Dependencies ===
		private readonly PuzzleService _puzzleService;

		// === Game State ===
		private Puzzle _activePuzzle;
		private Board _playerBoard;
		private Board _solutionBoard;

		// === Settings ===
		private ValidationMode _validationMode;

		// === Internal control ===
		private bool _isInternalUpdate;

		// === Public API ===

		// Start / reset game
		public void StartNewGame(Difficulty difficulty);
		public void RestartGame();

		// Access for UI binding
		public Board GetBoard();

		// Optional (if not fully binding-driven)
		public void SetCellValue(int row, int col, int? value);

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
