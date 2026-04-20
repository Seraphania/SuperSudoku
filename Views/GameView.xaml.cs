using Microsoft.Maui.Layouts;
using SuperSudoku.Services;
using System.Data.Common;

namespace SuperSudoku.Views;

public partial class GameView : ContentPage
{
	private readonly GameService gameService;

	public GameView()
	{
		InitializeComponent();

		var app = Application.Current as App;

		if (app == null)
			throw new Exception("App is null");

		gameService = new GameService(
			app.SettingsService,
			app.PuzzleService
		);
		gameService.InitialiseGame();
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
					BindingContext = gameService.CurrentBoard.Cells[row, column],
				};

				cell.SetBinding(Entry.TextProperty,
					new Binding("DisplayValue", BindingMode.TwoWay));

                if (gameService.CurrentBoard.Cells[row, column].IsGiven)
                {
                    cell.FontAttributes = FontAttributes.Bold;
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

private async void SwipeGestureRecognizer_SwipedRight(object sender, SwipedEventArgs e)
	{
		gameService.SaveCurrentGame();
		await Navigation.PopAsync();
	}
}