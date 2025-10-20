using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using System.Text.Json;

namespace ShopTARge24.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {


            string apiKey = "";
            var response = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={dto.CityName}";

            using (var client = new HttpClient())
            {
                var httpResponse = await client.GetAsync(response);
                string json = await httpResponse.Content.ReadAsStringAsync();

                List<AccuCityCodeRootDto> weatherData = 
                    JsonSerializer.Deserialize<List<AccuCityCodeRootDto>>(json);

                dto.CityName = weatherData[0].LocalizedName;
                dto.CityCode = weatherData[0].Key;
            }

            string weatherResponse = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={dto.CityCode}";

            using (var clientWeather = new HttpClient())
            {
                var httpResponseWeather = await clientWeather.GetAsync(weatherResponse);
                string jsonWeather = await httpResponseWeather.Content.ReadAsStringAsync();

                List<AccuLocationRootDto> weatherDataResult =
                    JsonSerializer.Deserialize<List<AccuLocationRootDto>>(jsonWeather);

                dto.TempMinCelsius = weatherDataResult[0].Temperature.Metric.Value;
            }

            return dto;
        }
    }
}
