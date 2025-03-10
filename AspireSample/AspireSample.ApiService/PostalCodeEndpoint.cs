using AspireSample.ApiService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AspireSample.ApiService
{
  public static class PostalCodeEndpoints
  {
    public static WebApplication MapPostalCodeEndpoints(this WebApplication app)
    {
      app.MapGet("/badpostalcode/{postalcode}/{housenumber}", async (string postalcode, string housenumber, FedDbContext context) =>
      {
        var sql = $"SELECT Id, Code, HouseNumber, City, StreetName FROM PostalCodes WHERE Code = '{postalcode}' AND HouseNumber = '{housenumber}'";
        var result = context.PostalCodes.FromSqlRaw(sql).AsQueryable() is { } code
            ? Results.Ok(code)
            : Results.NotFound();

        return result;
      })
      .WithName("GetBadPostalCode");

      app.MapGet("/postalcode/{postalcode}/{housenumber}", async (string postalcode, string housenumber, FedDbContext context) =>
      {
        var result = await context.PostalCodes
          .Where(pc =>pc.Code == postalcode && pc.HouseNumber == housenumber)
          .Select(pc => new { pc.Code, pc.City, pc.HouseNumber, pc.StreetName })
          .FirstOrDefaultAsync() is { } code
            ? Results.Ok(code)
            : Results.NotFound();

        return result;
      })
      .WithName("GetPostalCode");

      return app;
    }

    record Code(string PostalCode, string City, string HouseNumber, string Street);
  }
}