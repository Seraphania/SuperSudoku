using SuperSudoku.Services;

namespace SuperSudoku
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            GameService gameManager = new GameService();

            for (int i = 0; i < 9; i++)
            {
				GridSudokuBoard.AddRowDefinition(new RowDefinition());
				GridSudokuBoard.AddColumnDefinition(new ColumnDefinition());
            }

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    var cell = new Entry
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        MaxLength = 1
                    };

                    Grid.SetRow(cell , row);
                    Grid.SetColumn(cell , column);
                    GridSudokuBoard.Children.Add(cell);
                    cell.SetBinding(Entry.TextProperty, "Value");
                    cell.BindingContext = _gameManager.Board[row][col];
                }
            }
		}

        

	}
}
