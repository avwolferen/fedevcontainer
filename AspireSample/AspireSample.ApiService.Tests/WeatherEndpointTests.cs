using AspireSample.ApiService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace AspireSample.ApiService.Tests
{
    public class WeatherEndpointTests
    {
        [Fact]
        public void WeatherForecast_ShouldCalculateTemperatureF_Correctly()
        {
            // Arrange
            var tempC = 20;
            var expectedTempF = 68; // 20°C = 68°F using formula: (20 * 9/5) + 32

            // Act - Test the actual WeatherForecast conversion
            var forecast = new WeatherApiEndpoints.WeatherForecast(DateOnly.FromDateTime(DateTime.Now), tempC, "Test");
            var actualTempF = forecast.TemperatureF;

            // Assert
            Assert.Equal(expectedTempF, actualTempF);
        }

        [Theory]
        [InlineData(0, 32)]
        [InlineData(100, 212)]
        [InlineData(-40, -40)]
        [InlineData(20, 68)]
        [InlineData(-10, 14)]
        public void WeatherForecast_TemperatureConversion_ShouldBeAccurate(int celsius, int expectedFahrenheit)
        {
            // Arrange & Act - Test the actual WeatherForecast conversion
            var forecast = new WeatherApiEndpoints.WeatherForecast(DateOnly.FromDateTime(DateTime.Now), celsius, "Test");
            var actualFahrenheit = forecast.TemperatureF;

            // Assert
            Assert.Equal(expectedFahrenheit, actualFahrenheit);
        }

        [Fact]
        public async Task GetWeatherForecast_ShouldReturnFiveForecasts()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();
            app.MapWeatherEndpoints();
            await app.StartAsync();

            using var client = new HttpClient
            {
                BaseAddress = new Uri($"http://localhost:{GetPort(app)}")
            };

            try
            {
                // Act
                var response = await client.GetAsync("/weatherforecast");

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                
                var forecasts = await response.Content.ReadFromJsonAsync<JsonElement>();
                var forecastArray = forecasts.EnumerateArray().ToList();
                
                Assert.Equal(5, forecastArray.Count);
            }
            finally
            {
                await app.StopAsync();
                await app.DisposeAsync();
            }
        }

        private static int GetPort(WebApplication app)
        {
            var addresses = app.Urls;
            var address = addresses.First();
            var uri = new Uri(address);
            return uri.Port;
        }
    }
}
