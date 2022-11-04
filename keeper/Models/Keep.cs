namespace keeper.Models
{
  public class Keep : IDbItem<int>, IHasCreator
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int Views { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // NOTE Virtuals
    public Profile Creator { get; set; }
    public int kept { get; set; }
  }
}