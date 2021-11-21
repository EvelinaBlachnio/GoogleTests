using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project.Core.RestApiServices
{
    public class RestApiService : IRestApiService
    {
        private readonly HttpClient _httpClient;

        public RestApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetResponse<T>(string endpoint) where T: class
        {
            var response = await _httpClient.GetAsync(endpoint);
            var jsonString = await response.Content.ReadAsStringAsync();
            
            return !string.IsNullOrEmpty(jsonString) ? JsonConvert.DeserializeObject<T>(jsonString) : null;
        }
    }
}
