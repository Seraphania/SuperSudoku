using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	internal class SettingsService
	{
		private Settings? _settings;
		private readonly string SuperSudokuPath = Path.Combine(
			FileSystem.AppDataDirectory,
			"SuperSudoku"
		);
		
		public SettingsService() 
		{
			Directory.CreateDirectory(SuperSudokuPath);
			
		}

		public Settings GetSettings()
		{
			
			if (_settings != null)
			{
				return _settings;
			}
			else
			{
				string path = Path.Combine(SuperSudokuPath, "settings");
				Settings settings = JsonWrangler.Load<Settings>(path);
				if (settings != null)
				{
					_settings = settings;
					return _settings;
				}
				else
				{
					DefaultSettings();
					UpdateSettings(_settings);
					return _settings;
				}
			}
		}

		public void UpdateSettings(Settings settings)
		{
			_settings = settings;
			string path = Path.Combine(SuperSudokuPath, "settings");
			JsonWrangler.Save<Settings>(path, _settings);
		}

		private void DefaultSettings()
		{
				Settings defaultSettings = new Settings();
				defaultSettings.SelectedDifficulty = Difficulty.Medium;
				_settings = defaultSettings;
		}
	}
}
