namespace SuperSudoku.Models
{
	public class Settings
	{
		public Difficulty SelectedDifficulty { get; set; }
	}

	public enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}
}
