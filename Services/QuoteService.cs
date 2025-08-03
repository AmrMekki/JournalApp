using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JournalApp.Services
{
    public class QuoteService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private class QuoteResponse
        {
            [JsonPropertyName("q")]
            public string Quote { get; set; }

            [JsonPropertyName("a")]
            public string Author { get; set; }
        }

        public static async Task<string> GetQuoteAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://zenquotes.io/api/random");
                var quoteData = JsonSerializer.Deserialize<List<QuoteResponse>>(response);

                if (quoteData != null && quoteData.Count > 0)
                {
                    var q = quoteData[0];
                    return $"\"{q.Quote}\" — {q.Author}";
                }
            }
            catch
            {
                // fallback if API fails
                return "Write your own story today. 📝";
            }

            return "Stay inspired. 💡";
        }
    }
}
