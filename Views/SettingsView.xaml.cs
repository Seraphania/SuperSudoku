using SuperSudoku.Models;
using SuperSudoku.Services;

namespace SuperSudoku.Views;

public partial class SettingsView : ContentPage
{
	private SettingsService _settingsService;
	private Settings _settings;

	public SettingsView(App? current)
	{
		InitializeComponent();

		// Access Global SettingsService from App.xaml.cs
		_settingsService = current.SettingsService;
		_settings = _settingsService.GetSettings();

		PickerDifficulty.SelectedIndex = (int)_settings.SelectedDifficulty;
	}

    private void PickerDifficulty_SelectedIndexChanged(object sender, EventArgs e)
    {
		if (PickerDifficulty.SelectedIndex < 0)
			return;

		_settings.SelectedDifficulty = (Difficulty)PickerDifficulty.SelectedIndex;
		_settingsService.UpdateSettings(_settings);
    }
}