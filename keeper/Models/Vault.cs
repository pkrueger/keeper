namespace keeper.Models
{
  public class Vault : IDbItem<int>, IHasCreator
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // NOTE Virtual
    public Profile Creator { get; set; }
  }
}