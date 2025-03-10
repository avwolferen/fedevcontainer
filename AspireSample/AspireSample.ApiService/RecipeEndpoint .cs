using AspireSample.ApiService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AspireSample.ApiService
{
  public static class RecipeEndpoints
  {
    public static WebApplication MapRecipeEndpoints(this WebApplication app)
    {
      app.MapGet("/badrecipe/{id}", async (string id, FedDbContext context) =>
      {
        var sql = $"SELECT * FROM Recipes WHERE IsSecret = 0 and Id = '{id}'";
        var result = context.Recipes.FromSqlRaw(sql).AsQueryable() is { } code
            ? Results.Ok(code)
            : Results.NotFound();

        return result;
      })
      .WithName("GetRecipeBadExample");

      app.MapGet("/recipe/{id}", async (int id, FedDbContext context) =>
      {
        var result = await context.Recipes.FirstOrDefaultAsync(r => r.Id == id && r.IsSecret == false) is { } code
            ? Results.Ok(code)
            : Results.NotFound();

        return result;
      })
      .WithName("GetRecipe");

      app.MapGet("/recipes", async (string id, FedDbContext context) =>
      {
        var result = context.Recipes.Where(r => r.IsSecret == false).AsQueryable() is { } code
            ? Results.Ok(code)
            : Results.NotFound();

        return result;
      })
      .WithName("GetRecipes");

      return app;
    }

    record Code(string PostalCode, string City, string HouseNumber, string Street);
  }
}