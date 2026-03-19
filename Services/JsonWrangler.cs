using Newtonsoft.Json;


namespace SuperSudoku.Services
{
	internal static class JsonWrangler
	{
		/// <summary>
		/// Deserializes the specified data to JSON.
		/// Reads from a file with a .json extension at the given path.
		/// </summary>
		/// <typeparam name="T">The type of the data to deserialize.</typeparam>
		/// <param name="path">The base file path (without extension) where the JSON file will be saved.</param>
		public static T Load<T>(string path) where T : class
		{
			string jsonPath = $"{path}.json";

			if (!File.Exists(jsonPath))
			{
				return null;
			}
			else
			{
				return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPath));
			}
		}

		/// <summary>
		/// Serializes the specified data to JSON.
		/// Saves it to a file with a .json extension at the given path.
		/// </summary>
		/// <typeparam name="T">The type of the data to serialize.</typeparam>
		/// <param name="path">The base file path (without extension) where the JSON file will be saved.</param>
		/// <param name="data">The data object to serialize and save.</param>
		public static void Save<T>(string path, T data)
		{
			string jsonPath = $"{path}.json";
			File.WriteAllText(jsonPath, JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));
		}
	}
}
