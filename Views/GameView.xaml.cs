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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Get Elapsed time from GameService and start timer
        await CheckPuzzleStateAsync();
    }

    private void BuildGrid()
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

    private async void OnCellTextChanged(object? sender, TextChangedEventArgs e)

    {
        if (e.OldTextValue == e.NewTextValue)
            return;

        await CheckPuzzleStateAsync();
    }

    private async Task CheckPuzzleStateAsync()
    {
        if (!_app.GameService.IsBoardFull())
			return;

        if (_app.GameService.IsBoardSolved())
        {
            await HandleSolvedPuzzleAsync();
        }

        else
        {
            await HandleIncorrectSolutionAsync();
        }

    }

    private async Task HandleSolvedPuzzleAsync() 
	{
        if (_app.GameService.ActivePuzzle.IsCompleted)
            return;

		_app.GameService.ActivePuzzle.IsCompleted = true;

        _app.StatisticsService.RecordPuzzleCompletion(
			_app.GameService.ActivePuzzle.Difficulty,
			_app.GameService.ActivePuzzle.ElapsedTime);

        string action = await DisplayActionSheet(
			"Puzzle Solved!",
			"Cancel", null,
			"Next Puzzle",
			"Restart Puzzle",
			"View Stats"
		);

		switch (action)
		{
			case "Next Puzzle":
			{
				_app.GameService.CompletePuzzleAndQueueNext(
					_app.SettingsService.SelectedDifficulty);

				var currentPage = this;

				await Navigation.PushAsync(new GameView(_app));
				Navigation.RemovePage(currentPage);

				break;
			}
			case "Restart Puzzle":
			{
				_app.GameService.RestartPuzzle();

				var currentPage = this;

				await Navigation.PushAsync(new GameView(_app));
				Navigation.RemovePage(currentPage);

				break;
			}
			case "View Stats":
			{
				_app.GameService.CompletePuzzleAndQueueNext(
					_app.SettingsService.SelectedDifficulty);

				var currentPage = this;

				await Navigation.PushAsync(new StatisticsView(_app));
				Navigation.RemovePage(currentPage);

				break;
			}
		}
	}

	private async Task HandleIncorrectSolutionAsync() 
	{
		var restart = await DisplayAlert(
			"Incorrect Solution",
			"The board is full, but the solution is incorrect.",
			"Restart",
			"Continue"
		);

		if (restart)
		{
			_app.GameService.RestartPuzzle();

			var currentPage = this;

			await Navigation.PushAsync(new GameView(_app));
			Navigation.RemovePage(currentPage);
		}
	}

	private async void SwipeGestureRecognizer_SwipedRight(object? sender, SwipedEventArgs e)
	{
		_app.GameService.SaveCurrentGame();
		await Navigation.PopAsync();
	}

	private async void ButtonReset_clicked(object sender, EventArgs e)
	{
        _app.GameService.RestartPuzzle();

        var currentPage = this;

        await Navigation.PushAsync(new GameView(_app));
        Navigation.RemovePage(currentPage);
    }

    private async void ButtonNewPuzzle_clicked(object sender, EventArgs e)
    {
        _app.GameService.CompletePuzzleAndQueueNext(
            _app.SettingsService.SelectedDifficulty);

        var currentPage = this;

        await Navigation.PushAsync(new GameView(_app));
        Navigation.RemovePage(currentPage);
    }

    private void ButtonSolve_Clicked(object sender, EventArgs e)
    {
		_app.GameService.DebugFillBoard();
    }
}