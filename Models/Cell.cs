using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SuperSudoku.Models
{
    public class Cell : INotifyPropertyChanged
    {
        private int? _value;

        public int? Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayValue));
                }
            }
        }
        public string DisplayValue
        {
            get => Value?.ToString() ?? "";
            set => Value = int.TryParse(value, out var v) ? v : null;
        }
        public int Row { get; set; }
        public int Column { get; set; }
        public List<int> Notes { get; set; } = new List<int>(); // For use in later feature
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this, 
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

    


