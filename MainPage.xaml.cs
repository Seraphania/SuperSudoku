using SuperSudoku.Models;
using SuperSudoku.Services;

namespace SuperSudoku
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            

            InitializeComponent();

            PuzzleService _puzzleService = new PuzzleService();
            PuzzleBox puzzleBox = await Task _puzzleService.LoadOrRequestPuzzleBox();

            GameController _gameManager = new GameController();

		}

        private void ButtonPLay_Clicked(object sender, EventArgs e)
        {

        }
    }
}
