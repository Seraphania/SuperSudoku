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

        private async void ButtonPlay_Clicked(object sender, EventArgs e)
        {
            while (!_app.PuzzleService.IsReady)
            {
                await Task.Delay(100);
            }

            var difficulty = _app.SettingsService.SelectedDifficulty;
            var puzzle = _app.GameService.StartGame(difficulty);

            await Navigation.PushAsync(new GameView(puzzle));
        }
    }
}
