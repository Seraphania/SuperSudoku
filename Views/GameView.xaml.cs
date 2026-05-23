using Microsoft.Maui.Controls.PlatformConfiguration;
using SuperSudoku.Services;
using Cell = SuperSudoku.Models.Cell;

namespace SuperSudoku.Views;

public partial class GameView : ContentPage
{
	private readonly App _app;

	public GameView(App app)
	{
		InitializeComponent();
        _app = app;

		BuildGrid();
	}

	void BuildGrid()
	{
		BuildGridDefenitions();

		for (int row = 0; row < 9; row++)
		{
			for (int col = 0; col < 9; col++)
			{
				var border = CreateCell(row, col);

				GridSudoku.Add(border);
				Grid.SetRow(border, row);
				Grid.SetColumn(border, col);
			}
        }
    }

	private void BuildGridDefenitions() 
	{
		for (int i = 0; i < 9; i++)
		{
			GridSudoku.AddRowDefinition(
				new RowDefinition { Height = GridLength.Star });

			GridSudoku.AddColumnDefinition(
				new ColumnDefinition { Width = GridLength.Star });
		}
	}

	private Border CreateCell(int row, int column) 
	{
		var boardCell =
			_app.GameService.ActivePuzzle.CurrentBoard.Cells[row, column];
		var startingCell =
			_app.GameService.ActivePuzzle.StartingBoard.Cells[row, column];

        var cell = new Entry
		{
			BackgroundColor = Colors.Transparent,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			MaxLength = 1,
			BindingContext = boardCell,	
        };
        cell.SetBinding(
			Entry.TextProperty,
			new Binding("DisplayValue", BindingMode.TwoWay));
        cell.TextChanged += OnCellTextChanged; 

        if (startingCell.Value != null) 
		{
			cell.FontAttributes = FontAttributes.Bold;
			cell.IsReadOnly = true;
        }
        var border = new Border
        {
            Stroke = Color.FromArgb("#404040"),
            StrokeThickness = .1,
            Content = cell,
            Padding = 0,
            Margin = 0
        };

        return border;
    }

    private void OnCellTextChanged(object? sender, TextChangedEventArgs e)
    {
		if (e.OldTextValue == e.NewTextValue)
			return;

        if (sender is not Entry entry)
            return;

        var cell = (Cell)entry.BindingContext;

        _app.GameService.CheckGameCompletion(cell);
    }

    private async void SwipeGestureRecognizer_SwipedRight(object? sender, SwipedEventArgs e)
	{
		_app.GameService.SaveCurrentGame();
		await Navigation.PopAsync();
	}
}