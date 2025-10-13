using System.Text.Json;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            var response = $"https://api.weatherapi.com/v1/current.json";
            
            using (var client = new HttpClient())
            {
                //Tallinn AccuWeather Locationkey = 127964
                var httpResponse = await client.GetAsync(response);
                string json = await httpResponse.Content.ReadAsStringAsync();

                List<AccuLocationRootDto> weatherData = 
                    JsonSerializer.Deserialize<List<AccuLocationRootDto>>(json);

            }

            return dto;
        }
    }
}
