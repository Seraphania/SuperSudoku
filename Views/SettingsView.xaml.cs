using SuperSudoku.Models;
using SuperSudoku.Services;

namespace SuperSudoku.Views;

/// <summary>
/// Represents a page for viewing and modifying application settings.
/// </summary>
public partial class SettingsView : ContentPage
{
	private SettingsService _settingsService;
	private Settings _settings;
	private PuzzleService _puzzleService;
	private PuzzleBox _puzzleBox;

	/// <summary>
	/// Initializes a new instance of the SettingsView class using the specified application context.
	/// </summary>
	/// <param name="current">The current application instance, or null to use default settings.</param>
	public SettingsView(App? current)
	{
		InitializeComponent();

		_settingsService = current.SettingsService;
		_puzzleService = current.PuzzleService;
		_settings = _settingsService.GetSettings();
		_puzzleBox = _puzzleService.GetPuzzleBox();

		UpdatePickerItems();
		PickerDifficulty.SelectedIndex = (int)_settings.SelectedDifficulty;
	}

	private void UpdatePickerItems()
	{
		var items = new List<string>
		{
			$"Easy ({_puzzleBox.EasyPuzzles.Count})",
			$"Medium ({_puzzleBox.MediumPuzzles.Count})",
			$"Hard ({_puzzleBox.HardPuzzles.Count})"
		};

		PickerDifficulty.ItemsSource = items;
	}

	private void PickerDifficulty_SelectedIndexChanged(object sender, EventArgs e)
    {
		if (PickerDifficulty.SelectedIndex < 0)
			return;

		_settings.SelectedDifficulty = (Difficulty)PickerDifficulty.SelectedIndex;
		_settingsService.UpdateSettings(_settings);
    }
}