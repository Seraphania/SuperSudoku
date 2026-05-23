using Newtonsoft.Json.Linq;
using SuperSudoku.Models;

namespace SuperSudoku.Services
{
	internal static class ApiService
	{
		private static readonly HttpClient _client = new();
		private const int MaxRetries = 5;
		
		public static async IAsyncEnumerable<Puzzle> GetApiPuzzlesAsync(
			int count = 20
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
					$"with status code: {status}"
				);
			}

			string responseString = 
				await response.Content.ReadAsStringAsync();
			
			if (string.IsNullOrWhiteSpace(responseString))
				throw new InvalidOperationException(
					"Empty response from server."
				);

			JObject root = JObject.Parse(responseString);
			JObject? newBoard = 
				root.GetValue("newboard") as JObject;
			JArray? grids = 
				newBoard?["grids"] as JArray;

			if (grids == null)
			{
				throw new InvalidOperationException(
					"Invalid API response structure."				
				);
			}

			foreach (var grid in grids)
			{
				yield return ParseApiDataToPuzzle(grid);
			}			
		}

		private static Puzzle ParseApiDataToPuzzle(JToken grid) 
		{
			List<List<int>>? startingBoard = 
				grid["value"]?.ToObject<List<List<int>>>();

			List<List<int>>? solution =
				grid["solution"]?.ToObject<List<List<int>>>();

            List<List<int>>? currentBoard =
                grid["value"]?.ToObject<List<List<int>>>();

            string? difficulty = 
				grid["difficulty"]?.ToString();

			if (startingBoard == null || 
				solution == null || 
				string.IsNullOrWhiteSpace(difficulty) ||
				currentBoard == null
			) 
			{ 
				throw new InvalidOperationException(
					"Invalid data format from API."
				);
            }

			int[] Flatten(List<List<int>> grid)
			{
				return grid.SelectMany(row => row).ToArray();
			}

			Difficulty parsedDifficulty = 
				difficulty.ToLower() switch
			{
				"easy" => Difficulty.Easy,
				"medium" => Difficulty.Medium,
				"hard" => Difficulty.Hard,
				_ => throw new InvalidOperationException(
					$"Unknown difficulty level: {difficulty}"
                ),
            };

			return new Puzzle(
				new Board(Flatten(startingBoard)),
				new Board(Flatten(solution)),
                parsedDifficulty,
                new Board(Flatten(currentBoard))
            );
		}
    }
}
