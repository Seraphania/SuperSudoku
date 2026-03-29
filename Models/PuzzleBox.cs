namespace SuperSudoku.Models
{
    internal class PuzzleBox 
    {
        public List<Puzzle> EasyPuzzles { get; set; }
        public List<Puzzle> MediumPuzzles { get; set; }
        public List<Puzzle> HardPuzzles { get; set; }
        public Puzzle CurrentPuzzle { get; set; }

        public PuzzleBox() { }

        public PuzzleBox(
            List<Puzzle> easyPuzzles,
            List<Puzzle> mediaumPuzzles,
            List<Puzzle> hardPuzzles,
            Puzzle currentPuzzle)
        {
            this.EasyPuzzles = easyPuzzles;
            this.MediumPuzzles = mediaumPuzzles;
            this.HardPuzzles = hardPuzzles;
            this.CurrentPuzzle = currentPuzzle;
        }
    }
}