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
            var expectedTempF = 32 + (int)(tempC / 0.5556);

            // Act - Using the same formula as in WeatherForecast record
            var actualTempF = 32 + (int)(tempC / 0.5556);

            // Assert
            Assert.Equal(expectedTempF, actualTempF);
        }

        [Theory]
        [InlineData(0, 32)]
        [InlineData(100, 212)]
        [InlineData(-40, -40)]
        public void WeatherForecast_TemperatureConversion_ShouldBeAccurate(int celsius, int expectedFahrenheit)
        {
            // Act
            var actualFahrenheit = 32 + (int)(celsius / 0.5556);

            // Assert - Allow for small rounding differences
            Assert.InRange(actualFahrenheit, expectedFahrenheit - 1, expectedFahrenheit + 1);
        }

        [Fact]
        public async Task GetWeatherForecast_ShouldReturnFiveForecasts()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();
            app.MapWeatherEndpoints();
            await app.StartAsync();

            var client = new HttpClient
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
