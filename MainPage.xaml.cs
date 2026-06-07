using SuperSudoku.Views;

namespace SuperSudoku
{
    public partial class MainPage : ContentPage
    {
        private readonly App _app;

        public MainPage()
        {
            InitializeComponent();
			_app = (App)Application.Current!;
		}

        private async void ButtonSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView(_app));
        }

        private async void ButtonHelp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpView(_app));
        }

        private async void ButtonStats_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticsView(_app));
        }

        private async void ButtonPlay_Clicked(object sender, EventArgs e)
        {
            var difficulty = _app.SettingsService.SelectedDifficulty;

            if (!_app.PuzzleService.HasPuzzle(difficulty))
            {
                await DisplayAlert(
                    "No Puzzles", 
                    "No puzzles available for the selected difficulty. Please check your settings.\n" +
                    "If the issue persists, please check your network connection", 
                    "OK"
                );
                return;
            }
            else 
            {
                _app.GameService.StartGame(difficulty);

                await Navigation.PushAsync(new GameView(_app));

            }
        }
    }
}
