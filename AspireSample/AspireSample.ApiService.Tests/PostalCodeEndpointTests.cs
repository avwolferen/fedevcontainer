using AspireSample.ApiService;
using AspireSample.ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace AspireSample.ApiService.Tests
{
    public class PostalCodeEndpointTests
    {
        private FedDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<FedDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new FedDbContext(options);
            
            // Seed test data
            context.PostalCodes.Add(new PostalCode
            {
                Id = 1,
                Code = "8266AJ",
                HouseNumber = "19",
                StreetName = "Test Street",
                City = "Test City"
            });
            context.PostalCodes.Add(new PostalCode
            {
                Id = 2,
                Code = "1234AB",
                HouseNumber = "42",
                StreetName = "Another Street",
                City = "Another City"
            });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task PostalCode_ShouldReturnCorrectData_WhenFound()
        {
            // Arrange
            var context = CreateInMemoryDbContext();

            // Act
            var result = await context.PostalCodes
                .Where(pc => pc.Code == "8266AJ" && pc.HouseNumber == "19")
                .Select(pc => new { pc.Code, pc.City, pc.HouseNumber, pc.StreetName })
                .FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("8266AJ", result.Code);
            Assert.Equal("19", result.HouseNumber);
            Assert.Equal("Test Street", result.StreetName);
            Assert.Equal("Test City", result.City);
        }

        [Fact]
        public async Task PostalCode_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var context = CreateInMemoryDbContext();

            // Act
            var result = await context.PostalCodes
                .Where(pc => pc.Code == "9999ZZ" && pc.HouseNumber == "999")
                .Select(pc => new { pc.Code, pc.City, pc.HouseNumber, pc.StreetName })
                .FirstOrDefaultAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void PostalCode_Model_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            var postalCode = new PostalCode
            {
                Id = 1,
                Code = "1234AB",
                HouseNumber = "42",
                StreetName = "Main Street",
                City = "Amsterdam"
            };

            // Assert
            Assert.Equal(1, postalCode.Id);
            Assert.Equal("1234AB", postalCode.Code);
            Assert.Equal("42", postalCode.HouseNumber);
            Assert.Equal("Main Street", postalCode.StreetName);
            Assert.Equal("Amsterdam", postalCode.City);
        }

        [Theory]
        [InlineData("8266AJ", "19", true)]
        [InlineData("1234AB", "42", true)]
        [InlineData("9999ZZ", "999", false)]
        public async Task PostalCode_ShouldFindOrNotFind_BasedOnData(string code, string houseNumber, bool shouldFind)
        {
            // Arrange
            var context = CreateInMemoryDbContext();

            // Act
            var result = await context.PostalCodes
                .Where(pc => pc.Code == code && pc.HouseNumber == houseNumber)
                .FirstOrDefaultAsync();

            // Assert
            if (shouldFind)
            {
                Assert.NotNull(result);
                Assert.Equal(code, result.Code);
                Assert.Equal(houseNumber, result.HouseNumber);
            }
            else
            {
                Assert.Null(result);
            }
        }
    }
}
