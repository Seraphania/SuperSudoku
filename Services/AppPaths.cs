namespace SuperSudoku.Services
{
	public static class AppPaths
	{
		// For PC Saves to: C:\Users\<User>\AppData\Local\User Name\SuperSudoku\Data\SuperSudoku TODO: remove this later
		public static readonly string AppDirectory =
			 Path.Combine(
				 FileSystem.AppDataDirectory,
				"SuperSudoku"
			);

		public static string FilePath(string name) 
		{
			return Path.Combine( AppDirectory, name );
		}
	}
}
