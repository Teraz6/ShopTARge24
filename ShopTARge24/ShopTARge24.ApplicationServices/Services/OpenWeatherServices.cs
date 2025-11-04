using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using ShopTARge24.Core.Dto.OpenWeatherDto;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        private readonly HttpClient _http = new();

        public async Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto)
        {
            string apiKey = "4a92d9e88b48d6af41547036eb52d055";

            var city = dto.CityName?.Trim() ?? "";

            var lat = dto.Latitude.ToString(CultureInfo.InvariantCulture);
            var lon = dto.Longitude.ToString(CultureInfo.InvariantCulture);
            var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

            var weatherJson = await _http.GetStringAsync(weatherUrl);
            var w = JsonConvert.DeserializeObject<OpenWeatherCurrentRootDto>(weatherJson);

            dto.Temperature = w.Main?.Temperature ?? 0;
            dto.FeelsLike = w.Main?.FeelsLike ?? 0;
            dto.TempMin = w.Main?.TempMin ?? 0;
            dto.TempMax = w.Main?.TempMax ?? 0;
            dto.Humidity = w.Main?.Humidity ?? 0;
            dto.Pressure = w.Main?.Pressure ?? 0;

            dto.Main = w.Weather?.FirstOrDefault()?.Main ?? "";
            dto.Description = w.Weather?.FirstOrDefault()?.Description ?? "";
            dto.Icon = w.Weather?.FirstOrDefault()?.Icon ?? "";

            dto.WindSpeed = w.Wind?.Speed ?? 0;
            dto.WindDegrees = w.Wind?.Degrees ?? 0;
            dto.WindGust = w.Wind?.Gust ?? 0;

            dto.Cloudiness = w.Clouds?.All ?? 0;
            dto.Visibility = w.Visibility;
            dto.Rain1h = w.Rain?.OneHour ?? 0;

            dto.Timezone = w.Timezone;
            dto.Sunrise = w.Sys?.Sunrise ?? 0;
            dto.Sunset = w.Sys?.Sunset ?? 0;

            return dto;
        }
    }
}