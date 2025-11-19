

using backend.Interfaces;


namespace backend.Services
{

    public class MuseumApiService : IMuseumApiService
    {
        private readonly HttpClient _httpClient;

        public MuseumApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MuseumApi");
        }

        public async Task<string> GetDataAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Museum API request failed: {response.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync();
        }

    }
}