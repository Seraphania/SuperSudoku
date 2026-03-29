using Newtonsoft.Json.Linq;

namespace SuperSudoku.Services
{
	internal static class ApiService
	{
		public static async IAsyncEnumerable<(List<List<int>>, List<List<int>>, string)> GetApiPuzzlesAsync(int count=20)
		{
			HttpClient client = new HttpClient();
			string query = $"{{newboard(limit:{count}){{grids{{value,solution,difficulty}},results,message}}}}";
			string apiURL = "https://sudoku-api.vercel.app/api/dosuku?query=" + Uri.EscapeDataString(query);
			var request = new HttpRequestMessage(HttpMethod.Get, apiURL);

			HttpResponseMessage response = null; // 
			int maxRetries = 5;

			for (int i = 0; i < maxRetries; i++)
			{
				response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
					break;
				// Wait before trying again
				await Task.Delay(30000);
			}

			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException($"After {maxRetries} attempts, the server responded with status code: {response.StatusCode}"); 
			
			string responseString = await response.Content.ReadAsStringAsync();
			if (string.IsNullOrWhiteSpace(responseString))
				throw new InvalidOperationException("Empty response from server.");

			JObject root = JObject.Parse(responseString);
			JObject newBoard = (JObject)root.GetValue("newboard");

			JArray grids = (JArray)newBoard["grids"];

			foreach (var grid in grids)
			{
				var playerBoard = grid["value"].ToObject<List<List<int>>>();
				var solution = grid["solution"].ToObject<List<List<int>>>();
				var difficulty = grid["difficulty"].ToString();

				yield return (playerBoard, solution, difficulty);
			}			
		}
	}
}
