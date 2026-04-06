using SuperSudoku.Models;
using SuperSudoku.Services;
using SuperSudoku.Views;

namespace SuperSudoku
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var app = Application.Current as App;

            var settingsService = app.SettingsService;
            var puzzleService = app.PuzzleService;

            InitializeComponent();
        }

        private async void ButtonSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView(Application.Current as App));
        }

        private async void ButtonPlay_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameView());
        }
    }
}
