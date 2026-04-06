namespace SuperSudoku.Models
{
    public class PuzzleBox 
    {
        public List<Puzzle> EasyPuzzles { get; set; } = new List<Puzzle>();
        public List<Puzzle> MediumPuzzles { get; set; } = new List<Puzzle>();
        public List<Puzzle> HardPuzzles { get; set; } = new List<Puzzle>();
        public Puzzle CurrentPuzzle { get; set; }

        public PuzzleBox() { }

        public PuzzleBox(
            List<Puzzle> easyPuzzles,
            List<Puzzle> mediaumPuzzles,
            List<Puzzle> hardPuzzles,
            Puzzle currentPuzzle)
        {
            this.EasyPuzzles = easyPuzzles?? new List<Puzzle>();
            this.MediumPuzzles = mediaumPuzzles?? new List<Puzzle>();
            this.HardPuzzles = hardPuzzles ?? new List<Puzzle>();
            this.CurrentPuzzle = currentPuzzle;
        }
    }
}