using System.ComponentModel.DataAnnotations.Schema;

namespace AspireSample.ApiService.Models
{
  public class PostalCode
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public string Code { get; set; }

    public string StreetName { get; set; }

    public string HouseNumber { get; set; }

    public string City { get; set; }
  }
}
