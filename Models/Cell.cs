namespace SuperSudoku.Models
{
	public class Cell
	{
        public int? Value { get; set; }
        public string DisplayValue
        {
            get => Value?.ToString() ?? "";
            set => Value = int.TryParse(value, out var v)? v : null;
        }
        public int Row { get; set; }
        public int Column { get; set; }
		public List<int> Notes { get; set; } = new List<int>(); // For use in later feature
	}
}
