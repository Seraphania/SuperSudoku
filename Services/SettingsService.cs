using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	public class SettingsService 
	{
		private Settings _settings;
		public Difficulty SelectedDifficulty
		{
			get => _settings.SelectedDifficulty;
			set
			{
				_settings.SelectedDifficulty = value;
				SaveToDisk();
			}
		}
		public bool ShowTimer 
		{
			get
			{
				return Preferences.Get("ShowTimer", true);
            }
            set
            {
                Preferences.Set("ShowTimer", value);
            }
		}

        public bool DarkMode
        {
            get
            {
                return Preferences.Get("DarkMode", false);
            }
            set
            {
                Preferences.Set("DarkMode", value);
            }
        }

        public SettingsService() 
		{
			_settings = LoadFromDisk() ?? CreateDefaultSettings();
		}
		
		private Settings? LoadFromDisk()
		{
			return JsonWrangler.Load<Settings>(
					AppPaths.FilePath("settings")
			);
		}

		private Settings CreateDefaultSettings() 
		{
			return new Settings
			{
				SelectedDifficulty = Difficulty.Medium
			};
		}
		private void SaveToDisk()
		{
			JsonWrangler.Save(
				AppPaths.FilePath("settings"), 
				_settings
			);
		}


	}
}