using System.ComponentModel.DataAnnotations.Schema;

namespace AspireSample.ApiService.Models
{
  public class Recipe
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Preparations { get; set; } = string.Empty;

    public string Ingredients { get; set; }

    public bool IsSecret { get; set; } = false;
  }
}
