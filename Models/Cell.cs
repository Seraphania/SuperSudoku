using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SuperSudoku.Models
{
	public class Cell // : INotifyPropertyChanged
	{	
		public bool IsOriginal { get; set; }
		public List<int> Notes { get; set; } = new List<int>(); // For use in later feature
																//public int? _value;
		public int? Value;
		//{
		//	get => _value;
		//	set
		//	{
		//		if (_value != value)
		//		{
		//			_value = value;
		//			OnPropertyChanged();
		//		}
		//	}
		//}
		public int Row { get; set; }
		public int Column { get; set; }

		//public event PropertyChangedEventHandler PropertyChanged;

		//protected void OnPropertyChanged([CallerMemberName] string name = null) 
		//{
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		//} 
	}
}
