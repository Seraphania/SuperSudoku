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

		_app.GameService.InitialiseGame();
		BuildGrid();
	}

	void BuildGrid()
	{
		for (int i = 0; i < 9; i++)
		{
			GridSudoku.AddRowDefinition(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			GridSudoku.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
		}

        for (int row = 0; row < 9; row++)
		{
			for (int column = 0; column < 9; column++)
			{
                var cell = new Entry
				{
					BackgroundColor = Colors.Transparent,
					HorizontalTextAlignment = TextAlignment.Center,
					VerticalTextAlignment = TextAlignment.Center,
					MaxLength = 1,
					BindingContext = _app.GameService.CurrentBoard.Cells[row, column],
				};

				cell.SetBinding(Entry.TextProperty,
					new Binding("DisplayValue", BindingMode.TwoWay));
                cell.TextChanged += OnCellTextChanged;

                if (_app.GameService.CurrentBoard.Cells[row, column].IsGiven)
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

				GridSudoku.Add(border);
				Grid.SetRow(border, row);
				Grid.SetColumn(border, column);
			}
		}
	}

    private void OnCellTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        var cell = (Cell)entry.BindingContext;

        _app.GameService.HandleCellChanged(cell);
    }

    private async void SwipeGestureRecognizer_SwipedRight(object sender, SwipedEventArgs e)
	{
		_app.GameService.SaveCurrentGame();
		await Navigation.PopAsync();
	}

	//Play pressed
	//             → Read selected difficulty from Settings
	//             → Check for active puzzle for that difficulty
	//                 → if exists: resume it
	//                 → if not: pull puzzle from cache/ create new active puzzle
	//             → Navigate to GameView
}