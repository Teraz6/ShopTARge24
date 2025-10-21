using Nancy.Json;
using ShopTARge24.Core.Dto.WeatherWebClientDto;
using ShopTARge24.Core.Dto;
using System.Net;
using System.Text;

namespace ShopTARge24.ApplicationServices.Services
{
    public class ChuckNorrisServices
    {

        public async Task<ChuckNorrisDto> ChuckNorrisResult(ChuckNorrisDto dto)
        {

            var baseUrl = "https://api.chucknorris.io/jokes/random";

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={dto.CityName}"),
                    Content = new StringContent("", Encoding.UTF8, "application/json"),
                };

                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //127964
                var jsonResponse = await response.Content.ReadAsStringAsync();

            }

            using (var clientChuckNorris = new HttpClient())
            {
                var httpResponseChuckNorris = await clientChuckNorris.GetAsync(ChuckNorrisResponse);
                string jsonChuckNorris = await httpResponseChuckNorris.Content.ReadAsStringAsync();

                ChuckNorrisDto ChuckNorrisDto =
                    JsonSerializer.Deserialize<ChuckNorrisDto>(jsonChuckNorris);

                dto.IconUrl = ChuckNorrisDto.IconUrl;
                dto.Id = ChuckNorrisDto.Id;
                dto.Url = ChuckNorrisDto.Url;
                dto.Value = ChuckNorrisDto.Value;
            }

            return dto;
        }
    }
}
