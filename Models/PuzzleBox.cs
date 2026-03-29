namespace SuperSudoku.Models
{
    internal class PuzzleBox
    {
        internal List<Puzzle> EasyPuzzles;
        internal List<Puzzle> MediumPuzzles;
        internal List<Puzzle> HardPuzzles;
        internal Puzzle CurrentPuzzle;

        public PuzzleBox(List<Puzzle> easyPuzzles, List<Puzzle> mediumPuzzles, List<Puzzle> hardPuzzles, Puzzle currentPuzzle)
        {
            this.EasyPuzzles = easyPuzzles;
            this.MediumPuzzles = mediumPuzzles;
            this.HardPuzzles = hardPuzzles;
            this.CurrentPuzzle = currentPuzzle;
        }
    }
}
