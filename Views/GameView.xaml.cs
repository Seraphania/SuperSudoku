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
            GridSudokuBoard.AddRowDefinition(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            GridSudokuBoard.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        GridSudokuBoard.SizeChanged += (s, e) =>
        {
            var size = Math.Min(GridSudokuBoard.Width, GridSudokuBoard.Height);
            GridSudokuBoard.WidthRequest = size;
            GridSudokuBoard.HeightRequest = size;
        };

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

                var border = new Border
                {
                    
                    Padding = 0,
                    Margin = 0,
                    Content = cell,
                    Stroke = Colors.Black,
                    StrokeThickness = 1
                };

                Grid.SetRow(border, row);
                Grid.SetColumn(border, column);
                GridSudokuBoard.Children.Add(border);
                cell.SetBinding(Entry.TextProperty, new Binding("DisplayValue", BindingMode.TwoWay));              
            }
        }
    }

    private async void SwipeGestureRecognizer_SwipedRight(object sender, SwipedEventArgs e)
    {
        // await GameService.SaveCurrentGameAsync(); - not yet implemented
        await Navigation.PopAsync();
    }
}