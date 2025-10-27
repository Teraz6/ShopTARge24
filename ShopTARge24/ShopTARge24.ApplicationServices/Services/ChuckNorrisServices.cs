using ShopTARge24.Core.Dto;
using System.Text.Json;
using System.Net.Http;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class ChuckNorrisServices : IChuckNorrisServices
    {
        private readonly HttpClient _httpClient;

        public ChuckNorrisServices(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
        }

        public async Task<ChuckNorrisDto> ChuckNorrisResult(ChuckNorrisDto dto)
        {
            var response = await _httpClient.GetAsync("jokes/random");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var chuckNorrisData = JsonSerializer.Deserialize<ChuckNorrisDto>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            dto.IconUrl = chuckNorrisData?.IconUrl;
            dto.Id = chuckNorrisData?.Id;
            dto.Url = chuckNorrisData?.Url;
            dto.Value = chuckNorrisData?.Value;

            return dto;
        }
    }
}
