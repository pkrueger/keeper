namespace keeper.Models;

public class Profile : IDbItem<string>
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string CoverImg { get; set; }
  public string Picture { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
