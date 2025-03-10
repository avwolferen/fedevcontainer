using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using AspireSample.ApiService.Models;

namespace AspireSample.ApiService
{
  public class FedDbContext : DbContext
  {
    public FedDbContext(DbContextOptions options) : base(options)
    {
    }

    protected FedDbContext()
    {
    }

    public DbSet<PostalCode> PostalCodes { get; set; }

    public DbSet<Recipe> Recipes { get; set; }
  }
}
