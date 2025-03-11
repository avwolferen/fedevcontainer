using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AspireSample.Web
{
    public class PostalCodeApiClient
    {
        private readonly HttpClient _httpClient;

        public PostalCodeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PostalCodeResponse?> GetPostalCodeAsync(string postalCode, string houseNumber)
        {
            var response = await _httpClient.GetAsync($"/postalcode/{postalCode}/{houseNumber}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PostalCodeResponse>();
            }
            return null;
        }
    }

    public class PostalCodeResponse
    {
        public string Code { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
    }
}
