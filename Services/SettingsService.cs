using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	/// <summary>
	/// Provides functionality to manage application settings, 
	/// including loading, updating, and persisting user preferences.
	/// </summary>
	public class SettingsService
	{
		public Settings settings;
		// For PC Saves to: C:\Users\<User>\AppData\Local\User Name\com.companyname.supersudoku\Data TODO: remove this later
		private readonly string _SettingsPath = Path.Combine(
			FileSystem.AppDataDirectory,
			"SuperSudoku",
			"settings"
		);

		/// <summary>
		/// Initializes a new instance of the SettingsService class and ensures the settings directory exists.
		/// </summary>
		public SettingsService() 
		{
			settings = new Settings();
			Directory.CreateDirectory(Path.Combine(
			FileSystem.AppDataDirectory,
			"SuperSudoku"
			));
			GetSettings();
		}

		/// <summary>
		/// Retrieves the current application settings, loading from storage or 
		/// initializing defaults if unavailable.
		/// </summary>
		/// <returns>The loaded or default application settings.</returns>
		public Settings GetSettings()
		{
			settings = JsonWrangler.Load<Settings>(_SettingsPath);
			if (settings != null)
			{
				return settings;
			}
			else
			{
				SetDefaultSettings();
				return settings;
			}
		}

		/// <summary>
		/// Updates the current settings and saves them to persistent storage.
		/// </summary>
		/// <param name="settings">The new settings to apply and persist.</param>
		public void UpdateSettings(Settings settings)
		{
			this.settings = settings;
            JsonWrangler.Save<Settings>(_SettingsPath, this.settings);
		}

		private void SetDefaultSettings()
		{
			Settings defaultSettings = new Settings();
			defaultSettings.SelectedDifficulty = Difficulty.Medium;
			settings = defaultSettings;
			UpdateSettings(settings);
		}
	}
}
