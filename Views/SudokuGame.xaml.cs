using SuperSudoku.Services;

namespace SuperSudoku.Views;

public partial class SudokuGame : ContentPage
{
	public SudokuGame()
	{
        InitializeComponent();

        GameController _gameManager = new GameController();

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

                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, column);
                GridSudokuBoard.Children.Add(cell);
                cell.SetBinding(Entry.TextProperty, "Value");
                cell.BindingContext = _gameManager.Board[row][col];
            }
        }
    }

}
