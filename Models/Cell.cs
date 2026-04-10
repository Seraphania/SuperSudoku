namespace SuperSudoku.Models
{
	public class Cell
	{	
		public bool IsOriginal { get; set; }
		public List<int> Notes { get; set; } = new List<int>(); // For use in later feature

		public int? Value;
        public string DisplayValue
        {
            get => Value?.ToString() ?? "";
            set => Value = int.TryParse(value, out var v) ? v : null;
        }
        public int Row { get; set; }
		public int Column { get; set; }

	}
}
