using Newtonsoft.Json;

namespace SuperSudoku.Services
{
	internal static class JsonWrangler
	{
		private static string GetJsonPath(string path) 
			=> $"{path}.json";

        public static T? Load<T>(string path) where T : class
		{
			string jsonPath = GetJsonPath(path);

			if (!File.Exists(jsonPath))
			{
				return null;
			}
			return JsonConvert.DeserializeObject<T>(
				File.ReadAllText(jsonPath));
		}

		public static void Save<T>(string path, T data)
		{
			string jsonPath = GetJsonPath(path);
			
			string json = JsonConvert.SerializeObject(
				data, Formatting.Indented);
            
			File.WriteAllText(jsonPath, json);
		}
	}
}
