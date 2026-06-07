using SuperSudoku.Models;

namespace SuperSudoku.Views;

public partial class SettingsView : ContentPage
{
	private readonly App _app;

	public SettingsView(App app)
	{
		InitializeComponent();
		_app = app;

		UpdatePickerItems();

		PickerDifficulty.SelectedIndex = 
			(int)_app.SettingsService.SelectedDifficulty;
		SwitchShowTimer.IsToggled = _app.SettingsService.ShowTimer;
    }

	private void UpdatePickerItems()
	{
		var puzzleBox = _app.PuzzleService.GetPuzzleBox();
		var items = new List<string>
		{
			$"Easy ({puzzleBox.EasyPuzzles.Count})",
			$"Medium ({puzzleBox.MediumPuzzles.Count})",
			$"Hard ({puzzleBox.HardPuzzles.Count})"
		};

		PickerDifficulty.ItemsSource = items;
	}

	private void PickerDifficulty_SelectedIndexChanged(object sender, EventArgs e)
    {
		if (PickerDifficulty.SelectedIndex < 0)
			return;

		_app.SettingsService.SelectedDifficulty =
			(Difficulty)PickerDifficulty.SelectedIndex;
    }

    private void SwitchShowTimer_Toggled(object sender, ToggledEventArgs e)
    {
        _app.SettingsService.ShowTimer = e.Value;
    }
}