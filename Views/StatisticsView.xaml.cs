using SuperSudoku.Models;

namespace SuperSudoku.Views;

public partial class StatisticsView : ContentPage
{
	private readonly App _app;

    public StatisticsView(App app)
	{
		InitializeComponent();
		_app = app;

		UpdateStatistics();
    }

    private void UpdateStatistics()
    {
        // Update the statistics labels with the current values from the app
        var easy = _app.StatisticsService.GetStatistics(Difficulty.Easy);
        var medium = _app.StatisticsService.GetStatistics(Difficulty.Medium);
        var hard = _app.StatisticsService.GetStatistics(Difficulty.Hard);

        LabelEasySolvedCount.Text = $"{easy.SolvedCount}";

        LabelEasyFastestTime.Text = $"{easy.FastestTime?.ToString(
                @"mm\:ss") ?? "--:--"}";

        LabelMediumSolvedCount.Text = $"{medium.SolvedCount}";

        LabelMediumFastestTime.Text = $"{medium.FastestTime?.ToString(
                @"mm\:ss") ?? "--:--"}";
        LabelHardSolvedCount.Text = $"{hard.SolvedCount}";         

        LabelHardFastestTime.Text = $"{hard.FastestTime?.ToString(
                @"mm\:ss") ?? "--:--"}";
    }

    private async void SwipeGestureRecognizer_SwipedRight(object? sender, SwipedEventArgs e)
    {
        await Navigation.PopAsync();
    }
}