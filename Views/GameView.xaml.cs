//using SuperSudoku.Converters;
using SuperSudoku.Services;

namespace SuperSudoku.Views;

public partial class GameView : ContentPage
{
	public GameView()
	{
		var gameService = new GameService(
			(Application.Current as App).SettingsService,
			((App)Application.Current).PuzzleService
			);

		InitializeComponent();

        for (int i = 0; i < 9; i++)
        {
            GridSudokuBoard.AddRowDefinition(new RowDefinition());
            GridSudokuBoard.AddColumnDefinition(new ColumnDefinition());
        }

        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                var cell = new Entry // custom entry with ref to class cell. 
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    MaxLength = 1,
                    BindingContext = gameService.CurrentBoard.Cells[row, column],
				};

                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, column);
                GridSudokuBoard.Children.Add(cell);
                cell.SetBinding(Entry.TextProperty, new Binding("Value", BindingMode.TwoWay));              
            }
        }
    }

    private async void SwipeGestureRecognizer_SwipedRight(object sender, SwipedEventArgs e)
    {
        // await GameController.SaveCurrentGameAsync(); - not yet implemented
        await Navigation.PopAsync();
    }
}