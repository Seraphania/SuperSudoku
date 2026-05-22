namespace SuperSudoku.Models
{
    public class PuzzleBox 
    {
        public List<Puzzle> EasyPuzzles { get; set; } = new List<Puzzle>();
        public List<Puzzle> MediumPuzzles { get; set; } = new List<Puzzle>();
        public List<Puzzle> HardPuzzles { get; set; } = new List<Puzzle>();

        public Puzzle? ActiveEasyPuzzle { get; set; }
        public Puzzle? ActiveMediumPuzzle { get; set; }
        public Puzzle? ActiveHardPuzzle { get; set; }
    }
}