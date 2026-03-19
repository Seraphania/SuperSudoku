using Newtonsoft.Json.Linq;

namespace SuperSudoku.Services
{
	internal static class ApiService
	{
		public static async IAsyncEnumerable<(List<List<int>>, List<List<int>>, string)> GetPuzzlesAsync(int count=20)
		{
			HttpClient client = new HttpClient();
			string query = $"{{newboard(limit:{count}){{grids{{value,solution,difficulty}},results,message}}}}";
			string apiURL = "https://sudoku-api.vercel.app/api/dosuku?query=" + Uri.EscapeDataString(query);
			var request = new HttpRequestMessage(HttpMethod.Get, apiURL);

			HttpResponseMessage response = await client.SendAsync(request);

			while (!response.IsSuccessStatusCode)
			{
				for (int i = 0; i < 5; i++)
				{
					Thread.Sleep(30000);
					response = await client.SendAsync(request);
				}
				throw new HttpRequestException($"After 5 attempts, the server responded with status code: {response.StatusCode}");
			}
			
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
