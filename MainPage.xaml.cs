using SuperSudoku.Services;

namespace SuperSudoku
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            testing(); // Temp for testing API call works
		}

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        // Temp for testing API call works
		private async void testing()
		{
			System.Diagnostics.Debug.WriteLine("Hello Debug!");
			await foreach (var (player, solution, difficulty) in ApiService.GetPuzzlesAsync(1))
			{
				System.Diagnostics.Debug.WriteLine(player[0][0]);

				break; // only test the first puzzle
			}
		}
	}
}
