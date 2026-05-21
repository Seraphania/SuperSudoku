using Newtonsoft.Json.Linq;

namespace SuperSudoku.Services
{
	internal static class ApiService
	{
		private static readonly HttpClient _client = new();
		private const int MaxRetries = 5;
		
		public static async IAsyncEnumerable<Puzzle> GetApiPuzzlesAsync(
			int count=20
		)
		{
			string query = 
				$"{{newboard(limit:{count}){{grids{{value,solution,difficulty}},results,message}}}}";

			string apiUrl = 
				"https://sudoku-api.vercel.app/api/dosuku?query=" 
				+ Uri.EscapeDataString(query);

			HttpResponseMessage? response = null;
			
			for (int i = 0; i < MaxRetries; i++)
			{
				try
				{
					using HttpRequestMessage request =
						new(HttpMethod.Get, apiUrl);

					response = await _client.SendAsync(request);

					if (response.IsSuccessStatusCode)
						break;
				}
				catch (HttpRequestException)
				{
				}
				if (i < MaxRetries - 1)
				{
					await Task.Delay(5000);
				}
			}

			if (response == null || !response.IsSuccessStatusCode)
			{
				string status =
					response?.StatusCode.ToString() ?? "No response";

				throw new HttpRequestException(
					$"After {MaxRetries} attempts, the server responded " +
					$"with status code: {response.StatusCode}"
				);
			}

			// TODO: Continue refator from here:
			// Improve code readbility (stylistically), and make This service return Puzzles!

			string responseString = 
				await response.Content.ReadAsStringAsync();
			
			if (string.IsNullOrWhiteSpace(responseString))
				throw new InvalidOperationException(
					"Empty response from server."
				);

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

		private static Puzzle ParseApiDataToPuzzle() 
		{
			throw new NotImplementedException();
		}
	}
}
